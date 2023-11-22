using CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BaseWeapon : MonoBehaviour
{
    public int ComboCount { get; set; }
    public int MaxComboCount { get; set; }
    public float NormalizedTime { get { return normalizedTime; } }
    public GameObject Player;
    public RuntimeAnimatorController WeaponAnimator { get { return weaponAnimator; } }
    public string Name { get { return _name; } }
    public float AttackDamage { get { return attackDamage; } }
    public float AttackSpeed { get { return AttackSpeed; } }
    public float AttackRange { get { return AttackRange; } }
    public float SkillCost { get { return skillCost; } }
    public CoreType Core { get { return core; } }
    
    
    [Header("무기 정보")]
    [SerializeField] protected RuntimeAnimatorController weaponAnimator;
    [SerializeField] protected string _name;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float skillCost;
    [SerializeField] protected float normalizedTime;
    [SerializeField] protected CoreType core;




    public void SetWeaponData(string name, float attackDamage,float attackSpeed,
        float attackRange, float skillCost,CoreType core)
    {
        this._name = name;
        this.attackDamage = attackDamage;
        this.attackSpeed = attackSpeed;
        this.attackRange = attackRange;
        this.skillCost = skillCost;
        this.core = core;
    }

    public virtual void Attack()
    {
        Debug.Log("Virtual Attack...");
        TestPlayer testPlayer = Player.GetComponent<TestPlayer>();

        testPlayer.isAttack = true;
        testPlayer.curCombo++;
        //testPlayer.ani.SetInteger("AttackCombo", (int)testPlayer.curCombo);
    }
    public virtual void Skill()
    {
        Debug.Log("Virtual Skill...");
    }
    
}
