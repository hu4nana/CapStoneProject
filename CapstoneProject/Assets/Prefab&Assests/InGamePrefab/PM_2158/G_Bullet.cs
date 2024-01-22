using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class G_Bullet : MonoBehaviour
{
    public float speed;
    public float g_lifeTimer;
    public GameObject hitEffect;

    float g_lifeTime;
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
        if (g_lifeTime<=g_lifeTimer)
        {
            rigid.velocity = -transform.up * speed;
            g_lifeTime += Time.deltaTime;
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
                if (deadTime>=1)
                {
                    Destroy(gameObject);
                }
            }
        }
        if (col.enabled == false)
        {
            rigid.velocity= Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        col.enabled = false;
        if (count == 0)
        {
            Instantiate(hitEffect, transform);
            count++;
        }
    }
}
