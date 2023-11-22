using CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public bool isAttack { get; set; }
    public int curCombo { get; set; }

    public int ComboCount { get; set; }
    public int MaxComboCount { get; set; }
    
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

    public abstract void Attack();
    public abstract void Skill();
    
}
