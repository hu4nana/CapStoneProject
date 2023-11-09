using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedEnemy : Enemy
{
    //Collider coreCollider;
    int cur_BreakPoint;       //현재 그로기게이지
    int max_BreakPoint = 100; //최대 그로기게이지

    int cur_CoreCount = 0;    //현재 코어카운트
    int max_CoreCount = 3;    //최대 코어카운트

    int cur_Monster_Hp;       //현재체력
    int max_Monster_Hp = 100; //몬스터 최대 체력

    bool isMonsterAlive;      //몬스터 살아있는지 체크

    public GameObject[] monster_Core = new GameObject[3];
    Core[] core = new Core[3]; //코어생성

    int player_Damaged_Value = 0;//플레이어가 가하는 데미지 저장

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
        //Debug.Log($"충돌한 오브젝트 이름 : {collision.gameObject.name }");
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


    public int GetCurBreakPoint()//현재 그로기 게이지 반환
    {
        return cur_BreakPoint;
    }
    public int GetMaxBreakPoint()//최대 그로기 게이지 반환
    {
        return max_BreakPoint;
    }
    public void SetCurBreakPoint(int breakPoint)//보스현재BreakPoint 세팅
    {
        cur_BreakPoint = breakPoint;
    }
    public void SetCurMonsterHp(int hp)//보스현재BreakPoint 세팅
    {
        cur_Monster_Hp = hp;
    }
    public void SetMaxBreakPoint(int maxBreakPoint)//보스 최대 BreakPoint세팅
    {
        max_BreakPoint = maxBreakPoint;
    }
    private void IsCoreDead(Core core)//코어죽었는지 체크
    {
        if (!core.GetCoreAlive())
        {
            core.SetDeactiveCore();
            max_BreakPoint -= 25;
        }
        cur_CoreCount -= 1;
    }
    private void ReviveCore()//코어 3개 부활
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
    private void Monster_Break()//그로기상태, 브레이크상태
    {
        Debug.Log("몬스터가 브레이크상태에 빠졌습니다!");
        SetCurBreakPoint(0);
        SetMaxBreakPoint(100);

    }
    private void MonsterDead()
    {
        Debug.Log("몬스터 사망!");
        Destroy(this.gameObject,1.0f);
    }
    private void SetMonsterAlive(bool alive)//몬스터 살아있는지 체크
    {
        isMonsterAlive = alive;
    }
    private void Monster_Damaged(int damage)//몬스터 데미지입기
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
        Debug.Log($"현재 몬스터 체력 : {cur_Monster_Hp}");
        Debug.Log($"현재 브레이크포인트 : {cur_BreakPoint}");
        Debug.Log($"최대 브레이크포인트 : {max_BreakPoint}");
    }
}
