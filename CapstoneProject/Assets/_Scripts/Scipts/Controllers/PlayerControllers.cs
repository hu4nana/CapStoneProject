using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Players의 기능
public class PlayerControllers : MonoBehaviour
{
    
    public StatsScriptableObject playerScriptable;

    int dir=1;

    int maxDashCount = 2;
    int curdashCount = 0;
    int maxJumpCount = 2;
    int curjumpCount = 0;
    float pressJumpTimer = 0.3f;
    float pressJumpTime = 0;

    float dashTimer = 0.2f;
    float dashTime = 0;

    bool isWalk;
    bool dash;
    bool jump;
    bool isAtk;

    Animator ani;
    Rigidbody rigid;
    float pressAtkTime;
    bool atkDown = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid= GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpChecker();
        DashChecker();
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(Input.GetKeyUp(KeyCode.V))
            {
                Debug.Log("afd");
            }
            if(Input.GetKey(KeyCode.V)) {
                Debug.Log("afdasdfsfad");
            }
        }
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

    

    //이동관련 함수 정리본
    void PlayerMovement()
    {
        if (InputManager.GetIsCanInput())
        {
            PlayerVelocity();
            PlayerDirection();
            PlayerJump();
        }
        PlayerDash();


    }


    // 플레이어 캐릭터가 바라보는 방향
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
        if(InputManager.GetHorizontal()!=0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(
                Vector3.left*InputManager.GetHorizontal());
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * playerScriptable.rotateSpd);
            dir=(int)InputManager.GetHorizontal();
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
    void DashChecker()
    {
        if (Input.GetKeyDown(InputManager.GetDashKey()))
            dash = true;
    }
    // 플레이어 캐릭터의 대쉬
    void PlayerDash()
    {
        if (curdashCount<maxDashCount&&dash)
        {
            InputManager.SetIsCanInput(false);
            gameObject.layer = 0;
            rigid.useGravity = false;
            //rigid.velocity = transform.forward * 3;
            rigid.velocity = new Vector2(dir * 7,0);
            curdashCount++;
            Debug.Log("DashStart");
            Debug.Log(dash);
            Debug.Log(rigid.velocity);

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

    void JumpChecker()
    {
        if (Input.GetKeyDown(InputManager.GetJumpKey()))
            jump = true;
        if(Input.GetKeyUp(InputManager.GetJumpKey()))
            jump = false;
    }

    // 플레이어 캐릭터의 점프
    void PlayerJump()
    {
        //bool jump = Input.GetKey(InputManager.GetJumpKey());
       
        if (jump && curjumpCount < maxJumpCount)
        {
            pressJumpTime += Time.deltaTime;
            if (Input.GetKeyUp(InputManager.GetJumpKey()))
            {
                Debug.Log(pressJumpTime);
                Debug.Log("짧게 눌림");
                Jump(5);
            }
            if (pressJumpTime >= pressJumpTimer)
            {
                Debug.Log(pressJumpTime);
                Debug.Log("길게 눌림");
                Jump(7);

            }
            
        }
        //if (!jump && curjumpCount < maxJumpCount)
        //{
        //    if (Input.GetKey(InputManager.GetJumpKey()))
        //        pressJumpTime += Time.deltaTime;
        //    if (pressJumpTime >= pressJumpTimer)
        //    {
        //        Debug.Log("길게 눌림");
        //        Jump(6);

        //    }
        //    else if (Input.GetKeyUp(InputManager.GetJumpKey()))
        //    {
        //        Jump(3);
        //        Debug.Log("짧게 눌림");
        //    }
        //    Debug.Log(pressJumpTime);
        //}
        
    }
    void Jump(int value)
    {
        if (!dash)
        {
            rigid.velocity = Vector2.up * 0;
            rigid.velocity = Vector2.up * value;
            pressJumpTime = 0;
            jump = false;
            curjumpCount++;
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