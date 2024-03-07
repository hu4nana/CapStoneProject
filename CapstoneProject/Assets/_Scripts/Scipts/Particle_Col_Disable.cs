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
        
        // ��� ���
        yield return new WaitForSeconds(0.1f);  // ���÷� 0.5�� ���� ���������� ����

        particle_Col.enabled = false;
    }



}
