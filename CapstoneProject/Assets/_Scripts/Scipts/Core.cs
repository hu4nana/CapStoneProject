using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    float cur_Core_Hp; // ���� ü��
    float max_Core_Hp = 100;//�ִ�ü��

    float player_Damaged_Value = 0;//�÷��̾ ���ϴ� ������ ����

    bool isCoreAlive = true; // �ھ����ִ��� üũ

    bool isAcitveCore = false;

    public CoreType coreType; // �ھ�Ÿ��
    CoreType playerType;//�÷��̾��� �ھ�Ÿ��


    void Start()
    {
        //Init();
        //core_Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //private void OnCollisionEnter(Collision collision)
    //{

    //    //Debug.Log($"�浹�� ������Ʈ �̸� : {collision.gameObject.name }");
    //    if (UnityEngine.LayerMask.LayerToName(collision.gameObject.layer) == "Player")
    //    {
    //        //Debug.Log("�÷��̾� ���̾�� �ھ� �浹�� �Ǿ����ϴ�");
    //        if (collision.gameObject.CompareTag("Player"))
    //        {

    //            //Debug.Log("�÷��̾� �±׿� �ھ� �浹 �Ǿ����ϴ�");
    //            playerType = collision.gameObject.GetComponent<ModeManager>().GetCoreType();
    //            player_Damaged_Value = collision.gameObject.GetComponent<ModeManager>().GetAttackDamage();
    //            Core_Damaged(playerType);
    //        }
    //    }
    //    //Debug.Log("�ھ� �浹�� �Ǿ����ϴ�");
    //    //if(collision.gameObject.tag=="Player")

    //}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //playerType = collision.gameObject.GetComponent<ModeManager>().GetCoreType();
            PlayerAttack pa = collision.gameObject.GetComponent<PlayerAttack>();

            if (pa != null)
            {
                player_Damaged_Value = pa.Damage;

                Core_Damaged();
            }

        }
    }

    public void Init()//�ھ��ʱ⼳��
    {
        isAcitveCore = true;
        cur_Core_Hp = max_Core_Hp;
        coreType = CoreType.Magenta;
        isCoreAlive = true;
        Debug.Log($"���� �ھ��� ü���� {cur_Core_Hp}���� �ʱ�ȭ �Ǿ����ϴ�");


    }
    //private void Core_Damaged(CoreType type)//�ھ�����Ա�
    //{
    //    if (isCoreAlive)
    //    {
    //        if (coreType == type)
    //        {
    //            cur_Core_Hp -= 2 * player_Damaged_Value;
    //        }
    //        else
    //        {
    //            cur_Core_Hp -= player_Damaged_Value;
    //        }
    //        if (cur_Core_Hp < 0)
    //        {
    //            SetCoreAlive(false);
    //            CoreBreak();
    //        }
    //        Debug.Log($"���� �ھ��� ü���� {cur_Core_Hp}�Դϴ�.");
    //    }

    //}
    private void Core_Damaged()//�ھ�����Ա�
    {
        if (isCoreAlive)
        {
            cur_Core_Hp -= player_Damaged_Value;
            
            if (cur_Core_Hp < 0)
            {
                SetCoreAlive(false);
                CoreBreak();
            }
            Debug.Log($"���� �ھ��� ü���� {cur_Core_Hp}�Դϴ�.");
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
    public float GetCurCoreHp()//���� �ھ��� HP��ȯ
    {
        return cur_Core_Hp;
    }

    public void CoreRevive()//�ھ� ��Ȱ
    {

        cur_Core_Hp = max_Core_Hp;
    }
    public void SetActiveCore()
    {
        isAcitveCore = true;
        this.gameObject.SetActive(true);

    }
    public void SetDeactiveCore()
    {
        isAcitveCore = false;
        this.gameObject.SetActive(false);
    }


    public bool GetIsCoreActive()
    {
        return isAcitveCore;
    }
}
