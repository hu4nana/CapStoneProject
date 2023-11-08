using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionKnockBack : MonoBehaviour
{

    Rigidbody monsterRigidbody;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        monsterRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        direction = (transform.position - collision.transform.position).normalized;
    //        monsterRigidbody.AddForce(direction * 3.0f, ForceMode.Impulse);
    //    }
    //}
}
