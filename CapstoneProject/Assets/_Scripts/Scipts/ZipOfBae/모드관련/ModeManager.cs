using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    private ModeBase curWeapon; // ���� ��� ���庯��
    private List<ModeBase> modes;
    int curMode = 0;
    private float modeTimer = 0.0f;
    private float startTime = 0.0f;

    void Start()
    {
        //Init();
        modes.Add(new GreatSwordMode());
        modes.Add(new DuelBladeMode());
        modes.Add(new HandCannonMode());
        for(int i = 0; i < modes.Count; i++)
        {
            modes[i].GetComponent<ModeBase>();
        }
    }
    void Update()
    {
        //modes[].ApplyPassive();//�нú�� ��������̶� update �÷��̾� ��ũ��Ʈ�� �Űܵ���
    }
    //public void SetMode(ModeBase mode)//��� �⺻���� �޾ƿͼ� �����Ű�� ��
    //{
    //    modes[0].
    //    modes[] = mode;
    //    modes[].OnModeActivated(); // ��尡 Ȱ��ȭ �Ǿ��ٰ� �˸��� �޼���
    //}

    //public void Attack()//���ݸ޼��� 
    //{
    //    if (startTime != modeTimer)
    //        return;
    //    modes[].Attack();
    //    modeTimer = modes[].GetDelayTimer();
    //}
    //public void UseSkill()//��ų���޼���
    //{
    //    if (modeTimer != 0.0f)
    //        return;
    //    modes[].UseSkill();
    //}

    //public CoreType GetCoreType()//���� �ھ�Ÿ�� ��ȯ
    //{
    //    return modes[].GetCoreType();
    //}

    //public int GetAttackDamage()//���ݵ����� ��ȯ
    //{
    //    return modes[].GetAttackDamage();
    //}
    //public int GetSkillDamage()//��ų������ ��ȯ
    //{
    //    return modes[].GetSkillDamage();
    //}
    //public void Init()
    //{
    //    SetMode(new GreatSwordMode());//�⺻��� ����
    //}

    //public float GetModeDelayTime()
    //{
    //    modeTimer = GetModeDelayTime();
    //    return modes[].GetDelayTimer();
    //}
    //public void SetModeDelayTime(int time)
    //{
    //    modeTimer = time;
    //}
    
    public void SetMode(ModeBase mode)//��� �⺻���� �޾ƿͼ� �����Ű�� ��
    {
        curWeapon = mode;
        curWeapon.OnModeActivated(); // ��尡 Ȱ��ȭ �Ǿ��ٰ� �˸��� �޼���
    }

    public void Attack()//���ݸ޼��� 
    {
        if (startTime != modeTimer)
            return ;
        curWeapon.Attack();
        modeTimer = curWeapon.GetDelayTimer();
    }
    public void UseSkill()//��ų���޼���
    {
        if (modeTimer != 0.0f)
            return;
        curWeapon.UseSkill();
    }

    public CoreType GetCoreType()//���� �ھ�Ÿ�� ��ȯ
    {
        return curWeapon.GetCoreType();
    }

    public int GetAttackDamage()//���ݵ����� ��ȯ
    {
        return curWeapon.GetAttackDamage();
    }
    public int GetSkillDamage()//��ų������ ��ȯ
    {
        return curWeapon.GetSkillDamage();
    }
    public void Init()
    {
        SetMode(new GreatSwordMode());//�⺻��� ����
    }

    public float GetModeDelayTime()
    {
        modeTimer = GetModeDelayTime();
        return curWeapon.GetDelayTimer();
    }
    public void SetModeDelayTime(int time)
    {
        modeTimer = time;
    }
    

}
