using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    private ModeBase curWeapon; // 현재 모드 저장변수
    private List<ModeBase> modes;
    int curMode = 0;
    private float modeTimer = 0.0f;
    private float startTime = 0.0f;

    void Start()
    {
        //Init();
        modes.Add(new GreatSwordMode());
        modes.Add(new DuelBladeMode());
        modes.Add(new HandCannonMode());
        for(int i = 0; i < modes.Count; i++)
        {
            modes[i].GetComponent<ModeBase>();
        }
    }
    void Update()
    {
        //modes[].ApplyPassive();//패시브는 상시적용이라 update 플레이어 스크립트로 옮겨도됨
    }
    //public void SetMode(ModeBase mode)//모드 기본으로 받아와서 적용시키는 것
    //{
    //    modes[0].
    //    modes[] = mode;
    //    modes[].OnModeActivated(); // 모드가 활성화 되었다고 알리는 메서드
    //}

    //public void Attack()//공격메서드 
    //{
    //    if (startTime != modeTimer)
    //        return;
    //    modes[].Attack();
    //    modeTimer = modes[].GetDelayTimer();
    //}
    //public void UseSkill()//스킬사용메서드
    //{
    //    if (modeTimer != 0.0f)
    //        return;
    //    modes[].UseSkill();
    //}

    //public CoreType GetCoreType()//현재 코어타입 반환
    //{
    //    return modes[].GetCoreType();
    //}

    //public int GetAttackDamage()//공격데미지 반환
    //{
    //    return modes[].GetAttackDamage();
    //}
    //public int GetSkillDamage()//스킬데미지 반환
    //{
    //    return modes[].GetSkillDamage();
    //}
    //public void Init()
    //{
    //    SetMode(new GreatSwordMode());//기본모드 적용
    //}

    //public float GetModeDelayTime()
    //{
    //    modeTimer = GetModeDelayTime();
    //    return modes[].GetDelayTimer();
    //}
    //public void SetModeDelayTime(int time)
    //{
    //    modeTimer = time;
    //}
    
    public void SetMode(ModeBase mode)//모드 기본으로 받아와서 적용시키는 것
    {
        curWeapon = mode;
        curWeapon.OnModeActivated(); // 모드가 활성화 되었다고 알리는 메서드
    }

    public void Attack()//공격메서드 
    {
        if (startTime != modeTimer)
            return ;
        curWeapon.Attack();
        modeTimer = curWeapon.GetDelayTimer();
    }
    public void UseSkill()//스킬사용메서드
    {
        if (modeTimer != 0.0f)
            return;
        curWeapon.UseSkill();
    }

    public CoreType GetCoreType()//현재 코어타입 반환
    {
        return curWeapon.GetCoreType();
    }

    public int GetAttackDamage()//공격데미지 반환
    {
        return curWeapon.GetAttackDamage();
    }
    public int GetSkillDamage()//스킬데미지 반환
    {
        return curWeapon.GetSkillDamage();
    }
    public void Init()
    {
        SetMode(new GreatSwordMode());//기본모드 적용
    }

    public float GetModeDelayTime()
    {
        modeTimer = GetModeDelayTime();
        return curWeapon.GetDelayTimer();
    }
    public void SetModeDelayTime(int time)
    {
        modeTimer = time;
    }
    

}
