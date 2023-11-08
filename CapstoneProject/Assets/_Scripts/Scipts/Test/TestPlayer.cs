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
    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        ani=GetComponentInChildren<Animator>();
        rigid=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerDash();
        PlayerAttack();
        PlayerJump();
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
        if (!isJump && Input.GetKey(KeyCode.V))
        {
            jumpTime += Time.deltaTime;
            jumpPow += 0.2f;
            if (jumpPow >= maxJump)
                jumpPow = maxJump;
            Debug.Log(jumpTime);
            Debug.Log(jumpPow);
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
                transform.Translate(0, 0, 4*Time.deltaTime);
            
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
                Debug.Log("¤±");
                InputManager.SetIsCanInput(true);
                ani.SetBool("isDash", false);
                rigid.useGravity = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        ani.SetBool("isJump", false);
        curJump = 0;
        isJump = false;
    }
}
