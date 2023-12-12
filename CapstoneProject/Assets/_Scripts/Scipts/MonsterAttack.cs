using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public float Damage { get { return damage; } }

    [SerializeField] float damage;
}
