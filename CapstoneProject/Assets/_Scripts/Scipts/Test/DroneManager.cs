using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    private GameObject weaponObject;
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject FollowingTarget;
    public Transform effectGenerator;
    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    public float stayTimer;
    float stayTime;
    int input = 0;
    Animator ani;
    Collider droneCol;
    Collider weaponCol;
    public bool isAttacking { get; set; }
    public bool isStandby { get;set; }

    private void Awake()
    {
        ani=GetComponent<Animator>();
        droneCol=GetComponent<Collider>();
        isStandby = true;
    }
    private void Update()
    {
        //DroneMove();
        if(isStandby)
        {
            ani.SetBool("isAttack", false);
            ani.SetBool("LongSword", false);
            ani.SetBool("Dual", false);

        }
        if(isStandby&&Input.GetKeyDown(KeyCode.A))
        {
            input = 0;
            ChangeWeapon(weapons[0]);
            ani.SetBool("isAttack",true);
            ani.SetBool("LongSword", true);
        }
        if (isStandby&&Input.GetKeyDown(KeyCode.S))
        {
            input = 1;
            ChangeWeapon(weapons[1]);
            ani.SetBool("isAttack",true);
            ani.SetBool("Dual", true);
        }
        //if (weaponCol!=null&&weaponCol.enabled == false)
        //{
        //    stayTime += Time.deltaTime;
        //}
        //if (stayTime>=stayTimer)
        //{
        //    ani.SetBool("isAttack", false);
        //    stayTime = 0;
        //}
    }

    void DroneMove()
    {
        if(isAttacking==false)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,
            FollowingTarget.GetComponent<Manuka_Gun>().Dir * 90, transform.rotation.z);
        }
        
    }
    /* ====================Animator Events==================== */
    public void AtttackStart()
    {
        weaponObject.SetActive(true);
        droneCol.enabled = false;
        weaponCol.enabled = true;
        isAttacking = true;
        isStandby = false;
        Debug.Log("DroneAttackTrue");
    }
    public void AttackEnd()
    {
        weaponObject.SetActive(false);
        droneCol.enabled = true;
        weaponCol.enabled = false;
        isAttacking = false;
        isStandby = true;
        Debug.Log("DroneAttackFalse");
    }
    public void PlayEffect()
    {
        particleSystems[input].Play();
        Debug.Log("PlayEffect");
    }
    /* ====================Animator Events==================== */
    public void ChangeWeapon(GameObject weapon)
    {
        for(int i=0; i< weapons.Count; i++)
        {
            if (weapons[i] == weapon)
            {
                SetWeapon(weapons[i]);
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }
    }
    public void SetActive(bool value)
    {
        if (weaponObject != null)
        {
            weaponObject.SetActive(value);
        }
    }
    public void SetWeapon(GameObject weapon)
    {
        weaponObject = weapon;
        weaponObject.SetActive(true);
        weaponCol = weapon.GetComponent<Collider>();        
    }
}
