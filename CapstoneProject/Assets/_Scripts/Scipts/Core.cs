using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    int cur_Core_Hp; // ���� ü��
    int max_Core_Hp = 100;//�ִ�ü��

    int player_Damaged_Value = 0;//�÷��̾ ���ϴ� ������ ����

    bool isCoreAlive; // �ھ����ִ��� üũ

    CoreType coreType; // �ھ�Ÿ��
    CoreType playerType;//�÷��̾��� �ھ�Ÿ��

    //public Collider core_Collider;

    void Start()
    {
        Init();
        //core_Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag=="Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            playerType = collision.gameObject.GetComponent<ModeManager>().GetCoreType();
            player_Damaged_Value = collision.gameObject.GetComponent<ModeManager>().GetAttackDamage();
            Core_Damaged(playerType);
        }
    }

    public void Init()//�ھ��ʱ⼳��
    {
        cur_Core_Hp = max_Core_Hp;
        coreType = CoreType.Magenta;
        isCoreAlive = true;
        


    }
    private void Core_Damaged(CoreType type)//�ھ�����Ա�
    {
        if (isCoreAlive)
        {
            if (coreType == type)
            {
                cur_Core_Hp -= 2 * player_Damaged_Value;
            }
            else
            {
                cur_Core_Hp -= player_Damaged_Value;
            }
            if (cur_Core_Hp < 0)
            {
                SetCoreAlive(false);
                CoreBreak();
            }
        }

    }
    public void Set_CoreType(CoreType type)//�ھ� Ÿ�� ����
    {
        coreType = type;
    }
    public CoreType GetCoreType()//�ھ� Ÿ�� ��ȯ
    {
        return coreType;
    }
    public void SetCoreAlive(bool life) // �ھ��� ������ȯ
    {
        isCoreAlive = life;
    }
    public void CoreBreak()//�ھ� �μ����� �޼���
    {
        if (!isCoreAlive)
        {
            Debug.Log("�ھ �μ������ϴ�!");
        }
    }
    public bool GetCoreAlive()//���� �ھ ����ִ��� üũ �� ��ȯ
    {
        return isCoreAlive;
    }
    public int GetCurCoreHp()//���� �ھ��� HP��ȯ
    {
        return cur_Core_Hp;
    }

    public void CoreRevive()//�ھ� ��Ȱ
    {
        cur_Core_Hp = max_Core_Hp;
    }


}
