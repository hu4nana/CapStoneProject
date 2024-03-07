using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public BaseWeapon Weapon { get; private set; }
    private GameObject weaponObject;
    public List<GameObject> weapons = new List<GameObject>();
    public TestPlayer testPlayer;
    public GameObject leftHand;
    public float Speed { get { return weaponObject.GetComponent<BaseWeapon>().Speed; } }
    public float AttackEndTime { get { return weaponObject.
                GetComponent<BaseWeapon>().AttackEndTime; } }
    public int CurCombo { get { return weaponObject.GetComponent<BaseWeapon>().CurCombo; }
        set { weaponObject.GetComponent<BaseWeapon>().CurCombo = value; } }
    public int MaxCombo { get { return weaponObject.GetComponent<BaseWeapon>().MaxCombo; } }
    public float ExitTime
    {
        get { return weaponObject.GetComponent<BaseWeapon>().ExitTime; }}
    public bool PlayedEffect
    {
        get { return weaponObject.GetComponent<BaseWeapon>().PlayedEffect; }
        set { weaponObject.GetComponent<BaseWeapon>().PlayedEffect = value; }
    }
    public void PlayEffect()
    {
        weaponObject.GetComponent<BaseWeapon>().PlayEffect();
    }
    //private List<GameObject> weapons = new List<GameObject>();
    public void ChangeWeapon(GameObject weapon)
    {
        for(int i=0; i< weapons.Count; i++)
        {
            if (weapons[i] == weapon)
                SetWeapon(weapons[i]);
            else
            {
                weapons[i].SetActive(false);
                
                Weapon = null;
            }
            if (weapons[1]==weapon)
                leftHand.SetActive(true);
            else
                leftHand.SetActive(false);
        }
    }
    public void WeaponAttack()
    {
        weaponObject.GetComponent<BaseWeapon>().Attack();
        
    }
    public void UnRegisterWeapon(GameObject weapon)
    {
        if (weapons.Contains(weapon))
        {
            weaponObject = weapon;
            weaponObject.SetActive(false);
        }
    }
    public void SetActive(bool value)
    {
        if (weaponObject != weapons[2])
        {
            weaponObject.SetActive(value);
            if (weaponObject == weapons[1])
            {
                leftHand.SetActive(value);
                //Debug.Log(weaponObject.name + value);
            }
        }
    }
    public void SetWeapon(GameObject weapon)
    {
        if (Weapon == null)
        {
            weaponObject = weapon;
            Weapon=weapon.GetComponent<BaseWeapon> ();
            if (weaponObject == weapons[2])
                weaponObject.SetActive(true);
            testPlayer.ani.runtimeAnimatorController = Weapon.WeaponAnimator;
            testPlayer.ani.SetFloat("Speed", Speed);
            testPlayer.ani.SetFloat("MotionTime", ExitTime);
            //Tester.Instance.animator.runtimeAnimatorController = Weapon.WeaponAnimator;

            return;
        }

        for(int i=0;i<weapons.Count; i++)
        {
            if (weapons[i].Equals(weapon))
            {
                weaponObject = weapon;
                //weaponObject.SetActive(true);
                Weapon = weapon.GetComponent<BaseWeapon>();
                testPlayer.ani.runtimeAnimatorController = Weapon.WeaponAnimator;
                
                //Tester.Instance.animator.runtimeAnimatorController = Weapon.WeaponAnimator;
                continue;
            }
        }
        
    }
}
