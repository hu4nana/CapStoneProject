using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public enum ModeType
{
    GreatSword,
    DuelBlades,
    HandCannon
}
[CreateAssetMenu(fileName ="NewMode",menuName ="Game/Mode")]

public class ModeScriptableObject : ScriptableObject
{
    public ModeType mode;
    public Collider attackCollider;
    public float attackStart;
    public float attackEnd;
    public int attackCombo;
    int curAttackCombo = 0;

    public void NormalAttack()
    {
        switch (curAttackCombo)
        {
        }
    }
    public void ChargingAttack()
    {
        
    }
}
