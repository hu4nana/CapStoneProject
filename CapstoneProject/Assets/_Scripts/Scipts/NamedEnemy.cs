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

    public GameObject[] monster_Core = new GameObject[3];
    Core[] core = new Core[3]; //�ھ����

    int player_Damaged_Value = 0;//�÷��̾ ���ϴ� ������ ����

    //Rigidbody monsterRigidbody;

    //Vector3 direction;


    private void Start()
    {
        Init();
        //monsterRigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        IsCoreDead(core[0]);
        IsCoreDead(core[1]);
        IsCoreDead(core[2]);

        if (cur_BreakPoint >= GetMaxBreakPoint())
        {
            Monster_Break();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag=="Player")
        //Debug.Log($"�浹�� ������Ʈ �̸� : {collision.gameObject.name }");
        if (collision.gameObject.CompareTag("Player"))
        {
            player_Damaged_Value = collision.gameObject.GetComponent<ModeManager>().GetAttackDamage();
            Monster_Damaged(player_Damaged_Value);

            //direction = (transform.position - collision.transform.position).normalized;
            //monsterRigidbody.AddForce(direction * 3.0f, ForceMode.Impulse);
        }


    }





    private void Init()
    {
        if (monster_Core != null)
        {
            for (int i = 0; i < 3; i++)
            {
                if (monster_Core[i].CompareTag("Core"))
                {
                    core[i] = monster_Core[i].GetComponent<Core>();
                }
            }
        }

        //coreCollider.GetComponent<Collider>();
        foreach (Core cores in core)
        {
            cores.Init();
        }
        core[0].Set_CoreType(CoreType.Magenta);
        core[1].Set_CoreType(CoreType.Yellow);
        core[2].Set_CoreType(CoreType.Saian);

        //cur_BreakPoint = max_BreakPoint;
        isMonsterAlive = true;

        SetCurBreakPoint(0);
        SetCurMonsterHp(max_Monster_Hp);

        Debug.Log(cur_BreakPoint);
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
    public void SetCurMonsterHp(int hp)//��������BreakPoint ����
    {
        cur_Monster_Hp = hp;
    }
    public void SetMaxBreakPoint(int maxBreakPoint)//���� �ִ� BreakPoint����
    {
        max_BreakPoint = maxBreakPoint;
    }
    private void IsCoreDead(Core core)//�ھ��׾����� üũ
    {
        if (!core.GetCoreAlive())
        {
            core.SetDeactiveCore();
            max_BreakPoint -= 25;
        }
        cur_CoreCount -= 1;
    }
    private void ReviveCore()//�ھ� 3�� ��Ȱ
    {
        if (cur_CoreCount <= 0)
        {

            core[0].SetActiveCore();
            core[0].CoreRevive();
            core[1].SetActiveCore();
            core[1].CoreRevive();
            core[2].SetActiveCore();
            core[2].CoreRevive();

            cur_CoreCount = max_CoreCount;
            max_BreakPoint = 100;
        }
    }
    private void Monster_Break()//�׷α����, �극��ũ����
    {
        Debug.Log("���Ͱ� �극��ũ���¿� �������ϴ�!");
        SetCurBreakPoint(0);
        SetMaxBreakPoint(100);

    }
    private void MonsterDead()
    {
        Debug.Log("���� ���!");
        Destroy(this.gameObject,1.0f);
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
            cur_BreakPoint += 10;

            if (cur_Monster_Hp <= 0)
            {
                SetMonsterAlive(false);
                MonsterDead();
            }
            if (cur_BreakPoint == max_BreakPoint)
            {
                Monster_Break();
            }
        }
        Debug.Log($"���� ���� ü�� : {cur_Monster_Hp}");
        Debug.Log($"���� �극��ũ����Ʈ : {cur_BreakPoint}");
        Debug.Log($"�ִ� �극��ũ����Ʈ : {max_BreakPoint}");
    }
}
