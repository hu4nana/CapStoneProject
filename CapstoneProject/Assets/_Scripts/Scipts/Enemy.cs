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
    public StatsScriptableObject enemyScriptable;


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
    protected bool damaged = false;
    protected bool dead = false;
    protected bool isEnd = false;
    protected bool isTrace = false;
    protected float traceTimer = 0;
    protected float traceTime = 0;
    protected bool isWall;
    protected bool isFloor;
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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * enemyScriptable.rotateSpd);
        }
    }

    // 적을 쫓는 타이머
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
        if (damaged)
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
            rigid.velocity = new Vector2(dir * enemyScriptable.moveSpd * value, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
    }
    public bool GetDead()
    {
        return dead;
    }
    public void SetDead(bool value)
    {
        dead = value;
    }
    public bool GetDamaged()
    {
        return damaged;
    }
    public void SetDamaged(bool value)
    {
        damaged=value;
    }

    // 벽인지 확인하는 함수
    protected void WallCheck()
    {
        if (this.transform.rotation.y>0)
            isWall =
            Physics.Raycast(wallCheck.position, Vector2.right, 1.5f, w_Layer);
        else if(this.transform.rotation.y<0)
            isWall =
            Physics.Raycast(wallCheck.position, Vector2.left, 1, w_Layer);

        //if (isWall)
        //{
        //    Debug.Log("WallCheck!");
        //}
    }

    // 바닥인지 확인하는 함수
    protected void FloorCheck()
    {
        isFloor =
            (Physics.Raycast(floorCheck.position, Vector2.down,
            1, f_Layer));
        //if (isFloor)
        //{
        //    Debug.Log("FloorCheck!");
        //}             
    }
}
