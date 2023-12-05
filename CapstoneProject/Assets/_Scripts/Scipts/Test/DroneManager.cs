using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    private GameObject weaponObject;
    public Vector2 offSet;
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject FollowingTarget;
    public Transform effectGenerator;
    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    public CoreType Core { get { return core; } }

    CoreType core;
    public float stayTimer;
    float stayTime;
    int input = 0;
    Animator ani;
    Collider droneCol;
    Collider weaponCol;
    public bool isAttacking { get; set; }
    public bool isStandby { get;set; }
    public bool isStay { get; set; }
    private void Awake()
    {
        ani=GetComponent<Animator>();
        droneCol=GetComponent<Collider>();
        isStandby = true;
    }
    private void Update()
    {
        DroneMove();
        if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            
            ani.SetBool("isAttack", false);
            ani.SetBool("LongSword", false);
            ani.SetBool("Dual", false);
        }
        if(isStandby&&Input.GetKeyDown(KeyCode.A))
        {
            input = 0;
            core = CoreType.Magenta;
            ChangeWeapon(weapons[0]);
            ani.SetBool("isAttack",true);
            ani.SetBool("LongSword", true);
        }
        if (isStandby&&Input.GetKeyDown(KeyCode.S))
        {
            input = 1;
            core = CoreType.Yellow;
            ChangeWeapon(weapons[1]);
            ani.SetBool("isAttack",true);
            ani.SetBool("Dual", true);
        }
        if (weaponCol != null && weaponCol.enabled == false&&isStay==true)
        {
            stayTime += Time.deltaTime;
        }
        if (stayTime >= stayTimer)
        {
            ani.SetBool("isAttack", false);
            stayTime = 0;
            isStay= false;
        }
    }

    void DroneMove()
    {
        if(isAttacking==false&&isStay==false)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,
            FollowingTarget.GetComponent<Manuka_Gun>().Dir * 90, transform.rotation.z);
            this.transform.position =
            new Vector3((
            FollowingTarget.transform.position.x+offSet.x * FollowingTarget.GetComponent<Manuka_Gun>().Dir),
            FollowingTarget.transform.position.y+offSet.y, 0);
        }

        

        
    }
    /* ====================Animator Events==================== */
    public void AtttackStart()
    {
        stayTime = 0;
        weaponObject.SetActive(true);
        droneCol.enabled = false;
        weaponCol.enabled = true;
        isAttacking = true;
        isStandby = false;
        isStay = false;
        //Debug.Log("DroneAttackTrue");
    }
    public void AttackEnd()
    {
        weaponObject.SetActive(false);
        droneCol.enabled = true;
        weaponCol.enabled = false;
        isAttacking = false;
        isStandby = true;
        isStay = true;
        //Debug.Log("DroneAttackFalse");
    }
    public void PlayEffect()
    {
        particleSystems[input].Play();
        //Debug.Log("PlayEffect");
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
        //weaponObject.SetActive(true);
        weaponCol = weapon.GetComponent<Collider>();        
    }
}
