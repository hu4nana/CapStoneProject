using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Col_Disable : MonoBehaviour
{

    BoxCollider particle_Col;

    private void Start()
    {
        particle_Col = GetComponent<BoxCollider>();
        if (particle_Col != null)
        {
            Diable_Col();
        }
    }

    public void Diable_Col()
    {
        StartCoroutine(Diable_ColCoroutine());
    }



    IEnumerator Diable_ColCoroutine()
    {
        
        // 잠시 대기
        yield return new WaitForSeconds(0.1f);  // 예시로 0.5초 동안 빨간색으로 유지

        particle_Col.enabled = false;
    }



}
