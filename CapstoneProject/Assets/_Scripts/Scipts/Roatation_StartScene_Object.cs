using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roatation_StartScene_Object : MonoBehaviour
{
    float rotationSpeed = 20.0f;
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0,1,0)*timer * rotationSpeed);
    }
}