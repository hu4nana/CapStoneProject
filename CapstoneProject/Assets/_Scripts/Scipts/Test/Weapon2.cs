using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : BaseWeapon
{
    
    public override void Attack()
    {
        TestPlayer testPlayer = Player.GetComponent<TestPlayer>();

        testPlayer.isAttack = true;
        testPlayer.curCombo++;
        testPlayer.ani.SetInteger("AttackCombo", (int)testPlayer.curCombo);
    }

    public override void Skill()
    {
        Debug.Log(_name + " 스킬 사용");
    }
}
