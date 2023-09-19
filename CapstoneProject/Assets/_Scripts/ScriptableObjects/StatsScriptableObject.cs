using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsScriptableObject : ScriptableObject
{
    public int maxHealth;
    public int curHealth;
    public int atk;
    public int defense;

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
