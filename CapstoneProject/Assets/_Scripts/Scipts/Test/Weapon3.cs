using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3 : BaseWeapon
{
    [SerializeField] GameObject bullet;
    public override void Attack()
    {
        TestPlayer testPlayer = Player.GetComponent<TestPlayer>();
        //if(testPlayer.curCombo>=maxCombo)
        //{
        //    testPlayer.curCombo = maxCombo;
        //}
        //else
        //{
            
        //    SetEffectGenerator(testPlayer.curCombo);
        //    PlayEffect();
        //    testPlayer.curCombo++;
        //}
        //testPlayer.ani.SetInteger("AttackCombo", testPlayer.curCombo);
        SetEffectGenerator(testPlayer.curCombo);
        PlayEffect();
    }

    public override void Skill()
    {
        Debug.Log(_name + " 스킬 사용");
    }
}
