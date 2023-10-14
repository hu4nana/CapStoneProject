using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelBladeMode : ModeBase
{
    public int attackPower = 10;
    public int skillDamage = 20;
    public int energyCost = 30;
    public float attackDelay = 0.5f;

    public override void Attack()
    {
        Debug.Log("쌍검 일반 공격");
    }
    public override void UseSkill()
    {
        Debug.Log("쌍검 스킬 사용");
    }
    public override void ApplyPassive()
    {
        Debug.Log("쌍검 패시브 적용중");
    }
    public override void OnModeActivated()
    {
        Debug.Log("쌍검 모드가 적용되었습니다.");
        //초기화 작업 필요
    }
    public override int GetSkillEnergyCost()
    {
        return energyCost;
    }

    public override CoreType GetCoreType()
    {
        return CoreType.Yellow;
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