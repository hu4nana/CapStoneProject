using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    //파티클 충격시 데미지만 추가해주는 스크립트
    [SerializeField]
    int damage = 10;
    public int GetDamage()
    {
        return damage;
    }
}
