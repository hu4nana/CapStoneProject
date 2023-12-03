using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTimer;
    public GameObject hitEffect;

    float lifeTime;
    Rigidbody rigid;
    Collider col;
    int count = 0;
    float deadTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        ////ø°≥ ¡ˆ≈∫....?
        //Instantiate(hitEffect, transform);
        if (lifeTime<=lifeTimer)
        {
            rigid.velocity = Vector3.right * speed;
            lifeTime += Time.deltaTime;
        }
        else
        {
            col.enabled = false;
            rigid.velocity = Vector3.zero;
            if (count == 0)
            {
                Instantiate(hitEffect, transform);
                count++;
            }
            else
            {
                deadTime += Time.deltaTime;
                if (deadTime>=2)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnDestroy()
    {
        Debug.Log("a");
    }

    private void OnTriggerEnter(Collider other)
    {
        col.enabled = false;
        rigid.velocity = Vector3.zero;
        if (count == 0)
        {
            Instantiate(hitEffect, transform);
            count++;
        }
    }
}
