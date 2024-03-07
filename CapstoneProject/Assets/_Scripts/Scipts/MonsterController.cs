using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Rigidbody boss_Rigid;
    Animator col_animator;

    //Vector3 direction;
    //[SerializeField]
    //BoxCollider dashAttackCollider;

    public ParticleSystem[] left_ParticleObject;
    public ParticleSystem[] right_ParticleObject;

    public ParticleSystem[] left_GunFire_Particle;
    public ParticleSystem[] right_GunFire_Particle;

    public ParticleSystem[] break_effect_Particle;



    public ParticleSystem dashAttack_Particle;

    [SerializeField]
    GameObject MissilePrefabs;
    [SerializeField]
    GameObject UnderBombPrefabs;

    bool isUnderBombFirst = true;
    public int CannonCount = 0;



    //public GameObject MissilePrefabs;
    //public GameObject RocketBomb;
    float Cannon_ShootDelayTime = 1.6f;
    float PrepareAttackDelaytime = 1.0f;
    float dashAttackDelaytime = 1.0f;
    float MovetoPrepareDelaytime = 1.0f;
    float break_Duration = 3.0f;
    float min_SkillDelay = 2.0f;


    bool isSkillUsed = false;
    bool isDashAttack = false;//대쉬 사용시 위치를 한번만 지정하는 용도로 사용하는 변수

    [SerializeField]
    float _scanRange = 17; // 스캔범위 늘릴예정

    [SerializeField]
    float _attackRange_1 = 10; // 미사일 공격 범위
    [SerializeField]
    float _attackRange_2 = 10; // 캐넌 범위
    [SerializeField]
    float _attackRange_3 = 8; // 대쉬 공격 범위

    //Transform player_Transform;

    //float hp = 500.0f;
    //float _speed = 1.5f;
    float _move_Speed = 1.5f;

    //float _runSpeed = 3.0f;
    float distance = 0.0f;
    float stomp_Distance = 3.0f;
    //float traceDistance = 30.0f;
    bool isTracking = false;
    bool _stopSkill = false;


    //void Start()
    //{
    //    player_Transform = GameObject.Find("Player").GetComponent<Transform>();

    //}

    public override void Init()
    {
        //base.Init();
        //player_Transform = GameObject.Find("Player").GetComponent<Transform>();
        boss_Rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //dashAttackCollider = GetComponent<BoxCollider>();

    }


    //private void Update()
    //{
    //    timer += Time.deltaTime;
    //    //Debug.Log(timer);
    //}


    //void Update()
    //{
    //    //{
    //    //    switch (_M_state)
    //    //    {
    //    //        case Define.State.Idle:
    //    //            Update_Idle();
    //    //            break;
    //    //        case Define.State.Moving:
    //    //            Update_Moving();
    //    //            break;
    //    //        case Define.State.Die:
    //    //            Update_Die();
    //    //            break;
    //    //        case Define.State.Core_Break:
    //    //            Update_Core_Break();
    //    //            break;
    //    //        case Define.State.Break:
    //    //            Update_Break();
    //    //            break;
    //    //        case Define.State.Jumping:
    //    //            Update_Jumping();
    //    //            break;
    //    //        case Define.State.Falling:
    //    //            Update_Falling();
    //    //            break;
    //    //        case Define.State.Stomping_Skill:
    //    //            Update_Stomping_Skill();
    //    //            break;
    //    //        case Define.State.Missile_Strike_Skill:
    //    //            Update_Missile_Strike_Skill();
    //    //            break;
    //    //        case Define.State.Cannon_Shooting_Skill:
    //    //            Update_Cannon_Shooting_Skill();
    //    //            break;

    //    //    }

    //    //TracingPlayer();

    //    //anim.Play("Walk");
    //    //anim.Play("Idle");
    //    Update_Idle();
    //}

    protected override void Update_Idle()
    {

        if (isMonsterAlive)
        {
            if (!isMonsterBreak)
            {


                boss_Rigid.velocity = Vector3.zero;
                //애니메이션
                //Idle_Walk_Ratio = Mathf.Lerp(Idle_Walk_Ratio, 0, 10.0f * Time.deltaTime);
                //Animator anim = GetComponent<Animator>();
                //anim.SetFloat("Idle_Walk_Ratio", 0);
                //anim.Play("Idle_Walk");
                //Animator anim = GetComponent<Animator>();
                //anim.SetFloat("speed", 0);

                //Todo 전체 매니저가 생긴다면 플레이어를 받아오기 새롭게 다시 받아오기


                NavMeshAgent nwa = gameObject.GetComponent<NavMeshAgent>();
                if (nwa == null)
                    nwa = gameObject.AddComponent<NavMeshAgent>();
                nwa.isStopped = true;
                nwa.SetDestination(this.transform.position);

                Debug.Log("Monstter Update Idle");
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player == null)
                {
                    return;
                }

                float distance = (player.transform.position - transform.position).magnitude;
                //좀더 최적화를 하려면 sqrmagnitude를 활용하면된다.

                //Debug.Log(distance);
                if (distance <= _scanRange)
                {
                    if (isSkillUsed == false)
                    {
                        nwa.isStopped = false;
                        _lockTarget = player;
                        State = Define.State.Moving;
                        //return;
                    }
                    else
                    {
                        if (timer > min_SkillDelay)//스킬사용시간
                        {
                            isSkillUsed = false;
                        }
                    }
                }
                //if (_M_state == Define.State.Die)
                //{
                //    return;
                //}


                //_destPos = Vector3.up;//player_Transform.position;


                //Vector3 dir = _destPos - new Vector3(transform.position.x, _destPos.y, _destPos.z);

                //if (dir.magnitude < traceDistance)
                //{
                //    _M_state = Define.State.Moving;
                //}
                //else
                //{
                //    _M_state = Define.State.Idle;
                //}
            }
            else
            {
                Invoke("Break_effect_Particle_Play", 0.5f);

                Invoke("Break_effect_Particle_Play", 1.0f);

                Invoke("Break_effect_Particle_Play", 1.5f);
                timer = 0;
                State = Define.State.Break;
            }
        }
        else
        {
            State = Define.State.Die;
        }
    }

    protected override void Update_Moving()
    {
        
        if (isMonsterAlive)
        {
            if (!isMonsterBreak)
            {


                Debug.Log("Update Moving");
                if (_lockTarget != null)
                {
                    
                    GameObject.Find("Canvas").GetComponent<UIManager>().Boss_HP_Panel_On();
                    _destPos = new Vector3(_lockTarget.transform.position.x, 0, 0);

                    float distance = (_destPos - transform.position).magnitude;



                    //범위 수정 잘하면 skill 여러개 갈아가면서 상태 지정가능할듯함
                    if (distance <= _attackRange_1)
                    {
                        Debug.Log("공격사거리 내부진입");
                        NavMeshAgent nwa = gameObject.GetComponent<NavMeshAgent>();
                        if (nwa == null)
                            nwa = gameObject.AddComponent<NavMeshAgent>();

                        nwa.SetDestination(this.transform.position);//목적지를 현재위치로 전환? 작동오류
                        nwa.isStopped = true;
                        nwa.velocity = Vector3.zero;

                        /*******************************************************/
                        /*      State  =>  Define.State.Idle   둘 중 하나로 간다 */
                        timer = 0;
                        State = Define.State.PrepareAttack;


                        //if (timer > waitingTime)//스킬사용시간
                        //{
                        //    //Instantiate(MissilePrefabs);
                        //    State = Define.State.PrepareAttack;
                        //    timer = 0;
                        //}
                        //else
                        //{
                        //    State = Define.State.Idle;
                        //}


                        //return;
                    }
                    if (distance > _scanRange)
                    {
                        NavMeshAgent nwa = gameObject.GetComponent<NavMeshAgent>();
                        if (nwa == null)
                            nwa = gameObject.AddComponent<NavMeshAgent>();
                        nwa.SetDestination(this.transform.position);
                        nwa.isStopped = true;
                        nwa.velocity = Vector3.zero;
                        //_lockTarget = null;
                        State = Define.State.Idle;

                        //return;

                    }

                }
                //nwa.isStopped = false;
                Vector3 dir = Vector3.right * (_destPos.x - transform.position.x);


                //Debug.Log(distance);
                //Debug.Log(dir.magnitude);

                if (dir.magnitude < 0.1f)
                {
                    State = Define.State.Idle;
                }
                else
                {
                    NavMeshAgent nwa = gameObject.GetComponent<NavMeshAgent>();
                    if (nwa == null)
                        nwa = gameObject.AddComponent<NavMeshAgent>();
                    nwa.SetDestination(_destPos);
                    nwa.speed = _move_Speed;


                    float moveDist = Mathf.Clamp(_move_Speed * Time.deltaTime, 0, dir.magnitude);
                    nwa.Move(dir.normalized * moveDist);


                    //플레이어 바라보기 수정필요
                    //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
                    //transform.rotation = Quaternion.Euler(0,dir.x,0);


                }

                //Vector3 dir = _destPos - new Vector3(transform.position.x, _destPos.y, _destPos.z);
                //if (dir.magnitude < 0.001f) // 몬스터가 플레이어 위치에 도달했을때
                //{
                //    _M_state = Define.State.Idle;
                //}
                //else
                //{
                //    float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 1, dir.magnitude);
                //    transform.position += dir.normalized * moveDist;
                //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

                //    //애니메이션
                //    //Animator anim = GetComponent<Animator>();
                //    //Idle_Walk_Ratio = Mathf.Lerp(Idle_Walk_Ratio, 1, 10.0f * Time.deltaTime);
                //    //anim.Play("Idle_Walk");
                //    //Debug.Log($"{dir.magnitude}");
                //    //anim.SetFloat("speed", _speed);

                //}
            }
            else
            {
                timer = 0;
                State = Define.State.Break;
            }
        }
        else
        {
            State = Define.State.Die;
        }

    }
    protected override void Update_Die()
    {
        //할 수 있는 행위는 없음
        //Destroy(gameObject, 3.0f);
        //return;
        boss_Rigid.velocity = Vector3.zero;
        GameObject.Find("Canvas").GetComponent<UIManager>().Gameclear_Panel_On();
    }
    protected override void Update_Core_Break()
    {
        return;
    }
    protected override void Update_Break()
    {
        if (isMonsterAlive)
        {

            boss_Rigid.velocity = Vector3.zero;

            nameEnemyScript.CoreInvisible();

            nameEnemyScript.SetMonsterBreak(false);

            if (timer > break_Duration)//스킬사용시간
            {
                //Instantiate(MissilePrefabs);
                State = Define.State.Idle;
                timer = 0;
            }
        }
        else
        {
            State = Define.State.Die;
        }

    }

    protected override void Update_Jumping()
    {
        return;
    }

    protected override void Update_Falling()
    {
        return;
    }

    protected override void Update_DashAttack_Skill()
    {
        
        //isStompSkill = true;
        NavMeshAgent nwa = gameObject.GetComponent<NavMeshAgent>();
        if (nwa == null)
            nwa = gameObject.AddComponent<NavMeshAgent>();
        //nwa.isStopped = true;
        //nwa.velocity = Vector3.zero;
        //Debug.Log("몬스터 대쉬어택Skill 사용중");
        if (!isDashAttack)
        {
            _destPos = new Vector3(_lockTarget.transform.position.x, 0, 0);
            //Vector3 dir = Vector3.right * (_destPos.x - transform.position.x);

            //if (dir.magnitude < 0.1f)
            //{
            //    timer = 0.0f;
            //    State = Define.State.AfterAttackDelay;
            //}
            //else
            //{
            //    nwa.SetDestination(_destPos);
            //    nwa.speed = _move_Speed*2.5f;


            //    float moveDist = Mathf.Clamp(_move_Speed * 2.5f * Time.deltaTime, 0, dir.magnitude);
            //    nwa.Move(dir.normalized * moveDist);


            //    //플레이어 바라보기 수정필요
            //    //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            //    //transform.rotation = Quaternion.Euler(0,dir.x,0);

            //    isDashAttack = true;
            //}
            isDashAttack = true;
        }
        Vector3 dir = Vector3.right * (_destPos.x - transform.position.x);

        Quaternion quat = Quaternion.LookRotation(Vector3.right * dir.x);
        transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);

        if (dir.magnitude < 0.1f)
        {
            timer = 0.0f;
            State = Define.State.AfterAttackDelay;
        }
        else
        {
            nwa.SetDestination(_destPos);
            nwa.speed = _move_Speed * 10.0f;


            float moveDist = Mathf.Clamp(_move_Speed * 5.0f * Time.deltaTime, 0, dir.magnitude);
            nwa.Move(dir.normalized * moveDist);


            //플레이어 바라보기 수정필요
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0,dir.x,0);

            //isDashAttack = true;
        }


        //if (timer > waitingTime)//스킬사용시간
        //{
        //    Debug.Log("몬스터 대쉬어택Skill 사용중");
        //    //Instantiate(MissilePrefabs);
        //    timer = 0.0f;
        //    //State = Define.State.AfterAttackDelay;
        //}
        //isStompSkill = false;


    }

    protected override void Update_Missile_Strike_Skill()
    {
        boss_Rigid.velocity = Vector3.zero;
        //isStompSkill = true;
        NavMeshAgent nwa = gameObject.GetComponent<NavMeshAgent>();
        if (nwa == null)
            nwa = gameObject.AddComponent<NavMeshAgent>();
        nwa.isStopped = true;
        nwa.velocity = Vector3.zero;
        Debug.Log("몬스터 Missile_Strike_Skill 사용중");

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot_rockets"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)//스킬사용시간
            {
                //Instantiate(MissilePrefabs, _destPos, Quaternion.identity);
                //Instantiate(UnderBombPrefabs, _destPos, Quaternion.identity);
                State = Define.State.AfterAttackDelay;
                timer = 0;
            }
        }


    }

    protected override void Update_Cannon_Shooting_Skill()
    {
        boss_Rigid.velocity = Vector3.zero;
        //isStompSkill = true;
        NavMeshAgent nwa = gameObject.GetComponent<NavMeshAgent>();
        if (nwa == null)
            nwa = gameObject.AddComponent<NavMeshAgent>();
        nwa.isStopped = true;
        nwa.velocity = Vector3.zero;
        Debug.Log("Cannon_Shooting_Skill 사용중");

        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(Vector3.right * dir.x);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
        if (timer > Cannon_ShootDelayTime + 1.8f)//스킬사용시간
        {
            //Instantiate(UnderBombPrefabs);
            State = Define.State.AfterAttackDelay;
            timer = 0;
        }
        else
        {

            //스킬 사용 딜레이 애니메이션 종료 후 1초정도 멈추기

        }

        //nwa.isStopped = true;
        //nwa.velocity = Vector3.zero;
        //return;
    }

    //private void TracingPlayer()
    //{
    //}

    //OnHitEvent는 전면 수정예정
    //void OnHitEvent() // 애니메이션 event에 메서드 추가 후 사용      공격을 맞췄을때 hit판정 주기위해 씀 
    //{
    //    Debug.Log($"Monster On Hit Event");

    //    if (_lockTarget != null)
    //    {
    //        //damage공식 or 데미지 주기
    //        //target의 hp정보가 들어가있는 스크립트를 받아와서
    //        //hp데미지 감소시키기 or 데미지받는 메서드 실행시키기


    //        //데미지 부여 이후
    //        //타겟 (플레이어)의 hp < 0 or player 상태 dead일 경우의 상황 구현

    //        //만약 살아있다면?
    //        float distance = (_lockTarget.transform.position - transform.position).magnitude;

    //        //_attackRange 3개 비교 후 딜레이 시간 체크해서 State 상황에 맞게 Random 지정
    //        if (distance <= _attackRange)
    //        {
    //            State = Define.State.Cannon_Shooting_Skill;
    //        }
    //        else
    //        {
    //            State = Define.State.Idle;
    //        }

    //        //if(_lockTarget){

    //        //}
    //    }
    //    else
    //    {
    //        State = Define.State.Idle;
    //    }
    //}

    protected override void Update_PrepareAttack()
    {

        if (isMonsterAlive)
        {
            if (!isMonsterBreak)
            {
                boss_Rigid.velocity = Vector3.zero;
                NavMeshAgent nwa = gameObject.GetComponent<NavMeshAgent>();
                if (nwa == null)
                    nwa = gameObject.AddComponent<NavMeshAgent>();
                nwa.isStopped = true;
                nwa.velocity = Vector3.zero;
                Debug.Log("몬스터 스킬사용 대기중");

                //시간 딜레이 3초 정도 시키기

                int nextAttack = 0;
                int maxRandomCount = 0;

                _destPos = new Vector3(_lockTarget.transform.position.x, 0, 0);

                float distance = (_destPos - transform.position).magnitude;

                //0부터 maxRandomCount까지 랜덤돌리기
                //distance 스킬 범위 체크 후 범위별로 
                //할 수 있는 공격을 랜덤하게 돌려서 사용하는 메커니즘
                //

                //
                //랜덤돌리기 전 상황
                if (timer > PrepareAttackDelaytime)//스킬사용시간
                {
                    if (_attackRange_2 < distance && distance <= _attackRange_1)
                    {
                        Debug.Log("1번 사거리만 겹침 1번 스킬 사용");
                        maxRandomCount = 0;
                    }
                    else if (_attackRange_3 < distance && distance <= _attackRange_2)
                    {
                        Debug.Log("1번 사거리 2번사거리 겹침 1번, 2번 스킬 랜덤 사용");
                        maxRandomCount = 1;
                    }
                    else if (distance <= _attackRange_3)
                    {
                        Debug.Log("3가지 사거리 다 겹침 올 랜덤");
                        maxRandomCount = 2;
                    }
                    if (maxRandomCount == 0)
                    {
                        nextAttack = 0;
                    }
                    else
                    {
                        nextAttack = UnityEngine.Random.Range(0, maxRandomCount + 1);
                        nextAttack = UnityEngine.Random.Range(0, maxRandomCount + 1);
                        //랜덤 돌리고 그 랜덤값 nextAttack 에 넣기
                    }
                    Debug.Log($"랜덤 결과값 {nextAttack}");

                    //nextAttack = 2;

                    switch (nextAttack)
                    {
                        case 0:
                            Debug.Log("PrepareAttack -> 미사일스트라이크어택");
                            _destPos = new Vector3(_lockTarget.transform.position.x, _lockTarget.transform.position.y, 0);
                            State = Define.State.Missile_Strike_Skill;
                            timer = 0;
                            break;
                        case 1:
                            Debug.Log("PrepareAttack -> 캐논슈팅어택");
                            _destPos = new Vector3(_lockTarget.transform.position.x, 0, 0);
                            State = Define.State.Cannon_Shooting_Skill;
                            timer = 0;
                            break;
                        case 2:
                            Debug.Log("PrepareAttack -> 대쉬어택");
                            State = Define.State.DashAttack_Skill;
                            timer = 0;
                            break;
                    }

                }

            }
            else
            {
                Invoke("Break_effect_Particle_Play", 1.0f);

                Invoke("Break_effect_Particle_Play", 2.0f);

                Invoke("Break_effect_Particle_Play", 3.0f);
                timer = 0;
                State = Define.State.Break;
            }
        }
        else
        {
            State = Define.State.Die;
        }
    }

    protected override void Update_AfterAttackDelay()
    {
        if (isMonsterAlive)
        {
            if (!isMonsterBreak)
            {
                //코어 보이게하기
                nameEnemyScript.CoreVisible();
                isSkillUsed = true;

                this.boss_Rigid.velocity = Vector3.zero;
                //3초정도 딜레이시간 주고 바로 Idle로 전환
                //코어 열어주기
                //3초 딜레이주기
                //코어 닫기
                //State = Define.State.Idle;

                if (timer > waitingTime/3)//스킬사용시간
                {
                    //Instantiate(MissilePrefabs);
                    nameEnemyScript.CoreInvisible();
                    State = Define.State.Idle;
                    timer = 0;
                }
                else
                {

                    //스킬 사용 딜레이 애니메이션 종료 후 1초정도 멈추기
                    //State = Define.State.Idle;
                    //SetState(Define.State.Idle);
                }
            }
            else
            {
                Invoke("Break_effect_Particle_Play", 0.5f);

                Invoke("Break_effect_Particle_Play", 1.0f);

                Invoke("Break_effect_Particle_Play", 1.5f);
                timer = 0;
                State = Define.State.Break;
            }
        }
        else
        {
            State = Define.State.Die;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (isStompSkill && State != Define.State.Die)
        //{
        //    GameObject player = GameObject.FindGameObjectWithTag("Player");
        //    //player.GetComponent<>();
        //    //플레이어 hp 직접 감소시키기 or 감소 메서드 실행시키기


        //}

        //공격맞췄을때 플레이어 밀기
        if (collision.gameObject.CompareTag("Player") && State == Define.State.DashAttack_Skill)
        {
            Rigidbody col_rigid = collision.gameObject.GetComponent<Rigidbody>();
            //Transform col_trans = collision.gameObject.GetComponent<Transform>();
            col_animator = collision.gameObject.GetComponent<Animator>();


            Vector3 direction = new Vector3((collision.transform.position.x - transform.position.x), 1.0f, 0).normalized;
            //Vector3 direction = (collision.transform.position - transform.position).normalized;

            //collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-100, 1, 0);
            //col_trans.Translate(new Vector3((transform.position.x-10),transform.position.y+3,0));
            //col_trans.localPosition = (new Vector3((transform.position.x - 1)*Time.deltaTime, transform.position.y + 3, 0));
            //col_trans.position = (new Vector3((transform.position.x - 10), transform.position.y + 3, 0));
            //col_trans.Translate(((collision.transform.forward * -10)));


            if (collision.gameObject.GetComponent<Rigidbody>().velocity.x != 0)
            {
                //Debug.Log("=================================================================================");
                Debug.Log(collision.gameObject.GetComponent<Rigidbody>().velocity.x);
                //Debug.Log(collision.gameObject.name);
            }
            if (col_rigid != null && col_animator != null)
            //if (col_rigid != null)
            {
                Debug.Log("====================================================================================");

                col_animator.enabled = false;

                //Debug.Log(col_rigid.velocity);
                //Debug.Log($"플레이어의 rigidbody입니다{boss_Rigid.velocity}");
                //col_rigid.AddForce(new Vector3 (-105.0f, 3.0f, 0.0f), ForceMode.Impulse);

                //col_rigid.AddForce(this.boss_Rigid.velocity,ForceMode.Force);
                //col_rigid.AddForce(this.boss_Rigid.velocity, ForceMode.VelocityChange);
                //col_rigid.AddForce(direction, ForceMode.Impulse);
                col_rigid.AddForce(direction * 20.0f, ForceMode.Impulse);
                //col_rigid.AddExplosionForce(this.boss_Rigid.velocity.magnitude,this.transform.position,2.0f);



                //col_rigid.velocity = boss_Rigid.velocity;   // 그냥 뒤로 쭈욱감 보스랑 같이
                //col_rigid.velocity = (direction * 100000.0f); // 안움직임
                //col_rigid.velocity = boss_Rigid.velocity * direction.magnitude; // 그냥 뒤로 쭈욱 밀림

                //col_rigid.velocity = direction * 1000 ;  //y로만 팅김
                //col_animator.enabled = true;

                Invoke("EnablePlayerAnim", 0.5f);
                Debug.Log("플레이어에게 AddForce하였습니다.");
            }
            //player_Damaged_Value = collision.gameObject.GetComponent<ModeManager>().GetAttackDamage();
            //Monster_Damaged(player_Damaged_Value);

            //ColliderRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            //if (ColliderRigidbody != null)
            //{
            //    direction = new Vector3((transform.position.x - collision.transform.position.x), 0, 0).normalized;
            //    //ColliderRigidbody.AddForce(direction * 10.0f, ForceMode.Impulse);
            //    isDashAttack = false;
            //}

            }



        }

    //3초의 딜레이를 주는 메서드를 만들어 보는게 좋을 것 같다
    //원하는 시간만큼의 딜레이를 주는 메서드 만들기







    //void SetState(Define.State state)
    //{
    //    State = state;
    //}



    /// <summary>
    /// 애니메이션 Event 관련 메서드  정리 ↓
    /// </summary>
    /// 


    //미사일 파티클 관련 메서드 ↓8개
    void Left_Missile_Particle_Event()
    {
        Left_ParticleObject_Play();
    }

    void Right_Missile_Particle_Event()
    {
        Right_ParticleObject_Play();
    }


    void Left_ParticleObject_Play()
    {
        foreach (ParticleSystem particles in left_ParticleObject)
        {
            particles.Play();
        }
    }
    void Right_ParticleObject_Play()
    {
        foreach (ParticleSystem particles in right_ParticleObject)
        {
            particles.Play();
        }
    }


    void Left_GunFire_Particle_Play()
    {
        foreach (ParticleSystem particles in left_GunFire_Particle)
        {
            particles.Play();
        }
    }
    void Right_GunFire_Particle_Play()
    {
        foreach (ParticleSystem particles in right_GunFire_Particle)
        {
            particles.Play();
        }
    }
    
    
    void DashAttack_Particle_Play()
    {
        //dashAttackCollider.enabled = true;
        dashAttack_Particle.Play();
    }

    void DashAttack_Particle_Event()
    {
        DashAttack_Particle_Play();
        //Invoke("DashAttack_Collider_disabled", 0.1f);
    }

    void Break_effect_Particle_Play()
    {
        foreach (ParticleSystem particles in break_effect_Particle)
        {
            particles.Play();
        }
    }


    void DashAttack_Collider_disabled()
    {
        //dashAttackCollider.enabled = false;
    }

    /// <summary>
    /// FirstAttackCheckerEvent 와 SecondAttackCheckerEvent
    /// 는 사용하지 않음
    /// </summary>
    void FirstAttackCheckerEvent()
    {
        isUnderBombFirst = true;
    }
    void SecondAttackCheckerEvent()
    {
        isUnderBombFirst = false;
    }


    //공격관련 이벤트 - 애니메이션 event로 넣어서 수작업으로 관리함

    //캐논슈팅 이벤트 
    void CannonShootEvent()
    {

        Vector3 dir_Attack = new Vector3(_destPos.x - this.transform.position.x, 0, 0).normalized;
        if (CannonCount == 0)
        {
            Debug.Log($"발사안함{CannonCount}");
            //Instantiate(UnderBombPrefabs, _destPos , Quaternion.identity);
        }
        else if (CannonCount == 1)
        {
            Debug.Log($"1호 발사{CannonCount}");
            Instantiate(UnderBombPrefabs, new Vector3(_destPos.x - (dir_Attack.x) * 3, 0, 0), Quaternion.identity);
            Left_GunFire_Particle_Play();
            Right_GunFire_Particle_Play();



        }
        else if (CannonCount == 2)
        {
            Debug.Log($"2호 발사{CannonCount}");
            Instantiate(UnderBombPrefabs, _destPos, Quaternion.identity);
            Left_GunFire_Particle_Play();
            Right_GunFire_Particle_Play();
        }
        else if (CannonCount == 3)
        {
            Debug.Log("3호 발사");
            Instantiate(UnderBombPrefabs, new Vector3(_destPos.x + (dir_Attack.x) * 3, 0, 0), Quaternion.identity);
            Left_GunFire_Particle_Play();
            Right_GunFire_Particle_Play();
        }


        CannonCount++;


        if (CannonCount == 4)
        {
            CannonCount = 0;
        }
    }

    //미사일 슈팅 이벤트
    void MissileShootEvent()
    {
        Debug.Log($"발사");
        Instantiate(MissilePrefabs, _destPos, Quaternion.identity);
    }




    ///플레이어 대시공격중 피격시 애니메이터 다시 켜주는 역할 약간 딜레이 넣어서 INVOKE로 사용하여야함
    void EnablePlayerAnim()
    {
        if (col_animator != null)
        {
            col_animator.enabled = true;
        }
    }
}
