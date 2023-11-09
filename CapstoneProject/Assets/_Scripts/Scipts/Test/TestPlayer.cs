using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
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

    ModeManager modeManager;//모드매니저 객체
    ModeBase modeA;
    ModeBase modeB;
    ModeBase modeC;
    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        ani=GetComponentInChildren<Animator>();
        rigid=GetComponent<Rigidbody>();

        modeManager = GetComponent<ModeManager>();//modeManager에 컴포넌트를 받아와서 초기화
        modeA = new GreatSwordMode();
        modeB = new DuelBladeMode();
        modeC = new HandCannonMode();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerDash();
        PlayerAttack();
        PlayerJump();

        ModeChange();
        if (Input.GetKeyDown(InputManager.GetAttackKey())) // 공격 입력체크
        {
            modeManager.Attack();//모드에 따른 공격 실행
        }
        if (Input.GetKeyDown(InputManager.GetSkillKey()))// 스킬 입력 체크
        {
            modeManager.UseSkill();//모드에 따른 스킬 실행
        }
    }

    void PlayerAttack()
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
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
        float minJump = 5;
        float maxJump = 8;
        if(Input.GetKeyDown(KeyCode.V))
        {
            jumpPow = minJump;
        }
        if (!isJump && Input.GetKey(KeyCode.V))
        {
            jumpTime += Time.deltaTime;
            jumpPow += 0.1f;
            Debug.Log(jumpPow);
            if (jumpPow >= maxJump)
                jumpPow = maxJump;
        }
        if (!isJump&&(Input.GetKeyUp(KeyCode.V)||jumpTime>=jumpTimer))
        {
            rigid.AddForce(Vector2.up * jumpPow, ForceMode.Impulse);
            isJump = true;
            ++curJump;
            ani.SetBool("isJump", true);
            jumpTime = 0;
            jumpPow = minJump;
            
        }
        if (isJump)
        {
            if(Input.GetKey(KeyCode.LeftArrow)||
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
                Debug.Log("ㅁ");
                InputManager.SetIsCanInput(true);
                ani.SetBool("isDash", false);
                rigid.useGravity = false;
            }
        }
    }
    void ModeChange()//모드변경 메서드
    {
        if (Input.GetKeyDown(InputManager.GetGreatSwordModeKey()))//A버튼 입력을 받아서
        {
            modeManager.SetMode(modeA); // 대검모드로 변환
        }
        else if (Input.GetKeyDown(InputManager.GetDualBladeModeKey()))//S버튼을 입력받아서
        {
            modeManager.SetMode(modeB);//쌍검모드로 전환
        }
        else if (Input.GetKeyDown(InputManager.GetHandCannonKey()))//D버튼을 입력받아서
        {
            modeManager.SetMode(modeC);//총모드로 변환
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
        ani.SetBool("isJump", false);
        curJump = 0;
        isJump = false;
    }
}
