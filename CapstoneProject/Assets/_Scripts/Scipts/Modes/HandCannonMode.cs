using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCannonMode : ModeBase
{
    public int attackPower = 10;
    public int skillDamage = 20;
    public int energyCost = 30;
    public float attackDelay = 0.5f;

    public override void Attack()
    {
        Debug.Log("총 일반 공격");
    }
    public override void UseSkill()
    {
        Debug.Log("총 스킬 사용");
    }
    public override void ApplyPassive()
    {
        Debug.Log("총 패시브 적용중");
    }
    public override void OnModeActivated()
    {
        Debug.Log("총 모드가 적용되었습니다.");
        //초기화 작업 필요
    }
    public override int GetSkillEnergyCost()
    {
        return energyCost;
    }

    public override CoreType GetCoreType()
    {
        return CoreType.Saian;
    }
    public override int GetAttackDamage()
    {
        return attackPower;
    }
    public override int GetSkillDamage()
    {
        return skillDamage;
    }
}