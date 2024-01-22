using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotProj : MonoBehaviour
{
    public GameObject bullet;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("»£√‚µ ");
        Instantiate(bullet, other.transform);
    }
}
