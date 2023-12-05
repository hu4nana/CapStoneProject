using CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BaseWeapon : MonoBehaviour
{
    public int CurCombo { get { return curCombo; } set { curCombo = value; } }
    public int MaxCombo { get { return maxCombo; } }
    public float AttackEndTime { get { return attackEndTime; } }
    public GameObject Player;
    public RuntimeAnimatorController WeaponAnimator { get { return weaponAnimator; } }
    public string Name { get { return _name; } }
    public float AttackDamage { get { return attackDamage; } }
    public float SkillCost { get { return skillCost; } }
    public CoreType Core { get { return core; } }
    public bool PlayedEffect { get { return playedEffect; } set {  playedEffect = value; } }
    public float ExitTime { get { return exitTime; } }
    public float Speed { get { return speed; } }
    
    
    [Header("무기 정보")]
    [SerializeField] protected RuntimeAnimatorController weaponAnimator;
    [SerializeField] protected GameObject effectGenerator;
    [SerializeField] protected ParticleSystem effectParticle;
    [SerializeField] protected List<Vector3> effectPos=new List<Vector3>();
    [SerializeField] protected List<Vector3> effectRot=new List<Vector3>();
    [SerializeField] protected string _name;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float skillCost;
    [SerializeField] protected float exitTime;
    [SerializeField] protected float attackEndTime;
    [SerializeField] protected float speed;
    [SerializeField] protected int curCombo;
    [SerializeField] protected int maxCombo;
    [SerializeField] protected CoreType core;
    

    protected bool playedEffect;



    public void SetWeaponData(string name, float attackDamage,float skillCost,CoreType core)
    {
        this._name = name;
        this.attackDamage = attackDamage;
        this.skillCost = skillCost;
        this.core = core;
    }
    public virtual void Attack()
    {
        TestPlayer testPlayer = Player.GetComponent<TestPlayer>();
        //if(testPlayer.curCombo>=maxCombo)
        //{
        //    testPlayer.curCombo = maxCombo;
        //}
        //else
        //{

        //    SetEffectGenerator(testPlayer.curCombo);
        //    PlayEffect();
        //    testPlayer.curCombo++;
        //}
        //testPlayer.ani.SetInteger("AttackCombo", testPlayer.curCombo);
        SetEffectGenerator(testPlayer.curCombo);
        PlayEffect();
    }
    public void PlayEffect()
    {
        Instantiate(effectParticle);
        //effectParticle.Play();
        //if (effectParticle != null&&!playedEffect)
        //{
        //    effectParticle.Play();
        //    playedEffect = true;
        //}
        //else
        //{
        //    Debug.Log("effectParticle이 존재하지 않음");
        //}
    }
    public void SetEffectGenerator(int curCombo)
    {
        //TestPlayer testPlayer = Player.GetComponent<TestPlayer>();
        effectGenerator.transform.localPosition= effectPos[curCombo];
        effectGenerator.transform.localRotation = Quaternion.Euler(effectRot[curCombo]);
    }
    public virtual void Skill()
    {
        Debug.Log("Virtual Skill...");
    }   
}