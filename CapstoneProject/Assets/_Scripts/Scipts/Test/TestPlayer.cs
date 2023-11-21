using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] float maxHp;
    [SerializeField] float maxMp;
    public float curHp { get; set; }
    public float curMp { get; set; }
    ModeType mode = 0;

    bool isWall = false;
    bool isFloor = false;
    [SerializeField]
    protected Transform floorCheck;
    [SerializeField]
    protected Transform wallCheck;
    [SerializeField]
    protected LayerMask w_Layer;
    [SerializeField]
    protected LayerMask f_Layer;


    Animator ani;
    Rigidbody rigid;
    WeaponManager weaponManager;
    //dir = 1 right, dir = -1 left
    float dir = 1;
    float curCombo = 0;
    bool isAttack = false;
    bool isJump = false;
    float jumpTime=0;
    float jumpPow;
    int maxJump=1;
    int curJump=0;

    /* ���ش� �ڵ� ���� */
    //ModeManager modeManager;//���Ŵ��� ��ü
    //ModeBase modeA;
    //ModeBase modeB;
    //ModeBase modeC;
    //void ModeChange()//��庯�� �޼���
    //{
    //    if (Input.GetKeyDown(InputManager.GetGreatSwordModeKey()))//A��ư �Է��� �޾Ƽ�
    //    {
    //        modeManager.SetMode(modeA); // ��˸��� ��ȯ
    //    }
    //    else if (Input.GetKeyDown(InputManager.GetDualBladeModeKey()))//S��ư�� �Է¹޾Ƽ�
    //    {
    //        modeManager.SetMode(modeB);//�ְ˸��� ��ȯ
    //    }
    //    else if (Input.GetKeyDown(InputManager.GetHandCannonKey()))//D��ư�� �Է¹޾Ƽ�
    //    {
    //        modeManager.SetMode(modeC);//�Ѹ��� ��ȯ
    //    }
    //}
    /* ���ش� �ڵ� ���� */


    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        ani=GetComponentInChildren<Animator>();
        rigid=GetComponent<Rigidbody>();
        weaponManager.ChangeWeapon(weaponManager.weapons[0]);
        curHp = maxHp;
        curMp = maxMp;
        //modeManager = GetComponent<ModeManager>();//modeManager�� ������Ʈ�� �޾ƿͼ� �ʱ�ȭ
        /* ���ش� �ڵ� ���� */
        //modeA = new GreatSwordMode();
        //modeB = new DuelBladeMode();
        //modeC = new HandCannonMode();
        /* ���ش� �ڵ� ���� */
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerDash();
        PlayerAttack();
        PlayerJump();
        WeaponChange();
        CheckWallAndGroundCollision();
        //ModeChange();
        //if (Input.GetKeyDown(InputManager.GetAttackKey())) // ���� �Է�üũ
        //{
        //    modeManager.Attack();//��忡 ���� ���� ����
        //}
        //if (Input.GetKeyDown(InputManager.GetSkillKey()))// ��ų �Է� üũ
        //{
        //    modeManager.UseSkill();//��忡 ���� ��ų ����
        //}
    }

    void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            switch (weaponManager.Weapon.Core)
            {
                case CoreType.Yellow: break;
                case CoreType.Magenta: break;
                case CoreType.Saian: break;

            }

            weaponManager.WeaponAttack();
            transform.rotation = Quaternion.Euler(0, 90 * dir, 0);
            isAttack = true;
            curCombo++;
            ani.SetBool("isAttack", isAttack);
            ani.SetInteger("AttackCombo", (int)curCombo);
            
            //ani.SetFloat("curCombo", curCombo);
        }
        //if (ani.GetCurrentAnimatorStateInfo(0).IsName("AttackTree")&&
        //    ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        //{
        //    ani.SetBool("isAttack", false);
        //    ani.SetFloat("curCombo", 0);
        //    curCombo = 0;
        //}

        if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f)
        {
            ani.SetBool("isAttack", false);
            ani.SetInteger("AttackCombo", 0);
            curCombo = 0;
        }
        

    }
    void PlayerJump()
    {
        float jumpTimer = 0.4f;
        //float minJump = 5;
        //float maxJump = 8;
        //if(Input.GetKeyDown(KeyCode.V))
        //{
        //    jumpPow = minJump;
        //}
        //if (!isJump && Input.GetKey(KeyCode.V))
        //{
        //    jumpTime += Time.deltaTime;
        //    jumpPow += 0.1f;
        //    Debug.Log(jumpPow);
        //    if (jumpPow >= maxJump)
        //        jumpPow = maxJump;
        //}
        //if (!isJump&&(Input.GetKeyUp(KeyCode.V)||jumpTime>=jumpTimer))
        //{
        //    rigid.AddForce(Vector2.up * jumpPow, ForceMode.Impulse);
        //    isJump = true;
        //    ++curJump;
        //    ani.SetBool("isJump", true);
        //    jumpTime = 0;
        //    jumpPow = minJump;

        //}
        //if (isJump)
        //{
        //    if(Input.GetKey(KeyCode.LeftArrow)||
        //        Input.GetKey(KeyCode.RightArrow))
        //        transform.Translate(0, 0, 4 * Time.deltaTime);

        //}
        Vector3 jumpPow = new Vector3(rigid.velocity.x, 6, rigid.velocity.z);
        if (Input.GetKeyDown(InputManager.GetJumpKey()))
        {
            isJump = true;
            rigid.velocity = jumpPow;
            ani.SetBool("isJump", true);
        }
        if (isJump&&Input.GetKey(InputManager.GetJumpKey()))
        {
            rigid.velocity = jumpPow;
            jumpTime += Time.deltaTime;
            if(jumpTime >jumpTimer)
                isJump = false;
        }
        if (Input.GetKeyUp(InputManager.GetJumpKey()))
        {
            rigid.velocity=rigid.velocity;
            isJump = false;
            jumpTime = 0;
        }
        if (rigid.velocity.y!=0&&!isWall)
        {
            if (Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.RightArrow))
                transform.Translate(0, 0, 4 * Time.deltaTime);
        }
    }
    void PlayerMove()
    {
        if (InputManager.GetIsCanInput()&&InputManager.GetHorizontal() != 0)
        {
            dir=InputManager.GetHorizontal();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(
     Vector3.right * dir), Time.deltaTime * 24);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            ani.SetBool("isWalk", true);
        }
        if (InputManager.GetHorizontal() == 0)
        {
            ani.SetBool("isWalk", false);

        }
    }
    void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.C)){
            ani.SetBool("isDash", true);
            //gameObject.layer = 0;
            rigid.useGravity = false;
            InputManager.SetIsCanInput(false);
        }
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Dash")){
            if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                Debug.Log("��");
                InputManager.SetIsCanInput(true);
                ani.SetBool("isDash", false);
                rigid.useGravity = true;
            }
        }
    }

    void WeaponChange()
    {
        if (Input.GetKeyDown(InputManager.GetGreatSwordModeKey()))//A��ư �Է��� �޾Ƽ�
        {
            weaponManager.ChangeWeapon(weaponManager.weapons[0]);
        }
        else if (Input.GetKeyDown(InputManager.GetDualBladeModeKey()))//S��ư�� �Է¹޾Ƽ�
        {
            weaponManager.ChangeWeapon(weaponManager.weapons[1]);
        }
        else if (Input.GetKeyDown(InputManager.GetHandCannonKey()))//D��ư�� �Է¹޾Ƽ�
        {
            weaponManager.ChangeWeapon(weaponManager.weapons[0]);
        }
    }
    void CheckWallAndGroundCollision()
    {
        // �÷��̾��� ��ġ�� ������ ������� ����ĳ��Ʈ�� �߻��Ͽ� �浹 �˻�
        RaycastHit hitInfo;
        isFloor = Physics.Raycast(floorCheck.position, Vector3.down, out hitInfo, 0.5f,f_Layer);
        isWall = Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.5f,w_Layer);
        if (Physics.Raycast(floorCheck.position, Vector3.down, out hitInfo, 0.1f))
        {
            // �ٴڰ� �浹
            Debug.Log("�÷��̾ �ٴڿ� ����ֽ��ϴ�.");
        }

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.5f))
        {
            // ���� �浹
            Debug.Log("�÷��̾ ���� ����ֽ��ϴ�.");
        }
    }
    public interface IEffect
    {
        public void PlayComboAttackEffects();
        public void PlayDashEffect();
        public void PlaySkillEffect();
    }
    public interface ISount
    {
        public void PlayComboAttackSound();
        public void PlayDashSount();
        public void PlaySkillSound();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isFloor)
        {
            ani.SetBool("isJump", false);
            curJump = 0;
            isJump = false;
        }
    }
}
