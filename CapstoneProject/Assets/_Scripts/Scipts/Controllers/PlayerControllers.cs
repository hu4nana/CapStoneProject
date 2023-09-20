using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Players¿« ±‚¥…
public class PlayerControllers : MonoBehaviour
{
    public StatsScriptableObject playerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.y <= 180)
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.y * 2 * Time.deltaTime, 0);
        }
    }
}
