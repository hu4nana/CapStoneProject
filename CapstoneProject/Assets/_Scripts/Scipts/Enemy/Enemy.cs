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
    // 패턴의 지속시간
    protected float patternTimer;
    [SerializeField]
    protected int patternTime;

    // 공격 간격
    protected float attackTimer;
    [SerializeField]
    protected int attackTime;
    // Start is called before the first frame update

    protected GameObject target = null;
    [SerializeField]
    protected GameObject deadParticle;


    /*--Bool 작성해야 함 --*/
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
    /*--Bool 작성해야 함 --*/
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


    // 0 ~ maxPattern까지의 숫자를 랜덤 출력
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


    // -1 = 왼쪽, 1 = 오른쪽 , 어디를 바라볼지 정하는 함수
    public void Direction()
    {
        if (-90 < transform.rotation.y || transform.rotation.y < 90)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(dir, 0, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpd);
        }
    }

    // 목표를 감지하는 함수
    protected void TargetDetecter()
    {
        if (target != null)
        {
            isTrace = true;
        }
        TraceTimer();
    }
    // 대상의 x값에 따라 좌, 우로 이동하는 함수
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

    // 적이 탑지범위에서 나갔을 때 탐지가 종료되는 타이머
    protected void TraceTimer()
    {
        if (isTrace)
        {
            if (target == null)
            {
                Debug.Log("TraceTimer 작동됨");
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

    // 벽인지 바닥인지 Ray를 쏴서 확인하는 함수
    protected void CheckWallAndGroundCollision()
    {
        // 위치와 방향을 기반으로 레이캐스트를 발사하여 충돌 검사
        RaycastHit isFloorHit;
        RaycastHit isWallHit;
        RaycastHit isPlayerHit;
        isFloor = Physics.Raycast(floorCheck.position, Vector3.down, out isFloorHit, 0.1f);
        isWall = Physics.Raycast(wallCheck.position, transform.forward
            , out isWallHit, 0.1f, w_Layer);
        // 5f는 4칸
        isPlayer = Physics.Raycast(playerCheck.position, transform.forward
            , out isPlayerHit, 5f, p_Layer);
        Debug.DrawRay(playerCheck.position, transform.forward ,
            Color.red, 5f);
        if (isPlayer)
        {
            target = isPlayerHit.collider.gameObject;
            //Debug.Log(gameObject.name + "는 Player를 감지했다.");
            //Debug.Log(target);
        }
        else
        {
            target = null;
            //Debug.Log(target);
        }
        //if (isFloor && hit.collider.gameObject.layer != 8)
        //{
        //    Debug.Log(gameObject.name + "가 바닥에 닿아있습니다.");
        //    Debug.Log(isFloorHit.collider.gameObject.transform.localPosition);
        //    Debug.Log(isFloorHit.collider.gameObject.name + "가 바닥에 닿아있습니다.");
        //}

        //if (isWall)
        //{
        //    Debug.Log("벽과 충돌함");
        //    Debug.Log(isWallHit.collider.gameObject.transform.localPosition);
        //    Debug.Log(isWallHit.collider.gameObject.name + "가 벽에 닿아있습니다.");
        //}
    }

    // 체력이 0일 때 죽음
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
