using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float Damage { get { return damage; } }

    [SerializeField] float damage;
}
