using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBag : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " 충돌함 Laye : " + collision.gameObject.layer);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " 충돌함 Laye : " + other.gameObject.layer);
    }
}
