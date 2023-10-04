using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
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
    protected bool isDamaged = false;
    protected bool isDead = false;
    protected bool isEnd = false;
    protected bool isTrace = false;
    protected float traceTimer = 0;
    protected int traceTime = 0;
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
    public void Direction()
    {
        if (-90<transform.rotation.y||transform.rotation.y<90)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(dir, 0, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * enemyScriptable.rotateSpd);
        }
    }
    public bool GetIsDead()
    {
        return isDead;
    }
    public void SetIsDead(bool value)
    {
        isDead = value;
    }
    public bool GetIsDamaged()
    {
        return isDamaged;
    }
    public void SetIsDamaged(bool value)
    {
        isDamaged=value;
    }
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
