using CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : BaseWeapon
{
    
    public override void Attack()
    {
        Debug.Log(_name + " 공격 중");
    }

    public override void Skill()
    {
        Debug.Log(_name + " 스킬 사용");
    }
}
