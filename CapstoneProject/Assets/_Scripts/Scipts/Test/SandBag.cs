using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBag : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name + " 충돌함 Layer : " + collision.gameObject.layer);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " 충돌함 Laye : " + other.gameObject.layer);
        if (other.gameObject.layer == 11)
        {
            //BaseWeapon core = other.GetComponent<BaseWeapon>();
            //DroneManager core=other.GetComponent<DroneManager>();

            //switch (core.Core)
            //{
            //    case CoreType.Magenta:
            //        Debug.Log(other.name + "'s Core is Magenta // " + core.Core);
            //        break;
            //    case CoreType.Yellow:
            //        Debug.Log(other.name + "'s Core is Yellow // " + core.Core);
            //        break;
            //    case CoreType.Saian:
            //        Debug.Log(other.name + "'s Core is Sian // " + core.Core);
            //        break;

            //}
            Debug.Log(other.gameObject.tag);
        }
    }
}
