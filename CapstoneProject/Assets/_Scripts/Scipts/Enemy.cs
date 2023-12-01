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
    // 패턴의 지속시간
    protected float patternTimer;
    protected int patternTime;
    // Start is called before the first frame update

    protected GameObject target = null;

    
    /*--Bool 작성해야 함 --*/
    public bool isDamaged { get; set; }
    public bool isDead { get; set; }
    public bool isEnd { get; set; }
    public bool isTrace { get; set; }
    public bool isWall { get;set; }
    public bool isFloor { get; set; }

    [SerializeField]protected float traceTimer = 0;
    protected float traceTime = 0;
    
    /*--Bool 작성해야 함 --*/
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


    // -1 = 왼쪽, 1 = 오른쪽 , 어디를 바라볼지 정하는 함수
    public void Direction()
    {
        if (-90 < transform.rotation.y || transform.rotation.y < 90)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(dir, 0, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpd);
        }
    }

    // 적이 탑지범위에서 나갔을 때 탐지가 종료되는 타이머
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

    // 대상의 x값에 따라 좌, 우로 이동하는 함수
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
    // 벽인지 바닥인지 Ray를 뽜서 확인하는 함수
    protected void CheckWallAndGroundCollision()
    {
        // 플레이어의 위치와 방향을 기반으로 레이캐스트를 발사하여 충돌 검사
        RaycastHit hitInfo;
        isFloor = Physics.Raycast(floorCheck.position, Vector3.down, out hitInfo, 0.1f);
        isWall = Physics.Raycast(wallCheck.position, transform.forward, out hitInfo, 0.5f, w_Layer);
        if (Physics.Raycast(floorCheck.position, Vector3.down, out hitInfo, 0.1f))
        {
            // 바닥과 충돌

        }
        if (isFloor)
        {
            //Debug.Log(gameObject.name+"가 바닥에 닿아있습니다.");
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
            // 벽과 충돌
            Debug.Log(gameObject.name+"가 벽에 닿아있습니다.");
        }
    }
}
