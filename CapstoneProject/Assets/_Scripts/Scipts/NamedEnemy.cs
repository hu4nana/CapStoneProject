using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedEnemy : MonoBehaviour
{
    private Renderer monsterRenderer;
    public Material defaultMaterial;  // 몬스터의 기본 머티리얼




    //Collider coreCollider;
    int cur_BreakPoint=0;       //현재 그로기게이지
    private int max_BreakPoint = 1000; //최대 그로기게이지

    int reduce_MaxBreakValue = 0;

    private int add_Break_Point_Value = 1;

    int cur_CoreCount = 0;    //현재 코어카운트
    //[SerializeField]
    int max_CoreCount = 0;    //최대 코어카운트

    float cur_Monster_Hp;       //현재체력

    [SerializeField]
    float max_Monster_Hp = 500; //몬스터 최대 체력

    bool isMonsterAlive;      //몬스터 살아있는지 체크
    bool isMonsterBreak;
    bool isMonsterAttacked;

    public GameObject[] monster_Core;
    public Core[] core; //코어생성

    float player_Damaged_Value = 0;//플레이어가 가하는 데미지 저장

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
        //Debug.Log($"충돌한 오브젝트 이름 : {collision.gameObject.name }");
        if (collision.gameObject.layer == 11)
        {
            PlayerAttack p_Attack = collision.gameObject.GetComponent<PlayerAttack>();
            if (p_Attack != null) 
            {
                
                player_Damaged_Value = p_Attack.Damage;

                Monster_Damaged(player_Damaged_Value);
                Debug.Log("몬스터가 데미지를받았다");
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
        // 빨간색으로 변경
        monsterRenderer.material.color = Color.red;

        // 잠시 대기
        yield return new WaitForSeconds(0.1f);  // 예시로 0.5초 동안 빨간색으로 유지

        // 기본 색상으로 변경
        monsterRenderer.material.color = defaultMaterial.color;
    }

    public int GetCurBreakPoint()//현재 그로기 게이지 반환
    {
        return cur_BreakPoint;
    }
    public int GetMaxBreakPoint()//최대 그로기 게이지 반환
    {
        return max_BreakPoint;
    }


    public float GetCurHP()//현재 체력 반환
    {
        return cur_Monster_Hp;
    }
    public float GetMaxHP()//최대 체력 반환
    {
        return max_Monster_Hp;
    }



    public void SetCurBreakPoint(int breakPoint)//보스현재BreakPoint 세팅
    {
        cur_BreakPoint = breakPoint;
    }
    public void SetCurMonsterHp(float hp)//보스현재BreakPoint 세팅
    {
        cur_Monster_Hp = hp;
    }

    public void SetMaxBreakPoint(int maxBreakPoint)//보스 최대 BreakPoint세팅
    {
        max_BreakPoint = maxBreakPoint;
    }
    private void IsCoreDead(Core core)//코어죽었는지 체크
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
    private void ReviveCore()//코어 3개 부활
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
    private void Monster_Break()//그로기상태, 브레이크상태 관련 메서드 
    {
        isMonsterBreak = true;
        Debug.Log("몬스터가 브레이크상태에 빠졌습니다!");
        SetCurBreakPoint(0);
        SetMaxBreakPoint(1000);

    }
    private void MonsterDead() //몬스터 사망 디버그
    {
        Debug.Log("몬스터 사망!");
        //Destroy(this.gameObject,1.0f);
    }
    private void SetMonsterAlive(bool alive)//몬스터 살아있는지 체크
    {
        isMonsterAlive = alive;
    }
    private void Monster_Damaged(float damage)//몬스터 데미지입기
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
        Debug.Log($"현재 몬스터 체력 : {cur_Monster_Hp}");
        Debug.Log($"현재 브레이크포인트 : {cur_BreakPoint}");
        Debug.Log($"최대 브레이크포인트 : {max_BreakPoint}");
    }

    public void SetMonsterBreak(bool _break)//몬스터 브레이크 셋터
    {
        isMonsterBreak = _break;
    }

    public bool GetIsMonterBreak()//몬스터 브레이크 겟터
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
