using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    float cur_Core_Hp; // 현재 체력
    float max_Core_Hp = 100;//최대체력

    float player_Damaged_Value = 0;//플레이어가 가하는 데미지 저장

    bool isCoreAlive = true; // 코어살아있는지 체크

    bool isAcitveCore = false;

    public CoreType coreType; // 코어타입
    CoreType playerType;//플레이어의 코어타입


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

    //    //Debug.Log($"충돌한 오브젝트 이름 : {collision.gameObject.name }");
    //    if (UnityEngine.LayerMask.LayerToName(collision.gameObject.layer) == "Player")
    //    {
    //        //Debug.Log("플레이어 레이어와 코어 충돌은 되었습니다");
    //        if (collision.gameObject.CompareTag("Player"))
    //        {

    //            //Debug.Log("플레이어 태그와 코어 충돌 되었습니다");
    //            playerType = collision.gameObject.GetComponent<ModeManager>().GetCoreType();
    //            player_Damaged_Value = collision.gameObject.GetComponent<ModeManager>().GetAttackDamage();
    //            Core_Damaged(playerType);
    //        }
    //    }
    //    //Debug.Log("코어 충돌은 되었습니다");
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

    public void Init()//코어초기설정
    {
        isAcitveCore = true;
        cur_Core_Hp = max_Core_Hp;
        coreType = CoreType.Magenta;
        isCoreAlive = true;
        Debug.Log($"현재 코어의 체력은 {cur_Core_Hp}으로 초기화 되었습니다");


    }
    //private void Core_Damaged(CoreType type)//코어데미지입기
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
    //        Debug.Log($"현재 코어의 체력은 {cur_Core_Hp}입니다.");
    //    }

    //}
    private void Core_Damaged()//코어데미지입기
    {
        if (isCoreAlive)
        {
            cur_Core_Hp -= player_Damaged_Value;
            
            if (cur_Core_Hp < 0)
            {
                SetCoreAlive(false);
                CoreBreak();
            }
            Debug.Log($"현재 코어의 체력은 {cur_Core_Hp}입니다.");
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
    public float GetCurCoreHp()//현재 코어의 HP반환
    {
        return cur_Core_Hp;
    }

    public void CoreRevive()//코어 부활
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
