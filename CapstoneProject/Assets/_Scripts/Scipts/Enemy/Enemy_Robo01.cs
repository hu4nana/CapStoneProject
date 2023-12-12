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

    private void Start()
    {
        curHp = maxHp;
        isEnd = true;
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
        TargetDetecter();
        if (isTrace)
        {
            ani.SetBool("isWalk", false);
            ani.SetBool("isTrace", true);
            if (attackTimer <= attackTime)
            {
                ani.SetBool("shoot", false);
                attackTimer += Time.deltaTime;
            }
            else
            {
                ani.SetBool("shoot", true);
                if (ani.GetCurrentAnimatorStateInfo(1).IsName("Armature|ShootX1") &&
                    ani.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1f)
                {
                    attackTimer = 0;
                }
            }
        }
        else
        {
            ani.SetBool("isTrace", false);
        }
    }
    public void TestPattern()
    {
        // Target�� �ν����� ���� �ൿ
        if (isTrace&&target!=null)
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
            //rigid.velocity = new Vector2(dir * speed, rigid.velocity.y);
            rigid.velocity = new Vector3(0, rigid.velocity.y,0);
            Debug.Log("���� ������");
        }
        else
        {
            // Target�� ���� ���� �ൿ
            // ���ظ� �Ծ��� ���� �����ð� �ൿ����
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
                // �ٴڰ� ������ ��ֹ��� �ְ���� �Ǵ��ϰ� ������
                if (isFloor && !isWall)
                {
                    rigid.velocity = new Vector3(dir * speed, rigid.velocity.y, 0);
                    //Debug.Log("������ ������");
                }
                else
                {

                    rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
                    //Debug.Log("���� ������");
                }
            }
            else if (rigid.velocity.x == 0)
            {
                isDamaged = false;
            }
        }
        if (rigid.velocity != Vector3.zero)
            ani.SetBool("isWalk", true);
        else
            ani.SetBool("isWalk", false);
        //Debug.Log("isFloor is "+isFloor);
        //Debug.Log("isWall is "+!isWall);
        
    }

    public void Enemy_Robo01_Attack()
    {
        var bulletL = Instantiate(bulletPrefab, bulletSpawnPointLeft.position, bulletSpawnPointLeft.rotation);
        bulletL.GetComponent<Rigidbody>().velocity = bulletSpawnPointLeft.forward * bulletSpeed;
        var bulletR = Instantiate(bulletPrefab, bulletSpawnPointRight.position, bulletSpawnPointRight.rotation);
        bulletR.GetComponent<Rigidbody>().velocity = bulletSpawnPointRight.forward * bulletSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            isDamaged = true;
        }
        if (other.gameObject.layer == 10)
        {
            //target = other.gameObject;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer==10)
        {
            target=other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            target = null;
        }
    }
}