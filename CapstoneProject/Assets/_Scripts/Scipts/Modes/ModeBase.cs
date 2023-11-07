using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModeBase : MonoBehaviour
{
    //public int attackPower = 10;
    //public int skillDamage = 20;
    //public int energyCost = 30;
    //public float attackDelay = 0.5f;

    public abstract void Attack();//����
    public abstract void UseSkill();//��ų
    public abstract void ApplyPassive();//�нú�
    public abstract void OnModeActivated();//�������˸�
    public abstract int GetSkillEnergyCost();//������ �Ҹ� �˾Ƴ���
    public abstract CoreType GetCoreType();// ���� �ھ� Ÿ�� ��ȯ
    public abstract int GetAttackDamage();//���� �Ϲݰ����� ������ ��ȯ
    public abstract int GetSkillDamage();//���� ��ų�� ������ ��ȯ

    public abstract float GetDelayTimer();
}
