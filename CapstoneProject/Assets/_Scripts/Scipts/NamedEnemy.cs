using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedEnemy : MonoBehaviour
{
    private Renderer monsterRenderer;
    public Material defaultMaterial;  // ������ �⺻ ��Ƽ����




    //Collider coreCollider;
    int cur_BreakPoint=0;       //���� �׷α������
    private int max_BreakPoint = 1000; //�ִ� �׷α������

    int reduce_MaxBreakValue = 0;

    private int add_Break_Point_Value = 1;

    int cur_CoreCount = 0;    //���� �ھ�ī��Ʈ
    //[SerializeField]
    int max_CoreCount = 0;    //�ִ� �ھ�ī��Ʈ

    float cur_Monster_Hp;       //����ü��

    [SerializeField]
    float max_Monster_Hp = 500; //���� �ִ� ü��

    bool isMonsterAlive;      //���� ����ִ��� üũ
    bool isMonsterBreak;
    bool isMonsterAttacked;

    public GameObject[] monster_Core;
    public Core[] core; //�ھ����

    float player_Damaged_Value = 0;//�÷��̾ ���ϴ� ������ ����

    Rigidbody monsterRigidbody;

    Vector3 direction;



    private void Start()
    {
        Init();
        monsterRigidbody = GetComponent<Rigidbody>();
        //monsterRenderer = GetComponent<Renderer>();
        monsterRenderer = GetComponentInChildren<Renderer>();
    }


    private void Update()
    {
        if (isMonsterAttacked)
        {
            isMonsterAttacked = false;
        }

        if (core == null)
        {
            return;
        }
        foreach (Core cores in core)
        {
            IsCoreDead(cores);
        }
        
        //IsCoreDead(core[0]);
        //IsCoreDead(core[1]);
        //IsCoreDead(core[2]);

        if (cur_BreakPoint >= GetMaxBreakPoint())
        {
            Monster_Break();
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        //if(collision.gameObject.tag=="Player")
        //Debug.Log($"�浹�� ������Ʈ �̸� : {collision.gameObject.name }");
        if (collision.gameObject.layer == 11)
        {
            PlayerAttack p_Attack = collision.gameObject.GetComponent<PlayerAttack>();
            if (p_Attack != null) 
            {
                
                player_Damaged_Value = p_Attack.Damage;

                Monster_Damaged(player_Damaged_Value);
                Debug.Log("���Ͱ� ���������޾Ҵ�");
            }

            
            //player_Damaged_Value = 10;


            //direction = (transform.position - collision.transform.position).normalized;
            //monsterRigidbody.AddForce(direction * 3.0f, ForceMode.Impulse);
        }

    }





    private void Init()
    {
        add_Break_Point_Value = 1;
        reduce_MaxBreakValue = 150;
        isMonsterAttacked = false;
        if (monster_Core != null)
        {
            for (int i = 0; i < monster_Core.Length; i++)
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
        //core[0].Set_CoreType(CoreType.Magenta);
        //core[1].Set_CoreType(CoreType.Yellow);
        //core[2].Set_CoreType(CoreType.Saian);

        //cur_BreakPoint = max_BreakPoint;
        isMonsterAlive = true;

        SetCurBreakPoint(0);
        SetCurMonsterHp(max_Monster_Hp);
        
        max_CoreCount = core.Length;

        Debug.Log(cur_BreakPoint);
    }

    public void ShowHitEffect()
    {
        StartCoroutine(HitEffectCoroutine());
    }

    IEnumerator HitEffectCoroutine()
    {
        // ���������� ����
        monsterRenderer.material.color = Color.red;

        // ��� ���
        yield return new WaitForSeconds(0.1f);  // ���÷� 0.5�� ���� ���������� ����

        // �⺻ �������� ����
        monsterRenderer.material.color = defaultMaterial.color;
    }

    public int GetCurBreakPoint()//���� �׷α� ������ ��ȯ
    {
        return cur_BreakPoint;
    }
    public int GetMaxBreakPoint()//�ִ� �׷α� ������ ��ȯ
    {
        return max_BreakPoint;
    }


    public float GetCurHP()//���� ü�� ��ȯ
    {
        return cur_Monster_Hp;
    }
    public float GetMaxHP()//�ִ� ü�� ��ȯ
    {
        return max_Monster_Hp;
    }



    public void SetCurBreakPoint(int breakPoint)//��������BreakPoint ����
    {
        cur_BreakPoint = breakPoint;
    }
    public void SetCurMonsterHp(float hp)//��������BreakPoint ����
    {
        cur_Monster_Hp = hp;
    }

    public void SetMaxBreakPoint(int maxBreakPoint)//���� �ִ� BreakPoint����
    {
        max_BreakPoint = maxBreakPoint;
    }
    private void IsCoreDead(Core core)//�ھ��׾����� üũ
    {
        if (!core.GetCoreAlive() && core.GetIsCoreActive())
        {
            

            max_BreakPoint -= reduce_MaxBreakValue;
            if (max_BreakPoint <= 0)
            {
                max_BreakPoint = 0;
            }

            core.SetDeactiveCore();
        }

        cur_CoreCount -= 1;
        if (cur_CoreCount < 0)
        {
            cur_CoreCount = 0;
        }
    }
    private void ReviveCore()//�ھ� 3�� ��Ȱ
    {
        if (cur_CoreCount <= 0)
        {

            foreach (Core cores in core)
            {
                cores.SetActiveCore();
                cores.CoreRevive();
            }

            cur_CoreCount = max_CoreCount;
            max_BreakPoint = 1000;
        }
    }
    private void Monster_Break()//�׷α����, �극��ũ���� ���� �޼��� 
    {
        isMonsterBreak = true;
        Debug.Log("���Ͱ� �극��ũ���¿� �������ϴ�!");
        SetCurBreakPoint(0);
        SetMaxBreakPoint(1000);

    }
    private void MonsterDead() //���� ��� �����
    {
        Debug.Log("���� ���!");
        //Destroy(this.gameObject,1.0f);
    }
    private void SetMonsterAlive(bool alive)//���� ����ִ��� üũ
    {
        isMonsterAlive = alive;
    }
    private void Monster_Damaged(float damage)//���� �������Ա�
    {
        if (isMonsterAlive)
        {
            ShowHitEffect();
            isMonsterAttacked = true;
            cur_Monster_Hp -= damage;
            cur_BreakPoint += add_Break_Point_Value;

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

    public void SetMonsterBreak(bool _break)//���� �극��ũ ����
    {
        isMonsterBreak = _break;
    }

    public bool GetIsMonterBreak()//���� �극��ũ ����
    {
        return isMonsterBreak;
    }
    public bool  GetIsMonterDead()
    {
        return isMonsterAlive;
    } 

    public void CoreVisible()
    {
        foreach (Core cores in core)
        {
            cores.SetActiveCore();
        }
        
    }

    public void CoreInvisible()
    {
        foreach (Core cores in core)
        {
            cores.SetDeactiveCore();
        }
    }


    
}
