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
        bullet.transform.localPosition = effectPos[0];
        bullet.transform.rotation = testPlayer.transform.rotation;
        Instantiate(bullet);
        //PlayEffect();
    }

    public override void Skill()
    {
        Debug.Log(_name + " 스킬 사용");
    }
}
