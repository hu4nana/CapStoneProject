using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public BaseWeapon Weapon { get; private set; }
    private GameObject weaponObject;
    public List<GameObject> weapons = new List<GameObject>();
    public TestPlayer testPlayer;
    public float NormalizedTime { get { return weaponObject.
                GetComponent<BaseWeapon>().NormalizedTime; } }
    //private List<GameObject> weapons = new List<GameObject>();

    private void Update()
    {
        if (testPlayer.isAttack)
        {
            weaponObject.SetActive(true);
        }
        else
        {
            weaponObject.SetActive(false);
        }
    }
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
