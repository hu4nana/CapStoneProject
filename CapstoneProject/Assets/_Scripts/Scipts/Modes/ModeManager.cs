using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    private ModeBase currentMode; // ���� ��� ���庯��
    private float modeTimer = 0.0f;
    private float startTime = 0.0f;

    void Start()
    {
        Init();
    }
    void Update()
    {
        //currentMode.ApplyPassive();//�нú�� ��������̶� update �÷��̾� ��ũ��Ʈ�� �Űܵ���
    }
    public void SetMode(ModeBase mode)//��� �⺻���� �޾ƿͼ� �����Ű�� ��
    {
        currentMode = mode;
        currentMode.OnModeActivated(); // ��尡 Ȱ��ȭ �Ǿ��ٰ� �˸��� �޼���
    }

    public void Attack()//���ݸ޼��� 
    {
        if (startTime != modeTimer)
            return ;
        currentMode.Attack();
        modeTimer = currentMode.GetDelayTimer();
    }
    public void UseSkill()//��ų���޼���
    {
        if (modeTimer != 0.0f)
            return;
        currentMode.UseSkill();
    }

    public CoreType GetCoreType()//���� �ھ�Ÿ�� ��ȯ
    {
        return currentMode.GetCoreType();
    }

    public int GetAttackDamage()//���ݵ����� ��ȯ
    {
        return currentMode.GetAttackDamage();
    }
    public int GetSkillDamage()//��ų������ ��ȯ
    {
        return currentMode.GetSkillDamage();
    }
    public void Init()
    {
        SetMode(new GreatSwordMode());//�⺻��� ����
    }

    public float GetModeDelayTime()
    {
        modeTimer = GetModeDelayTime();
        return currentMode.GetDelayTimer();
    }
    public void SetModeDelayTime(int time)
    {
        modeTimer = time;
    }

}
