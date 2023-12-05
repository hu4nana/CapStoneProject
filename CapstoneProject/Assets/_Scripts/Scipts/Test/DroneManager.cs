using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    public BaseWeapon Weapon { get; private set; }
    private GameObject weaponObject;
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject FollowingTarget;
    public TestPlayer testPlayer;
    
    public float stayTimer;
    float stayTime;
    Animator ani;
    Collider droneCol;
    Collider weaponCol;
    public bool isAttacking { get; set; }
    //public float Speed { get { return weaponObject.GetComponent<BaseWeapon>().Speed; } }
    //public float AttackEndTime { get { return weaponObject.
    //            GetComponent<BaseWeapon>().AttackEndTime; } }
    //public int CurCombo { get { return weaponObject.GetComponent<BaseWeapon>().CurCombo; }
    //    set { weaponObject.GetComponent<BaseWeapon>().CurCombo = value; } }
    //public int MaxCombo { get { return weaponObject.GetComponent<BaseWeapon>().MaxCombo; } }
    //public float ExitTime
    //{
    //    get { return weaponObject.GetComponent<BaseWeapon>().ExitTime; }}
    public bool PlayedEffect
    {
        get { return weaponObject.GetComponent<BaseWeapon>().PlayedEffect; }
        set { weaponObject.GetComponent<BaseWeapon>().PlayedEffect = value; }
    }

    private void Awake()
    {
        ani=GetComponent<Animator>();
        droneCol=GetComponent<Collider>();
    }
    private void Update()
    {
        //DroneMove();
        if(Input.GetKeyDown(KeyCode.A))
        {
            ChangeWeapon(weapons[0]);
            ani.SetBool("isAttack",true);
            ani.SetBool("LongSword", true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeWeapon(weapons[1]);
            ani.SetBool("isAttack",true);
            ani.SetBool("Dual", true);
        }
        if (weaponCol!=null&&weaponCol.enabled == false)
        {
            stayTime += Time.deltaTime;
        }
        if (stayTime>=stayTimer)
        {
            ani.SetBool("isAttack", false);
        }
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
        isAttacking = true;
        droneCol.enabled = false;
        weaponCol.enabled = true;
        Debug.Log("DroneAttackTrue");
    }
    public void AttackEnd()
    {
        weaponObject.SetActive(false);
        isAttacking = false;
        droneCol.enabled = true;
        weaponCol.enabled = false;
        Debug.Log("DroneAttackFalse");
    }
    public void PlayEffect()
    {
        weaponObject.GetComponent<BaseWeapon>().PlayEffect();
        Debug.Log("PlayEffect");
    }



    /* ====================Animator Events==================== */




    //private List<GameObject> weapons = new List<GameObject>();
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
                
                Weapon = null;
            }
        }
    }
    public void WeaponAttack()
    {
        weaponObject.GetComponent<BaseWeapon>().Attack();
        
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
        if (Weapon == null)
        {
            weaponObject = weapon;
            weaponObject.SetActive(true);
            weaponCol =weapon.GetComponent<Collider>();

            return;
        }

        for(int i=0;i<weapons.Count; i++)
        {
            if (weapons[i].Equals(weapon))
            {
                weaponObject = weapon;
                weaponObject.SetActive(true);
                weaponCol = weapon.GetComponent<Collider>();
                continue;
            }
        }
        
    }
}
