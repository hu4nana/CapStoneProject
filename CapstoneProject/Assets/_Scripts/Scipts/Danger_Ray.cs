using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger_Ray : MonoBehaviour
{
    //[SerializeField]
    float threatDistance = 15.0f;  // ������ Ȱ��ȭ �Ÿ�

    //private GameObject threatIndicator;  // ������ ������Ʈ
    private LineRenderer lineRenderer;   // ������ ������

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.enabled = false;  // ó������ ������ ��Ȱ��ȭ
        lineRenderer.material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToGround = CalculateDistanceToGround();

        //Debug.Log($"������ �Ÿ� = {distanceToGround}");
        //Debug.Log($"���� �Ÿ�{threatDistance}");

        // ���� �Ÿ� ���Ϸ� ��������� ������ Ȱ��ȭ
        if (distanceToGround < threatDistance)
        {
            ActivateThreatIndicator();
        }
        else
        {
            DeactivateThreatIndicator();
        }
    }


    float CalculateDistanceToGround()
    {
        // �������� ������Ʈ���� ���̸� ��� �ٴڱ����� �Ÿ� ���
        //Ray ray = new Ray(transform.position, Vector3.down);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    return hit.distance;
        //}
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(transform.position.x,transform.position.y-0.5f,transform.position.z), Vector3.down);

        // Physics.Raycast�� Ray�� �߻��ϰ� �浹 ���θ� ��ȯ�մϴ�.
        if (Physics.Raycast(ray, out hit))
        {
            // �ٴڱ����� �Ÿ� ��ȯ
            return hit.distance;
        }

        return 1;
    }

    void ActivateThreatIndicator()
    {
        // �������� �������� ���� ����
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, new Vector3(transform.position.x, 0f, transform.position.z));

        // ������ Ȱ��ȭ
        lineRenderer.enabled = true;
    }

    void DeactivateThreatIndicator()
    {
        // ������ ��Ȱ��ȭ
        lineRenderer.enabled = false;
    }
}
