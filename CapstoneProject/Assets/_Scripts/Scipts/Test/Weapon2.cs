using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : BaseWeapon
{
    
    public override void Attack()
    {
        TestPlayer testPlayer = Player.GetComponent<TestPlayer>();
        SetEffectGenerator(testPlayer.curCombo);
        //PlayEffect();
    }

    public override void Skill()
    {
        Debug.Log(_name + " 스킬 사용");
    }
}
