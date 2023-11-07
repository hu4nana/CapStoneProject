using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedEnemy : Enemy
{
    //Collider coreCollider;
    int cur_BreakPoint;       //���� �׷α������
    int max_BreakPoint = 100; //�ִ� �׷α������

    int cur_CoreCount = 0;    //���� �ھ�ī��Ʈ
    int max_CoreCount = 3;    //�ִ� �ھ�ī��Ʈ

    int cur_Monster_Hp;       //����ü��
    int max_Monster_Hp = 100; //���� �ִ� ü��

    bool isMonsterAlive;      //���� ����ִ��� üũ

    Core[] core = new Core[3]; //�ھ����


    private void Update()
    {
        IsCoreDead(core[0]);
        IsCoreDead(core[1]);
        IsCoreDead(core[2]);

        if (cur_BreakPoint == 0)
        {
            Monster_Break();
        }
    }


    private void Init()
    {
        //coreCollider.GetComponent<Collider>();
        foreach (Core cores in core)
        {
            //cores.GetComponent<Core>().Init();
            cores.Init();
        }
        core[0].Set_CoreType(CoreType.Magenta);
        core[1].Set_CoreType(CoreType.Yellow);
        core[2].Set_CoreType(CoreType.Saian);

        cur_BreakPoint = max_BreakPoint;
        isMonsterAlive = true;
    }


    public int GetCurBreakPoint()//���� �׷α� ������ ��ȯ
    {
        return cur_BreakPoint;
    }
    public int GetMaxBreakPoint()//�ִ� �׷α� ������ ��ȯ
    {
        return max_BreakPoint;
    }
    public void SetCurBreakPoint(int breakPoint)//��������BreakPoint ����
    {
        cur_BreakPoint = breakPoint;
    }
    public void SetMaxBreakPoint(int maxBreakPoint)//���� �ִ� BreakPoint����
    {
        max_BreakPoint = maxBreakPoint;
    }
    private void IsCoreDead(Core core)//�ھ��׾����� üũ
    {
        if (core.GetCoreAlive())
        {
            cur_BreakPoint -= 25;
        }
        cur_CoreCount -= 1;
    }
    private void ReviveCore()//�ھ� 3�� ��Ȱ
    {
        if (cur_CoreCount <= 0)
        {
            core[0].CoreRevive();
            core[1].CoreRevive();
            core[2].CoreRevive();

            cur_CoreCount = max_CoreCount;
            max_BreakPoint = 100;
        }
    }
    private void Monster_Break()//�׷α����, �극��ũ����
    {
        Debug.Log("���Ͱ� �극��ũ���¿� �������ϴ�!");
    }
    private void MonsterDead()
    {
        Debug.Log("���� ���!");
    }
    private void SetMonsterAlive(bool alive)//���� ����ִ��� üũ
    {
        isMonsterAlive = alive;
    }
    private void Monster_Damaged(int damage)//���� �������Ա�
    {
        if (isMonsterAlive)
        {
            cur_Monster_Hp -= damage;
            cur_BreakPoint += 1;

            if (cur_Monster_Hp < 0)
            {
                SetMonsterAlive(false);
                MonsterDead();
            }
            if (cur_BreakPoint == max_BreakPoint)
            {
                Monster_Break();
            }
        }

    }
}
