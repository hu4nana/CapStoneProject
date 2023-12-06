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
    public float Dir { get { return dir; } }

    //dir = 1 right, dir = -1 left
    float dir = 1;
    public int curCombo { get; set; }
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
        PlayerMove();
        PlayerDash();
        PlayerJump();
        WeaponChange();
        PlayerWeaponAttack();
        CheckWallAndGroundCollision();
        //ModeChange();
    }

    public void PlayerEffect() { 
        weaponManager.PlayEffect();
    }
    public void WeaponSetActiveTrue()
    {
        weaponManager.SetActive(true);
    }
    public void WeaponSetActiveFalse()
    {
        weaponManager.SetActive(false);
    }
    void PlayerWeaponAttack()
    {
        if (isFloor&&Input.GetKeyDown(KeyCode.X))
        {
            ani.SetBool("isAttack", true);
            //ani.Play("Attack_0");
            isAttack = true;
            
            transform.rotation = Quaternion.Euler(0, 90 * dir, 0);
            Debug.Log("=============================="+curCombo+"==========================================");
            if (curCombo >= weaponManager.MaxCombo)
            {
                curCombo = weaponManager.MaxCombo;
                
            }
            else
            {
                //if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack_" + (curCombo-1)))
                //{
                //    weaponManager.PlayedEffect = false;
                //}
                ani.SetBool("NextCombo",true);
                weaponManager.WeaponAttack();
                curCombo++;
                ani.SetInteger("AttackCombo", curCombo);
            }
            //ani.SetInteger("AttackCombo", curCombo);
        }
        
        if(isAttack&&!(isJump||isDash))
        {
            ani.SetBool("AttackEnd", ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= weaponManager.AttackEndTime);
            if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.1f)
            {
                ani.SetBool("NextCombo", false);
            }
            if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >=
                ani.GetFloat("MotionTime"))
            {
                isAttack = false;
                ani.SetBool("isAttack", false);
                //InputManager.SetIsCanInput(true);
            }
        }
        else
        {
            weaponManager.SetActive(false);
            curCombo = 0;
            //ani.StopPlayback();
            ani.SetInteger("AttackCombo", curCombo);
            ani.SetBool("NextCombo", false);
            ani.SetBool("AttackEnd", false);
        }
        //ani.SetBool("isAttack", isAttack);
        //ani.SetInteger("AttackCombo", curCombo);

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
            rigid.velocity = jumpPow;
        }
        if (jumpTime <= jumpTimer&&!isDash&&Input.GetKey(InputManager.GetJumpKey()))
        {
            rigid.velocity = jumpPow;
            jumpTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(InputManager.GetJumpKey()))
        {
            rigid.velocity=rigid.velocity;
            jumpTime = 0;
        }
        if (!isWall&&rigid.velocity.y!=0)
        {
            if (Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.RightArrow))
                transform.Translate(0, 0, 4 * Time.deltaTime);
        }
        ani.SetBool("isJump", isJump);
    }
    void PlayerMove()
    {
        if (!isAttack&&InputManager.GetIsCanInput()&& (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.RightArrow)))
        {
            if(InputManager.GetHorizontal()!=0)
                dir=InputManager.GetHorizontal();
            ani.SetBool("isWalk", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(
     Vector3.right * dir), Time.deltaTime * 24);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            if(Input.GetKeyDown(InputManager.GetAttackKey()))
            {
                //ani.SetBool("isWalk", false);
                //InputManager.SetIsCanInput(false);
                isAttack = true;
                ani.SetBool("isAttack", true);
                return;
            }
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
            gameObject.layer = 12;
            rigid.useGravity = false;
            InputManager.SetIsCanInput(false);
        }
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Dash")){

            if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                //Debug.Log("��");
                InputManager.SetIsCanInput(true);
                gameObject.layer = 10;
                isDash = false;
                rigid.useGravity = true;
            }
        }
        ani.SetBool("isDash", isDash);

    }

    void WeaponChange()
    {
        if (!isAttack)
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
                weaponManager.ChangeWeapon(weaponManager.weapons[2]);
            }
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
            //Debug.Log("�÷��̾ �ٴڿ� ����ֽ��ϴ�.");
            ani.SetBool("isJump", false);
            jumpTime = 0;
            curJump = 0;
            isJump = false;
        }
        else
        {
            isJump = true;
            curJump++;
        }
        
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.5f))
        {
            // ���� �浹
            //Debug.Log("�÷��̾ ���� ����ֽ��ϴ�.");
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
