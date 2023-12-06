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

    AudioSource aud;
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = gunProjEffect.GetComponent<ParticleSystem>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Gun_Shoot_Bullet();
    }

    public void Gun_Shoot_Bullet()
    {
        Instantiate(gunFireEffect, gunFireTransform.position,
               transform.rotation * gunFireEffect.transform.rotation);
        Instantiate(gunProjEffect, gunFireTransform.position,
           transform.rotation * gunProjEffect.transform.rotation);
        //Instantiate(gunHitEffect, gunFireTransform.position,
        //   transform.rotation * gunHitEffect.transform.rotation);
        
        Instantiate(bullet, gunFireTransform.position,
           transform.rotation);
        aud.Play();
        //bul.GetComponent<Rigidbody>().velocity = transform.right * 10;

        
    }
    //public void Gun_Shoot_Bullet(Transform parents)
    //{
    //    Instantiate(gunFireEffect, gunFireTransform.position,
    //           transform.rotation * gunFireEffect.transform.rotation);
    //    Instantiate(gunProjEffect, gunFireTransform.position,
    //       transform.rotation * gunProjEffect.transform.rotation);
    //    //Instantiate(gunHitEffect, gunFireTransform.position,
    //    //   transform.rotation * gunHitEffect.transform.rotation);

    //    Instantiate(bullet, gunFireTransform.position,
    //       parents.rotation);
    //    //bul.GetComponent<Rigidbody>().velocity = transform.right * 10;
    //}
}
