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
    public float NormalizedTime { get { return weaponObject.
                GetComponent<BaseWeapon>().NormalizedTime; } }
    public float ExitTime
    {
        get { return weaponObject.GetComponent<BaseWeapon>().ExitTime; }}
    public bool PlayedEffect
    {
        get { return weaponObject.GetComponent<BaseWeapon>().PlayedEffect; }
        set { weaponObject.GetComponent<BaseWeapon>().PlayedEffect = value; }
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
    public void SetGreatSword()
    {

    }
    public void SetDuelBlades()
    {

    }
    public void SetHandCannon()
    {

    }
    public void SetWeapon(GameObject weapon)
    {
        if (Weapon == null)
        {
            weaponObject = weapon;
            Weapon=weapon.GetComponent<BaseWeapon> ();
            weaponObject.SetActive(true);
            testPlayer.ani.runtimeAnimatorController = Weapon.WeaponAnimator;
            //Tester.Instance.animator.runtimeAnimatorController = Weapon.WeaponAnimator;

            return;
        }

        for(int i=0;i<weapons.Count; i++)
        {
            if (weapons[i].Equals(weapon))
            {
                weaponObject = weapon;
                weaponObject.SetActive(true);
                Weapon = weapon.GetComponent<BaseWeapon>();
                testPlayer.ani.runtimeAnimatorController = Weapon.WeaponAnimator;
                //Tester.Instance.animator.runtimeAnimatorController = Weapon.WeaponAnimator;
                continue;
            }
        }
        
    }
}
