using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger_Ray : MonoBehaviour
{
    //[SerializeField]
    float threatDistance = 15.0f;  // 레이저 활성화 거리

    //private GameObject threatIndicator;  // 레이저 오브젝트
    private LineRenderer lineRenderer;   // 레이저 렌더러

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.enabled = false;  // 처음에는 레이저 비활성화
        lineRenderer.material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToGround = CalculateDistanceToGround();

        //Debug.Log($"땅과의 거리 = {distanceToGround}");
        //Debug.Log($"위협 거리{threatDistance}");

        // 일정 거리 이하로 가까워지면 레이저 활성화
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
        // 떨어지는 오브젝트에서 레이를 쏘아 바닥까지의 거리 계산
        //Ray ray = new Ray(transform.position, Vector3.down);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    return hit.distance;
        //}
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(transform.position.x,transform.position.y-0.5f,transform.position.z), Vector3.down);

        // Physics.Raycast는 Ray를 발사하고 충돌 여부를 반환합니다.
        if (Physics.Raycast(ray, out hit))
        {
            // 바닥까지의 거리 반환
            return hit.distance;
        }

        return 1;
    }

    void ActivateThreatIndicator()
    {
        // 레이저의 시작점과 끝점 설정
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, new Vector3(transform.position.x, 0f, transform.position.z));

        // 레이저 활성화
        lineRenderer.enabled = true;
    }

    void DeactivateThreatIndicator()
    {
        // 레이저 비활성화
        lineRenderer.enabled = false;
    }
}
