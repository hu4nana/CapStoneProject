using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    Animator ani;
    Rigidbody rigid;
    float dir;

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

    }


    void PlayerMove()
    {
        //if (Input.GetKeyDown(KeyCode.LeftArrow)||
        //    Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    if(InputManager.GetHorizontal()!=dir)
        //    {
        //        ani.SetBool("isTurn", true);
        //    }
        //    Debug.Log("KeyDown");
        //}
        if (Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            ani.SetBool("isWalk", true);
            Debug.Log("Key");
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.RightArrow))
        {
            ani.SetBool("isWalk", false);
            Debug.Log("KeyUp");
        }
        if (InputManager.GetHorizontal() != 0)
        {
            Quaternion targetrotation = Quaternion.LookRotation(
                Vector3.right * InputManager.GetHorizontal());
            transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, Time.deltaTime * 90);
        }
        
        ////ani.SetFloat("xDir", InputManager.GetMove().x);
        ////ani.SetFloat("yDir", InputManager.GetMove().y);
        //ani.SetFloat("Blend", dir);
    }
}
