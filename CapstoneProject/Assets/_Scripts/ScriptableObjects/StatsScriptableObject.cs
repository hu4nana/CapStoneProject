using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewCharacterStats", menuName ="Game/Character Stats")]
public class StatsScriptableObject : ScriptableObject
{
    public int maxHealth;
    public int curHealth;
    public int atk;
    public int defense;
    public float moveSpd;
    public float dashPow;
    public float minJumpPow;
    public float maxJumpPow;
    public int rotateSpd;

    public int GetHealth()
    {
        return curHealth;
    }
    public void SetHealth(int health)
    {
        this.curHealth = health;
    }
    public void Go_To_I_Frame_Layer()
    {
        
    }
    public void Back_To_Own_Layer()
    {

    }
}
