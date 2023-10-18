using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Players의 기능
public class PlayerControllers : MonoBehaviour
{
    
    public StatsScriptableObject playerScriptable;

    int dir=1;

    [SerializeField]
    int maxDashCount = 2;
    int curdashCount = 0;
    [SerializeField]
    int maxJumpCount = 1;
    int curjumpCount = 0;

    float dashTimer = 0.2f;
    float dashTime = 0;

    bool isWalk;
    bool dash;
    bool isAtk;

    Animator ani;
    Rigidbody rigid;
    float pressAtkTime;
    bool atkDown = false;

    /*=========Attack함수 제작 중1==========*/
    bool combo = false;
    float comboTime = 0;
    float comboTimer = 3;
    /*=========Attack함수 제작 중1==========*/

    /*=========Jump=========*/
    float jumpPressTimer = 0.2f;
    float jumpPressTime = 0;
    bool jump = false;

    float jumpStartPos;
    float jumpEndPos = 3.5f;
    /*=========Jump=========*/


    // Start is called before the first frame update
    void Start()
    {
        rigid= GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.GetIsCanInput())
        {
            PlayerJump();
            
        }
        
        PlayerDash();
        

    }
    private void FixedUpdate()
    {
        PlayerMovement();
    }

    // 차징 타이머, 매개변수 후에 차징됨
    public void PlayerCharging(float Timer)
    {
        if(Input.GetKeyDown(InputManager.GetAttackKey()))
            atkDown = true;
        if (Input.GetKey(InputManager.GetAttackKey()))
            pressAtkTime += Time.deltaTime;
        if (Input.GetKeyUp(InputManager.GetAttackKey()))
        {
            Debug.Log(pressAtkTime);
            if (pressAtkTime < Timer)
            {
                Debug.Log("짧게 눌림");
            }
            else
            {
                Debug.Log("길게 눌림");
            }
            atkDown = false;
            pressAtkTime = 0.0f;
        }
    }

    /*=========Attack함수 제작 중1==========*/
    void Attack()
    {
        if (Input.GetKeyDown(InputManager.GetAttackKey()))
        {
            combo = true;
            comboTime = 0;
            Debug.Log("combo is True Timer has ticking");
        }
        if (combo == true)
        {
            comboTime += Time.deltaTime;
            Debug.Log(comboTime);
            if (comboTime >= comboTimer ||
                (Input.GetKeyDown(InputManager.GetDashKey())
                || Input.GetKeyDown(InputManager.GetJumpKey())
                || InputManager.GetHorizontal() == 1))
            {
                combo = false;
            }
        }
        if (combo == false)
        {
            Debug.Log("Combo is End");
        }
    }
    /*=========Attack함수 제작 중1==========*/

    //이동관련 함수 정리본
    void PlayerMovement()
    {
        if (InputManager.GetIsCanInput())
        {
            PlayerVelocity();
            PlayerDirection();
        }
        


    }


    // 플레이어 캐릭터가 바라보는 방향
    // dir = 1 right, dir = -1 left
    void PlayerDirection()
    {
        // 1 == right, -1 == left

        // 방법 1 ( 좌 우 로만 움직임 )
        //if (InputManager.GetInputHorizontal() == 1)
        //{
        //    transform.LookAt(Vector3.right*100000);
        //}
        //else if(InputManager.GetInputHorizontal() == -1)
        //{
        //    transform.LookAt(Vector3.left*100000);
        //}

        // 방법 2 ( 키 입력된 Vector3값에 따라 바라봄 )
        //if(InputManager.GetHorizontal()!=0)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(
        //        Vector3.left*InputManager.GetHorizontal());
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * playerScriptable.rotateSpd);
        //    dir=(int)InputManager.GetHorizontal();
        //    ani.SetBool("isWalk", true);
        //}
        //else
        //{
        //    ani.SetBool("isWalk", false);
        //}

        if (InputManager.GetHorizontal() !=0)
        {
            dir = (int)InputManager.GetHorizontal();
            Debug.Log(dir);
            ani.SetInteger("dir", -dir);
            ani.SetBool("isWalk", true);
        }
        else
        {
            ani.SetBool("isWalk", false);
        }
    }

    // 플레이어 캐릭터의 이동
    void PlayerVelocity()
    {
        rigid.velocity =
            new Vector3(
            InputManager.GetHorizontal() * playerScriptable.moveSpd,
            rigid.velocity.y,
            0);
    }
    // 플레이어 캐릭터의 대쉬
    void PlayerDash()
    {
        if (curdashCount<maxDashCount&&
            Input.GetKeyDown(InputManager.GetDashKey()))
        {
            dash = true;
            InputManager.SetIsCanInput(false);
            gameObject.layer = 0;
            rigid.useGravity = false;
            dashTime = 0;
            //rigid.velocity = transform.forward * 3;
            rigid.velocity = new Vector2(dir * playerScriptable.dashPow,0);
            curdashCount++;

        }
        if (dash)
        {
            dashTime += Time.deltaTime;
            Debug.Log(dashTime);
            if (dashTime >= dashTimer)
            {
                rigid.useGravity = true;
                dash = false;
                dashTime = 0;
                curdashCount = 0;
                InputManager.SetIsCanInput(true);
                Debug.Log("DashEnd");
                Debug.Log(rigid.velocity);
                Debug.Log("===================");
            }
        }
    }
    void PlayerJump()
    {
        // Type 1
        if (!jump &&
            curjumpCount < maxJumpCount &&
            Input.GetKey(KeyCode.V))
        {
            jumpPressTime += Time.deltaTime;
        }
        if (jumpPressTime >= jumpPressTimer)
        {
            jumpPressTime = 1;
            if (curjumpCount < maxJumpCount)
            {
                jump = true;
                curjumpCount++;
                //rigid.velocity = Vector2.up * 0;
                rigid.velocity = Vector2.up * playerScriptable.maxJumpPow;
                Debug.Log("높은 점프");
                jumpPressTime = 0;
                if(Input.GetKeyUp(KeyCode.V))
                {
                    rigid.velocity = Vector2.up * 0;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            if (jump)
            {
                jumpPressTime = 0;
                jump = false;
            }
            else if (curjumpCount < maxJumpCount)
            {
                Debug.Log(jumpPressTime);
                curjumpCount++;
                //rigid.velocity = Vector2.up * 0;
                rigid.velocity = Vector2.up * playerScriptable.minJumpPow;
                jumpPressTime = 0;
                jump = false;
                Debug.Log("낮은 점프");
            }
        }

        // Type2
        //if (!jump &&
        //    curjumpCount < maxJumpCount &&
        //    Input.GetKey(KeyCode.V))
        //{
        //    jumpPressTime += Time.deltaTime;

        //    if (jumpPressTime <= jumpPressTimer)
        //    {
        //        jump = true;
        //    }
        //    else
        //    {
        //        jump = false;
        //    }

        //}
        //if (Input.GetKeyUp(KeyCode.V))
        //{
        //    jump = false;
        //}
        //if(jump)
        //{
        //    rigid.velocity = Vector2.up * 10;
        //}
        //if(Input.GetKeyUp(KeyCode.V))
        //{
        //    rigid.velocity = Vector2.up * 0;
        //    jumpPressTime = 0;
        //}


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            rigid.velocity = Vector2.zero;
            curjumpCount = 0;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer== 7)
        {
            curjumpCount = 0;
        }
    }
}