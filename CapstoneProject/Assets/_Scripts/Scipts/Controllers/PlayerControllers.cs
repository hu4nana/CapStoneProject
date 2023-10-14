using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

// Players의 기능
public class PlayerControllers : MonoBehaviour
{
    ModeManager modeManager;//모드매니저 객체

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

    /*=========Attack함수 제작 중1==========*/


    float jumpPressTimer = 0.2f;
    float jumpPressTime = 0;
    bool jump = false;
    // Start is called before the first frame update
    void Start()
    {
        rigid= GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();

        modeManager = GetComponent<ModeManager>();//modeManager에 컴포넌트를 받아와서 초기화
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.GetIsCanInput())
        {
            PlayerJump();
        }
        ModeChange(); //모드변경 함수
        PlayerDash();

        if (Input.GetKeyDown(InputManager.GetAttackKey())) // 공격 입력체크
        {
            modeManager.Attack();//모드에 따른 공격 실행
        }
        if (Input.GetKeyDown(InputManager.GetSkillKey()))// 스킬 입력 체크
        {
            modeManager.UseSkill();//모드에 따른 스킬 실행
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

    /*=========Attack함수 제작 중1==========*/
    void Attack()
    {

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
        else
        {

        }
    }
    void PlayerJump()
    {
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
                rigid.velocity = Vector2.up * 7;
                Debug.Log("높은 점프");
                jumpPressTime = 0;
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
                rigid.velocity = Vector2.up * 5;
                jumpPressTime = 0;
                jump = false;
                Debug.Log("낮은 점프");
            }
        }
    }
    void ModeChange()//모드변경 메서드
    {
        if (Input.GetKeyDown(InputManager.GetGreatSwordModeKey()))//A버튼 입력을 받아서
        {
            modeManager.SetMode(new GreatSwordMode()); // 대검모드로 변환
        }
        else if (Input.GetKeyDown(InputManager.GetDualBladeModeKey()))//S버튼을 입력받아서
        {
            modeManager.SetMode(new DuelBladeMode());//쌍검모드로 전환
        }
        else if (Input.GetKeyDown(InputManager.GetHandCannonKey()))//D버튼을 입력받아서
        {
            modeManager.SetMode(new HandCannonMode());//총모드로 변환
        }
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