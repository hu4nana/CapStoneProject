using CharacterController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon1 : BaseWeapon
{
    
    public override void Attack()
    {
        Debug.Log(_name + " ���� ���");
    }

    public override void Skill()
    {
        Debug.Log(_name + " ��ų ���");
    }
}
