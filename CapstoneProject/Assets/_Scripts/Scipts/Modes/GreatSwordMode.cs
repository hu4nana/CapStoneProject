using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSwordMode : ModeBase
{
    // Start is called before the first frame update
    public int attackPower = 10;
    public int skillDamage = 20;
    public int energyCost = 30;
    public float attackDelay = 0.5f;
    public float modeDelay = 0.0f;

    public override void Attack()
    {
        Debug.Log("��˽�ų �Ϲ� ����");
    }
    public override void UseSkill()
    {
        Debug.Log("��˽�ų ���");
    }
    public override void ApplyPassive()
    {
        Debug.Log("����нú� ������");
    }
    public override void OnModeActivated()
    {
        Debug.Log("��� ��尡 ����Ǿ����ϴ�.");
        //�ʱ�ȭ �۾� �ʿ�
    }
    public override int GetSkillEnergyCost()
    {
        return energyCost;
    }

    public override CoreType GetCoreType()
    {
        return CoreType.Magenta;
    }

    public override int GetAttackDamage()
    {
        return attackPower;
    }

    public override int GetSkillDamage()
    {
        return skillDamage;
    }

    public override float GetDelayTimer()
    {
        return modeDelay;
    }
}
