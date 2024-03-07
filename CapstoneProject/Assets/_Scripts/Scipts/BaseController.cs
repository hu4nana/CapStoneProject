using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected float timer;
    protected float waitingTime;

    protected bool isMonsterAlive;
    protected bool isMonsterBreak;
    


    [SerializeField]
    protected Vector3 _destPos;

    [SerializeField]
    protected GameObject _lockTarget;

    [SerializeField]
    protected Define.State _M_state = Define.State.Idle;

    protected Animator anim;


    [SerializeField]
    protected NamedEnemy nameEnemyScript;

    public virtual Define.State State//프로퍼티 식 구현
    {
        get { return _M_state; }
        set
        {
            _M_state = value;

            anim = GetComponent<Animator>();
            switch (_M_state)
            {
                case Define.State.Idle:
                    anim.speed = 1.0f;
                    anim.CrossFade("Idle", 0.1f);
                    break;
                case Define.State.Die:
                    anim.speed = 0.8f;
                    anim.CrossFade("Die", 0.1f);
                    break;
                case Define.State.Moving:
                    anim.speed = 0.5f;
                    anim.CrossFade("Walk", 0.1f);
                    break;
                case Define.State.NormalAttack:
                    anim.CrossFade("Attack", 0.1f);
                    break;
                case Define.State.DashAttack_Skill:
                    anim.speed = 1.0f;
                    anim.CrossFade("Run", 0.1f);
                    break;
                case Define.State.Cannon_Shooting_Skill:
                    anim.speed = 1.0f;
                    //anim.CrossFade("Idle", 0.1f);
                    //anim.speed = 0.5f;
                    anim.CrossFade("Shoot_cannons", 0.3f);
                    //anim.CrossFade("Shoot_cannons", 0.1f, -1,  0);

                    //anim.Play("Shoot_cannons");
                    break;
                case Define.State.Missile_Strike_Skill:
                    anim.speed = 0.5f;
                    anim.CrossFade("Shoot_rockets", 0.1f);
                    break;
                case Define.State.Break:
                    break;
                case Define.State.Core_Break:
                    break;
                case Define.State.PrepareAttack:
                    anim.CrossFade("Die", 0.1f);
                    anim.speed = 1.0f;
                    break;
                case Define.State.AfterAttackDelay:
                    anim.CrossFade("Die", 0.1f);
                    anim.speed = 1.0f;
                    break;
            }
        }
    }




    private void Start()
    {
        timer = 0.0f;
        waitingTime = 6.0f;
        Init();
        isMonsterAlive = true;
        isMonsterBreak = false;
        nameEnemyScript = GetComponent<NamedEnemy>();
    }
    void Update()
    {
        isMonsterBreak = nameEnemyScript.GetIsMonterBreak();
        isMonsterAlive = nameEnemyScript.GetIsMonterDead();
        timer += Time.deltaTime;
        //Debug.Log(timer);
        switch (State)
        {
            case Define.State.Idle:
                Update_Idle();
                break;
            case Define.State.Moving:
                Update_Moving();
                break;
            case Define.State.Die:
                Update_Die();
                break;
            case Define.State.Core_Break:
                Update_Core_Break();
                break;
            case Define.State.Break:
                Update_Break();
                break;
            case Define.State.Jumping:
                Update_Jumping();
                break;
            case Define.State.Falling:
                Update_Falling();
                break;
            case Define.State.NormalAttack:
                Update_NormalAttack();
                break;
            case Define.State.DashAttack_Skill:
                Update_DashAttack_Skill();
                break;
            case Define.State.Missile_Strike_Skill:
                Update_Missile_Strike_Skill();
                break;
            case Define.State.Cannon_Shooting_Skill:
                Update_Cannon_Shooting_Skill();
                break;
            case Define.State.PrepareAttack:
                Update_PrepareAttack();
                break;
            case Define.State.AfterAttackDelay:
                Update_AfterAttackDelay();
                break;

        }
    }



    public abstract void Init();
    protected virtual void Update_Idle() { }
    protected virtual void Update_Moving() { }
    protected virtual void Update_Die() { }
    protected virtual void Update_Core_Break() { }
    protected virtual void Update_Break() { }
    protected virtual void Update_Jumping() { }
    protected virtual void Update_Falling() { }
    protected virtual void Update_DashAttack_Skill() { }
    protected virtual void Update_Missile_Strike_Skill() { }
    protected virtual void Update_Cannon_Shooting_Skill() { }
    protected virtual void Update_PrepareAttack() { }
    protected virtual void Update_AfterAttackDelay() { }

    protected virtual void Update_NormalAttack() { }
}
