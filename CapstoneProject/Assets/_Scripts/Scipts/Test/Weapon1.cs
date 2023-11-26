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

        Animator ani= testPlayer.GetComponent<Animator>();
        if(testPlayer.curCombo>=maxCombo)
        {
            testPlayer.curCombo = maxCombo;
        }
        else
        {
            SetEffectGenerator(0);
            PlayEffect();
            testPlayer.curCombo++;
        }
        testPlayer.ani.SetInteger("AttackCombo",testPlayer.curCombo);
    }

    public override void Skill()
    {
        Debug.Log(_name + " 스킬 사용");
    }
}
