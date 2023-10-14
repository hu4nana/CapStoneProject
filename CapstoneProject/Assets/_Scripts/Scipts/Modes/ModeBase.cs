using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModeBase : MonoBehaviour
{
    //public int attackPower = 10;
    //public int skillDamage = 20;
    //public int energyCost = 30;
    //public float attackDelay = 0.5f;

    public abstract void Attack();//공격
    public abstract void UseSkill();//스킬
    public abstract void ApplyPassive();//패시브
    public abstract void OnModeActivated();//모드적용알림
    public abstract int GetSkillEnergyCost();//에너지 소모량 알아내기
    public abstract CoreType GetCoreType();// 현재 코어 타입 반환
    public abstract int GetAttackDamage();//현재 일반공격의 데미지 반환
    public abstract int GetSkillDamage();//현재 스킬의 데미지 반환

}
