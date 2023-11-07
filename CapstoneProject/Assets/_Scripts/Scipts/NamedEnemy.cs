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

    Core[] core = new Core[3]; //코어생성


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
    public void SetMaxBreakPoint(int maxBreakPoint)//보스 최대 BreakPoint세팅
    {
        max_BreakPoint = maxBreakPoint;
    }
    private void IsCoreDead(Core core)//코어죽었는지 체크
    {
        if (core.GetCoreAlive())
        {
            cur_BreakPoint -= 25;
        }
        cur_CoreCount -= 1;
    }
    private void ReviveCore()//코어 3개 부활
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
    private void Monster_Break()//그로기상태, 브레이크상태
    {
        Debug.Log("몬스터가 브레이크상태에 빠졌습니다!");
    }
    private void MonsterDead()
    {
        Debug.Log("몬스터 사망!");
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
