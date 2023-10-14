using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    int cur_Core_Hp;
    int max_Core_Hp = 100;

    int player_Damaged_Value = 0;

    bool isCoreAlive;

    CoreType coreType;
    CoreType playerType;

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
        if (collision.gameObject.CompareTag("Player"))
        {
            playerType = collision.gameObject.GetComponent<ModeManager>().GetCoreType();
            player_Damaged_Value = collision.gameObject.GetComponent<ModeManager>().GetAttackDamage();
            Core_Damaged(playerType);
        }
    }

    public void Init()//코어초기설정
    {
        cur_Core_Hp = max_Core_Hp;
        coreType = CoreType.Magenta;
        isCoreAlive = true;
        


    }
    private void Core_Damaged(CoreType type)//코어데미지입기
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
    public void Set_CoreType(CoreType type)//코어 타입 세팅
    {
        coreType = type;
    }
    public CoreType GetCoreType()//코어 타입 반환
    {
        return coreType;
    }
    public void SetCoreAlive(bool life) // 코어의 생존전환
    {
        isCoreAlive = life;
    }
    public void CoreBreak()//코어 부셔지는 메서드
    {
        if (!isCoreAlive)
        {
            Debug.Log("코어가 부셔졌습니다!");
        }
    }
    public bool GetCoreAlive()//현재 코어가 살아있는지 체크 및 반환
    {
        return isCoreAlive;
    }
    public int GetCurCoreHp()//현재 코어의 HP반환
    {
        return cur_Core_Hp;
    }

    public void CoreRevive()//코어 부활
    {
        cur_Core_Hp = max_Core_Hp;
    }


}
