using CharacterController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon1 : BaseWeapon
{
    
    public override void Attack()
    {
        TestPlayer testPlayer = Player.GetComponent<TestPlayer>();
        SetEffectGenerator(testPlayer.curCombo);
    }

    public override void Skill()
    {
        Debug.Log(_name + " 스킬 사용");
    }
}
