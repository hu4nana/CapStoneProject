using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    int cur_Core_Hp; // ���� ü��
    int max_Core_Hp = 100;//�ִ�ü��

    int player_Damaged_Value = 0;//�÷��̾ ���ϴ� ������ ����

    bool isCoreAlive = true; // �ھ����ִ��� üũ

    CoreType coreType; // �ھ�Ÿ��
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
        Debug.Log($"���� �ھ��� ü���� {cur_Core_Hp}���� �ʱ�ȭ �Ǿ����ϴ�");


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
    public int GetCurCoreHp()//���� �ھ��� HP��ȯ
    {
        return cur_Core_Hp;
    }

    public void CoreRevive()//�ھ� ��Ȱ
    {

        cur_Core_Hp = max_Core_Hp;
    }
    public void SetActiveCore()
    {
        this.gameObject.SetActive(true);
    }
    public void SetDeactiveCore()
    {
        this.gameObject.SetActive(false);
    }
}
