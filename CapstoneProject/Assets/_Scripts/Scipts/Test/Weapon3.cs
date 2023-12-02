using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3 : BaseWeapon
{
    [SerializeField] GameObject bullet;
    public override void Attack()
    {
        TestPlayer testPlayer = Player.GetComponent<TestPlayer>();
        SetEffectGenerator(testPlayer.curCombo);
        Instantiate(bullet, transform.position,Quaternion.Euler(0,testPlayer.transform.rotation.y,0));
        PlayEffect();
    }

    public override void Skill()
    {
        Debug.Log(_name + " 스킬 사용");
    }
}
