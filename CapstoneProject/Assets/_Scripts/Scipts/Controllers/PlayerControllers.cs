using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

// Players�� ���
public class PlayerControllers : MonoBehaviour
{
    ModeManager modeManager;//���Ŵ��� ��ü
    ModeBase modeA;
    ModeBase modeB;
    ModeBase modeC;


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

    /*=========Attack�Լ� ���� ��1==========*/

    /*=========Attack�Լ� ���� ��1==========*/


    float jumpPressTimer = 0.2f;
    float jumpPressTime = 0;
    bool jump = false;
    // Start is called before the first frame update
    void Start()
    {
        rigid= GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();

        modeManager = GetComponent<ModeManager>();//modeManager�� ������Ʈ�� �޾ƿͼ� �ʱ�ȭ
        modeA = new GreatSwordMode();
        modeB = new DuelBladeMode();
        modeC = new HandCannonMode();

    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.GetIsCanInput())
        {
            PlayerJump();
        }
        //Invoke("ModeChange",modeManager.GetModeDelayTime()); //��庯�� �Լ�
        ModeChange();
        PlayerDash();

        if (Input.GetKeyDown(InputManager.GetAttackKey())) // ���� �Է�üũ
        {
            modeManager.Attack();//��忡 ���� ���� ����
        }
        if (Input.GetKeyDown(InputManager.GetSkillKey()))// ��ų �Է� üũ
        {
            modeManager.UseSkill();//��忡 ���� ��ų ����
        }
    }
    private void FixedUpdate()
    {
        PlayerMovement();
    }

    // ��¡ Ÿ�̸�, �Ű����� �Ŀ� ��¡��
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
                Debug.Log("ª�� ����");
            }
            else
            {
                Debug.Log("��� ����");
            }
            atkDown = false;
            pressAtkTime = 0.0f;
        }
    }

    /*=========Attack�Լ� ���� ��1==========*/
    void Attack()
    {

    }
    /*=========Attack�Լ� ���� ��1==========*/

    //�̵����� �Լ� ������
    void PlayerMovement()
    {
        if (InputManager.GetIsCanInput())
        {
            PlayerVelocity();
            PlayerDirection();
        }
        


    }


    // �÷��̾� ĳ���Ͱ� �ٶ󺸴� ����
    void PlayerDirection()
    {
        // 1 == right, -1 == left

        // ��� 1 ( �� �� �θ� ������ )
        //if (InputManager.GetInputHorizontal() == 1)
        //{
        //    transform.LookAt(Vector3.right*100000);
        //}
        //else if(InputManager.GetInputHorizontal() == -1)
        //{
        //    transform.LookAt(Vector3.left*100000);
        //}

        // ��� 2 ( Ű �Էµ� Vector3���� ���� �ٶ� )
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

    // �÷��̾� ĳ������ �̵�
    void PlayerVelocity()
    {
        rigid.velocity =
            new Vector3(
            InputManager.GetHorizontal() * playerScriptable.moveSpd,
            rigid.velocity.y,
            0);
    }
    // �÷��̾� ĳ������ �뽬
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
                Debug.Log("���� ����");
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
                Debug.Log("���� ����");
            }
        }
    }
    void ModeChange()//��庯�� �޼���
    {
        if (Input.GetKeyDown(InputManager.GetGreatSwordModeKey()))//A��ư �Է��� �޾Ƽ�
        {
            modeManager.SetMode(modeA); // ��˸��� ��ȯ
        }
        else if (Input.GetKeyDown(InputManager.GetDualBladeModeKey()))//S��ư�� �Է¹޾Ƽ�
        {
            modeManager.SetMode(modeB);//�ְ˸��� ��ȯ
        }
        else if (Input.GetKeyDown(InputManager.GetHandCannonKey()))//D��ư�� �Է¹޾Ƽ�
        {
            modeManager.SetMode(modeC);//�Ѹ��� ��ȯ
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