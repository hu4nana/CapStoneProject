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


    public Animator ani { get; set; }
    Rigidbody rigid;
    WeaponManager weaponManager;
    //dir = 1 right, dir = -1 left
    float dir = 1;
    public float curCombo { get; set; }
    public bool isDash { get; set; }
    public bool isAttack {get;set;}
    public bool isJump { get; set; }
    bool isAttacking;
    float jumpTime=0;
    int maxJump=0;
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
        PlayerAttack();
        PlayerMove();
        PlayerDash();
        PlayerJump();
        WeaponChange();
        CheckWallAndGroundCollision();
        //ModeChange();
    }

    void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isAttack = true;
            weaponManager.WeaponAttack();
            transform.rotation = Quaternion.Euler(0, 90 * dir, 0);
            //isAttack = true;
            //curCombo++;

            //ani.SetInteger("AttackCombo", (int)curCombo);


        }
        if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >=
                weaponManager.NormalizedTime||(isJump||isDash))
        {
            isAttack = false;

            ani.SetInteger("AttackCombo", 0);
            curCombo = 0;
        }
        InputManager.SetIsCanInput(!isAttack);
        ani.SetBool("isAttack", isAttack);
        //if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        //{
        //    isAttack = false;

        //    ani.SetInteger("AttackCombo", 0);
        //    curCombo = 0;
        //}
        //ani.SetBool("isAttack", isAttack);


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
        if (!isDash&&Input.GetKeyDown(InputManager.GetJumpKey())&&curJump<=maxJump)
        {
            isJump = true;
            rigid.velocity = jumpPow;
            curJump++;
            ani.SetBool("isJump", true);
        }
        if (isJump&&!isDash&&Input.GetKey(InputManager.GetJumpKey()))
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
        if (!isWall&&rigid.velocity.y!=0)
        {
            if (Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.RightArrow))
                transform.Translate(0, 0, 4 * Time.deltaTime);
        }
    }
    void PlayerMove()
    {
        if (!isAttack&&InputManager.GetIsCanInput()&&InputManager.GetHorizontal() != 0)
        {
            dir=InputManager.GetHorizontal();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(
     Vector3.right * dir), Time.deltaTime * 24);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            ani.SetBool("isWalk", true);
        }
        if (isAttack||InputManager.GetHorizontal() == 0)
        {
            ani.SetBool("isWalk", false);
        }
    }
    void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.C)){
            isDash = true;
            
            rigid.velocity = Vector3.zero;
            //gameObject.layer = 0;
            rigid.useGravity = false;
            InputManager.SetIsCanInput(false);
        }
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Dash")){

            if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                //Debug.Log("��");
                InputManager.SetIsCanInput(true);
                isDash = false;
                rigid.useGravity = true;
            }
        }
        ani.SetBool("isDash", isDash);

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
        isFloor = Physics.Raycast(floorCheck.position, Vector3.down, out hitInfo, 0.1f);
        isWall = Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.5f,w_Layer);
        if (Physics.Raycast(floorCheck.position, Vector3.down, out hitInfo, 0.1f))
        {
            // �ٴڰ� �浹
            
        }
        if (isFloor)
        {
            Debug.Log("�÷��̾ �ٴڿ� ����ֽ��ϴ�.");
            ani.SetBool("isJump", false);
            curJump = 0;
            isJump = false;
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
        //if (isFloor)
        //{
        //    ani.SetBool("isJump", false);
        //    curJump = 0;
        //    isJump = false;
        //}
    }
}