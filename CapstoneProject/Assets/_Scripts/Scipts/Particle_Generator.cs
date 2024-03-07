using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Generator : MonoBehaviour
{


    [SerializeField]
    int damage = 10;

    public ParticleSystem particle;

    //private void OnCollisionEnter(Collision col)
    //{
    //    Instantiate(particle,this.transform.position,Quaternion.identity);
    //    Destroy(this.gameObject);
        
    //}

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(particle, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);

    }


    public int GetDamage()
    {
        return damage;
    }
}
