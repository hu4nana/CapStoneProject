using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public CoreType Core { get { return core; } }


    [SerializeField] protected CoreType core;
    //[SerializeField] protected float speed;
    [SerializeField] protected float lifeTimer;
    [SerializeField] protected ParticleSystem hit;
    [SerializeField] protected GameObject gunShotFire;

    float lifeTime = 0;

    Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid= GetComponent<Rigidbody>();
        Instantiate(gunShotFire,this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = Vector3.right * 3;
        lifeTime += Time.deltaTime;

        if (lifeTime >= lifeTimer)
        {
            BulletDestroy();
        }
    }
    void BulletDestroy()
    {
        hit.GetComponent<ParticleSystem>();
        hit.Play();
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        BulletDestroy();
    }
}
