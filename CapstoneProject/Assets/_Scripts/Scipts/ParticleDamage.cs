using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    //��ƼŬ ��ݽ� �������� �߰����ִ� ��ũ��Ʈ
    [SerializeField]
    int damage = 10;
    public int GetDamage()
    {
        return damage;
    }
}
