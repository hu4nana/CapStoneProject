using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    [SerializeField] LayerMask triggerObj;
    [SerializeField] float triggerTimer;
    float triggerTime;
    public bool isTrigger {  get; private set; }

    private void Update()
    {
        if (isTrigger)
        {
            triggerTime += Time.deltaTime;
            if (triggerTime >= triggerTimer)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //ug.Log(collision.name);7

        if (collision.gameObject.layer == 10)
        {
            isTrigger = true;
            //Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.gameObject.layer == 10)
        {
            isTrigger = true;
            Debug.Log("Trigger¿€µøµ ");
            //Destroy(gameObject);
        }
    }
}
