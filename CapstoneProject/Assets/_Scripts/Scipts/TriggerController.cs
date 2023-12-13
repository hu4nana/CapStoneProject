using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    [SerializeField] LayerMask triggerObj;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }
    }
}
