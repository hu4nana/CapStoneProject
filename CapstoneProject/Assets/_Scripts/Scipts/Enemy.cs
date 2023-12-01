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
    protected float speed;
    [SerializeField]
    protected float rotateSpd;


    protected Rigidbody rigid;
    protected Animator ani;

    protected int maxPattern;
    protected int curPattern;
    [SerializeField]
    protected int dir = -1;
    // ������ ���ӽð�
    protected float patternTimer;
    protected int patternTime;
    // Start is called before the first frame update

    protected GameObject target = null;

    
    /*--Bool �ۼ��ؾ� �� --*/
    public bool isDamaged { get; set; }
    public bool isDead { get; set; }
    public bool isEnd { get; set; }
    public bool isTrace { get; set; }
    public bool isWall { get;set; }
    public bool isFloor { get; set; }

    [SerializeField]protected float traceTimer = 0;
    protected float traceTime = 0;
    
    /*--Bool �ۼ��ؾ� �� --*/
    [SerializeField]
    protected Transform floorCheck;
    [SerializeField]
    protected Transform wallCheck;
    [SerializeField]
    protected LayerMask w_Layer;
    [SerializeField]
    protected LayerMask f_Layer;
    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        //ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }



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
                //Debug.Log(curPattern);
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

    // ���� ž���������� ������ �� Ž���� ����Ǵ� Ÿ�̸�
    protected void TraceTimer()
    {
        if (isTrace)
        {
            isTrace = true;
            traceTime += Time.deltaTime;
            if (traceTime >= traceTimer)
            {
                isTrace= false;
                target = null;
            }
        }
        if (isDamaged)
        {
            traceTime = 0;
        }
    }

    // ����� x���� ���� ��, ��� �̵��ϴ� �Լ�
    protected void TraceTarget(int value)
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
            rigid.velocity = new Vector2(dir * speed * value, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
    }
    public bool GetDead()
    {
        return isDead;
    }
    public void SetDead(bool value)
    {
        isDead = value;
    }
    public bool GetDamaged()
    {
        return isDamaged;
    }
    public void SetDamaged(bool value)
    {
        isDamaged=value;
    }
    // ������ �ٴ����� Ray�� �ؼ� Ȯ���ϴ� �Լ�
    protected void CheckWallAndGroundCollision()
    {
        // �÷��̾��� ��ġ�� ������ ������� ����ĳ��Ʈ�� �߻��Ͽ� �浹 �˻�
        RaycastHit hitInfo;
        isFloor = Physics.Raycast(floorCheck.position, Vector3.down, out hitInfo, 0.1f);
        isWall = Physics.Raycast(wallCheck.position, transform.forward, out hitInfo, 0.5f, w_Layer);
        if (Physics.Raycast(floorCheck.position, Vector3.down, out hitInfo, 0.1f))
        {
            // �ٴڰ� �浹

        }
        if (isFloor)
        {
            //Debug.Log(gameObject.name+"�� �ٴڿ� ����ֽ��ϴ�.");
            //ani.SetBool("isJump", false);
            //jumpTime = 0;
            //curJump = 0;
            //isJump = false;
        }
        //else
        //{
        //    isJump = true;
        //    curJump++;
        //}

        if (isWall)
        {
            // ���� �浹
            Debug.Log(gameObject.name+"�� ���� ����ֽ��ϴ�.");
        }
    }
}
