using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController;
using Unity.VisualScripting;

[RequireComponent(typeof(Tester))]
public class TesterController : MonoBehaviour
{
    public Tester tester {  get; private set; }
    public Vector2 inputDirection {  get; private set; }
    public Vector2 calculatedDirection { get; private set; }
    public Vector2 gravity { get; private set; }

    [Header("경사 지형 검사")]
    [SerializeField, Tooltip("등반할 수 있는 최대 경사 각도입니다.")]
    float maxSlopeAngle;
    [SerializeField, Tooltip("경사를 체크할 Raycast 발사 시작 지점입니다.")]
    Transform raycastOrigin;

    private const float RAY_DISTANCE = 2f;
    private RaycastHit slopeHit;
    private bool isOnSlope;

    [Header("땅 체크")]
    [SerializeField, Tooltip("땅에 붙어 있는지 확인하기 위한 CheckBox 시작 지점")]
    Transform groundCheck;
    private int groundLayer;
    private bool isGrounded;

    private void Start()
    {
        tester=GetComponent<Tester>();
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
    }
}
