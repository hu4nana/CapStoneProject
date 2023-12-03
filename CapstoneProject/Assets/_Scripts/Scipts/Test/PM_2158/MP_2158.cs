using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MP_2158 : MonoBehaviour
{
    public Transform gunFireTransform;
    public GameObject gunFireEffect;
    public GameObject gunProjEffect;
    public GameObject gunHitEffect;
    public GameObject bullet;

    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = gunProjEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(gunFireTransform.position, Vector3.down * 50, Color.red);
        Attack();
    }

    public void Attack()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(gunFireEffect,gunFireTransform.position,
               transform.rotation*gunFireEffect.transform.rotation);
            Instantiate(gunProjEffect, gunFireTransform.position,
               transform.rotation * gunProjEffect.transform.rotation);
            //Instantiate(gunHitEffect, gunFireTransform.position,
            //   transform.rotation * gunHitEffect.transform.rotation);
            Instantiate(bullet, gunFireTransform.position,
               transform.rotation * bullet.transform.rotation);
        }
    }
}
