using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public BaseWeapon Weapon { get; private set; }
    private GameObject weaponObject;
    public List<GameObject> weapons = new List<GameObject>();
    //private List<GameObject> weapons = new List<GameObject>();

    public void ChangeWeapon(GameObject weapon)
    {
        for(int i=0; i< weapons.Count; i++)
        {
            if (weapons[i] == weapon)
                SetWeapon(weapons[i]);
            else
                weapons[i].SetActive(false);
        }
    }
    public void WeaponAttack()
    {
        Weapon.Attack();
    }
    public void UnRegisterWeapon(GameObject weapon)
    {
        if (weapons.Contains(weapon))
        {
            weaponObject = weapon;
            weaponObject.SetActive(false);
        }
    }
    public void SetWeapon(GameObject weapon)
    {
        if (Weapon == null)
        {
            weaponObject = weapon;
            Weapon=weapon.GetComponent<BaseWeapon> ();
            weaponObject.SetActive(true);
            Tester.Instance.animator.runtimeAnimatorController = Weapon.WeaponAnimator;
            return;
        }

        for(int i=0;i<weapons.Count; i++)
        {
            if (weapons[i].Equals(Weapon))
            {
                weaponObject = weapon;
                weaponObject.SetActive(true);
                Weapon = weapon.GetComponent<BaseWeapon>();
                Tester.Instance.animator.runtimeAnimatorController = Weapon.WeaponAnimator;
                continue;
            }
        }
        
    }
}
