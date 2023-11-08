using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public int ComboCount { get;set; }
    public int MaxComboCount { get;set;}
    public string Name { get { return _name; } }
    public float AttackDamage { get { return attackDamage; } }
    ModeState modeState;
    [Header("무기 정보")]
    [SerializeField] protected RuntimeAnimatorController weaponAnimator;
    [SerializeField] protected string _name;
    [SerializeField] protected float attackDamage;


    public void SetWeaponData(string name, float attackDamage)
    {
        this._name = name;
        this.attackDamage = attackDamage;
    }

    public abstract void Attack();
}
