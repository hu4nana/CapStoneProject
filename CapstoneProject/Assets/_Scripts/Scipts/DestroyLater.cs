using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLater : MonoBehaviour
{
    [SerializeField]
    public float deadTime = 6.0f;

    void Start()
    {
        Destroy(this.gameObject, deadTime);
    }
}
