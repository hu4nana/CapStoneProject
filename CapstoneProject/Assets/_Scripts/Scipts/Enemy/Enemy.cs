using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float maxHp;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float rotateSpd;


    protected Rigidbody rigid;
    protected Animator ani;

    protected int maxPattern;
    protected int curPattern;
    protected int dir = -1;

    protected float curHp;
    // ������ ���ӽð�
    protected float patternTimer;
    [SerializeField]
    protected int patternTime;

    // ���� ����
    protected float attackTimer;
    [SerializeField]
    protected int attackTime;
    // Start is called before the first frame update

    protected GameObject target = null;
    [SerializeField]
    protected GameObject deadParticle;


    /*--Bool �ۼ��ؾ� �� --*/
    public float CurHp { get { return curHp; }}
    public bool isDamaged { get; set; }
    public bool isDead { get; set; }
    public bool isEnd { get; set; }
    public bool isTrace { get; set; }
    public bool isWall { get;set; }
    public bool isFloor { get; set; }
    public bool isPlayer { get; set; }

    [SerializeField]protected float traceTimer = 0;
    [SerializeField] protected float deadTimer = 0;
    protected float traceTime = 0;
    protected float deadTime = 0;
    protected bool isPlayedDead = false;
    /*--Bool �ۼ��ؾ� �� --*/
    [SerializeField]
    protected Transform floorCheck;
    [SerializeField]
    protected Transform wallCheck;
    [SerializeField]
    protected Transform playerCheck;
    [SerializeField]
    protected LayerMask w_Layer;
    [SerializeField]
    protected LayerMask f_Layer;
    [SerializeField]
    protected LayerMask p_Layer;

    //private void Awake()
    //{
    //    ani = GetComponentInChildren<Animator>();
    //    //ani = GetComponent<Animator>();
    //    rigid = GetComponent<Rigidbody>();
    //}


    // 0 ~ maxPattern������ ���ڸ� ���� ���
    protected void PatternSelecter()
    {
        if (!isTrace)
        {
            if (isEnd)
            {
                curPattern = Random.Range(0, maxPattern);
                patternTime = Random.Range(3, 6);
                isEnd = false;
                Debug.Log(curPattern);
            }
            else
            {
                patternTimer += Time.deltaTime;
                if (patternTimer >= patternTime)
                {
                    patternTimer = 0;
                    isEnd = true;
                }
            }
        }
    }


    // -1 = ����, 1 = ������ , ��� �ٶ��� ���ϴ� �Լ�
    public void Direction()
    {
        if (-90 < transform.rotation.y || transform.rotation.y < 90)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(dir, 0, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpd);
        }
    }

    // ��ǥ�� �����ϴ� �Լ�
    protected void TargetDetecter()
    {
        if (target != null)
        {
            isTrace = true;
        }
        TraceTimer();
    }
    // ����� x���� ���� ��, ��� �̵��ϴ� �Լ�
    protected void TargetTracer(float value)
    {
        if (isTrace&&target != null)
        {
            if (target.transform.position.x - transform.position.x > 0)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }
            Direction();
            if (isFloor)
            {
                rigid.velocity = new Vector3(dir * speed * value, rigid.velocity.y,0);
            }
            else
            {
                rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
            }
        }
    }

    // ���� ž���������� ������ �� Ž���� ����Ǵ� Ÿ�̸�
    protected void TraceTimer()
    {
        if (isTrace)
        {
            if (target == null)
            {
                Debug.Log("TraceTimer �۵���");
                if (traceTime <= traceTimer)
                {
                    traceTime += Time.deltaTime;
                }
                else
                {
                    isTrace = false;
                    target = null;
                }
            }
        }
        //if (isDamaged)
        //{
        //    traceTime = 0;
        //}
    }

    // ������ �ٴ����� Ray�� ���� Ȯ���ϴ� �Լ�
    protected void CheckWallAndGroundCollision()
    {
        // ��ġ�� ������ ������� ����ĳ��Ʈ�� �߻��Ͽ� �浹 �˻�
        RaycastHit isFloorHit;
        RaycastHit isWallHit;
        RaycastHit isPlayerHit;
        isFloor = Physics.Raycast(floorCheck.position, Vector3.down, out isFloorHit, 0.1f);
        isWall = Physics.Raycast(wallCheck.position, transform.forward
            , out isWallHit, 0.1f, w_Layer);
        // 5f�� 4ĭ
        isPlayer = Physics.Raycast(playerCheck.position, transform.forward
            , out isPlayerHit, 5f, p_Layer);
        Debug.DrawRay(playerCheck.position, transform.forward ,
            Color.red, 5f);
        if (isPlayer)
        {
            target = isPlayerHit.collider.gameObject;
            //Debug.Log(gameObject.name + "�� Player�� �����ߴ�.");
            //Debug.Log(target);
        }
        else
        {
            target = null;
            //Debug.Log(target);
        }
        //if (isFloor && hit.collider.gameObject.layer != 8)
        //{
        //    Debug.Log(gameObject.name + "�� �ٴڿ� ����ֽ��ϴ�.");
        //    Debug.Log(isFloorHit.collider.gameObject.transform.localPosition);
        //    Debug.Log(isFloorHit.collider.gameObject.name + "�� �ٴڿ� ����ֽ��ϴ�.");
        //}

        //if (isWall)
        //{
        //    Debug.Log("���� �浹��");
        //    Debug.Log(isWallHit.collider.gameObject.transform.localPosition);
        //    Debug.Log(isWallHit.collider.gameObject.name + "�� ���� ����ֽ��ϴ�.");
        //}
    }

    // ü���� 0�� �� ����
    protected void Dead()
    {
        if (curHp <= 0)
        {
            ani.SetBool("isDead",true);
            isDead = true;
        }
        if(isDead)
        {
            if(ani.GetCurrentAnimatorStateInfo(0).IsName("Death")&&
                ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                if (!isPlayedDead)
                {
                    Instantiate(deadParticle, transform);
                    isPlayedDead = true;
                }
                GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                gameObject.layer = 12;
            }
            deadTime += Time.deltaTime;
            if (deadTime >= deadTimer)
            {
                Destroy(gameObject);
            }
           
        }
    }
    protected void DamagedTimer()
    {
        if(isDamaged)
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Damaged")&&
                ani.GetCurrentAnimatorStateInfo(0).normalizedTime>=0.1f)
            {
                isDamaged = false;
            }
        }
    }
}
