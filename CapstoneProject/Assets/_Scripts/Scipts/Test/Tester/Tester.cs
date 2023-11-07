using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController;
public class Tester : MonoBehaviour
{
    public static Tester Instance { get { return Instance; } }
    public StateMachine stateMachine { get; private set; }
    public Rigidbody rigidBody { get; private set; }
    public Animator animator { get; private set; }
    public CapsuleCollider capsuleCollider { get; private set; }

    private static Tester instance;

    public float MaxHP { get { return MaxHP; } }
    public float CurrentHP { get { return CurrentHP; } }
    public float Armor { get { return Armor; } }
    public float MoveSpeed { get { return MoveSpeed; } }
    public int DashCount { get { return DashCount; } }

    [Header("캐릭터 스탯")]
    [SerializeField] protected float maxHP;
    [SerializeField] protected float currentHP;
    [SerializeField] protected float armor;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float dashCount;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            rigidBody=GetComponent<Rigidbody>();
            animator=GetComponent<Animator>();
            capsuleCollider=GetComponent<CapsuleCollider>();
            DontDestroyOnLoad(gameObject);
            return;
        }
        DestroyImmediate(gameObject);
    }

    private void Start()
    {
        InitStateMachine();
    }
    private void Update()
    {
        stateMachine?.UpdateState();
    }
    private void FixedUpdate()
    {
        stateMachine?.FixedUpdateState();
    }

    public void ObUpdateStat(float maxHP,float currentHP,float armor, float moveSpeed, int dashCount)
    {
        this.maxHP = maxHP;
        this.currentHP = currentHP;
        this.armor = armor;
        this.moveSpeed = moveSpeed;
        this.dashCount = dashCount;
    }

    private void InitStateMachine()
    {
        // 상태를 만들고 여기에 틍록
    }
}
