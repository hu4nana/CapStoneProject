using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Robo01 : Enemy
{
    //public float speed = 1.0f;
    public float battleSpeed = 2f;
    public Transform bulletSpawnPointLeft;
    public Transform bulletSpawnPointRight;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5;
    public float particleDalay = 2;

    private void Start()
    {
        curHp = maxHp;
        ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        ani.SetFloat("speedMultiplier", speed);
        maxPattern = 3;
        //curPattern = 0;
    }


    private void Update()
    {
        PatternSelecter();
        CheckWallAndGroundCollision();
        TestPattern();
        TraceTimer();
        TraceTarget(speed);
        //if (Input.GetKeyDown("e"))
        //{
        //    ani.SetBool("shoot", true);
        //    StartCoroutine(StartDelay());
        //}
    }
    public void TestPattern()
    {

        if (isTrace && target != null)
        {

            if (target.transform.position.x - transform.position.x > 0)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }
            Direction();
            rigid.velocity = new Vector2(dir * speed, rigid.velocity.y);

        }
        else
        {
            if (!isDamaged)
            {
                switch (curPattern)
                {
                    case 0:
                        dir = 0;
                        break;
                    case 1:
                        dir = 1;
                        Direction();
                        break;
                    case 2:
                        dir = -1;
                        Direction();
                        break;
                }

                if (isFloor && !isWall)
                {
                    rigid.velocity = new Vector3(dir * speed, rigid.velocity.y, 0);
                }
                else
                {
                    rigid.velocity = Vector3.zero;
                }



                //if ((isWall || !isFloor))
                //{
                //    rigid.velocity = Vector2.zero;
                //}
                //else
                //{
                //    rigid.velocity = new Vector2(-dir * stats.moveSpeed, rigid.velocity.y);
                //}

            }
            else if (rigid.velocity.x == 0)
            {
                isDamaged = false;
            }
        }
        if (rigid.velocity != Vector3.zero)
            ani.SetBool("Walk Forward", true);
        else
            ani.SetBool("Walk Forward", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            //isDamaged = true;
        }
        if (other.gameObject.layer == 10)
        {
            target = other.gameObject;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer==10)
        {
            target=other.gameObject;
            ani.SetBool("shoot", true);
            StartCoroutine(StartDelay());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            target = null;
        }
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(particleDalay);
        var bulletL = Instantiate(bulletPrefab, bulletSpawnPointLeft.position, bulletSpawnPointLeft.rotation);
        bulletL.GetComponent<Rigidbody>().velocity = bulletSpawnPointLeft.forward * bulletSpeed;
        var bulletR = Instantiate(bulletPrefab, bulletSpawnPointRight.position, bulletSpawnPointRight.rotation);
        bulletR.GetComponent<Rigidbody>().velocity = bulletSpawnPointRight.forward * bulletSpeed;
    }
}