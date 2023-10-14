using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    private ModeBase currentMode; // 현재 모드 저장변수

    void Start()
    {
        Init();
    }
    void Update()
    {
        //currentMode.ApplyPassive();//패시브는 상시적용이라 update 플레이어 스크립트로 옮겨도됨
    }
    public void SetMode(ModeBase mode)//모드 기본으로 받아와서 적용시키는 것
    {
        currentMode = mode;
        currentMode.OnModeActivated(); // 모드가 활성화 되었다고 알리는 메서드
    }

    public void Attack()//공격메서드 
    {
        currentMode.Attack();
    }
    public void UseSkill()//스킬사용메서드
    {
        currentMode.UseSkill();
    }

    public CoreType GetCoreType()//현재 코어타입 반환
    {
        return currentMode.GetCoreType();
    }

    public int GetAttackDamage()//공격데미지 반환
    {
        return currentMode.GetAttackDamage();
    }
    public int GetSkillDamage()//스킬데미지 반환
    {
        return currentMode.GetSkillDamage();
    }
    public void Init()
    {
        SetMode(new GreatSwordMode());//기본모드 적용
    }


}
