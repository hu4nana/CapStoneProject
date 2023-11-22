using CharacterController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon1 : BaseWeapon
{
    
    public override void Attack()
    {
        Debug.Log(_name + " 공격 사용");
    }

    public override void Skill()
    {
        Debug.Log(_name + " 스킬 사용");
    }
}
