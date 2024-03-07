using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Ű �Ҵ�
public static class InputManager
{
    public static bool isCanInput = true;

    public static void SetIsCanInput(bool value)
    {
        isCanInput = value;
    }
    public static bool GetIsCanInput()
    {
        return isCanInput;
    }
    public static Vector3 GetMove()
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

        return new Vector3(horizontal, 0, vertical);
        //return new Vector3(horizontal, 0, 0);
        
    }
    public static float GetVertical()
    {
        float vertical = 0;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            vertical = Input.GetAxisRaw("Vertical");
        }
        return vertical;
    }
    public static float GetHorizontal()
    {
        float horizontal = 0;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        return horizontal;
    }

    // 0�̸� NormalAttack, 1�̸� ChargingAttack
    public static KeyCode GetAttackKey()
    {
        return KeyCode.X;
    }
    public static KeyCode GetDashKey()
    {
        return KeyCode.C;
    }
    public static KeyCode GetJumpKey()
    {
        return KeyCode.Z;
    }
    public static KeyCode GetSkillKey()
    {

        return KeyCode.V;
    }
    public static KeyCode GetGreatSwordModeKey()
    {

        return KeyCode.A;
    }

    public static KeyCode GetDualBladeModeKey()
    {

        return KeyCode.S;
    }

    public static KeyCode GetHandCannonKey()
    {

        return KeyCode.D;
    }
}
