using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    Animator ani;
    Rigidbody rigid;
    //dir = 1 right, dir = -1 left
    float dir = 1;

    // Start is called before the first frame update
    void Start()
    {
        ani=GetComponentInChildren<Animator>();
        rigid=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerDash();
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
        if (Input.GetKeyDown(KeyCode.X)){
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
                rigid.useGravity = false;
            }
        }
    }
}
