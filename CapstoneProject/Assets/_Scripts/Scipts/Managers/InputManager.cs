using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;


// 키 할당
public static class InputManager
{
    
    public static Vector3 GetInputMove()
    {
        float horizontal = 0;
        float vertical = 0;
        //Vector3 inputXZ = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            vertical = Input.GetAxisRaw("Vertical");
        }

        //return new Vector3(horizontal, 0, vertical);
        return new Vector3(horizontal, 0, 0);
    }
    public static float GetInputVertical()
    {
        float vertical = 0;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            vertical = Input.GetAxisRaw("Vertical");
        }
        return vertical;
    }
    public static float GetInputHorizontal()
    {
        float horizontal = 0;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        return horizontal;
    }

    // 0이면 NormalAttack, 1이면 ChargingAttack
    public static KeyCode GetAttackKey()
    {
        return KeyCode.X;
    }
    public static KeyCode GetDasyKey()
    {
        return KeyCode.C;
    }
}
