using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Players의 기능
public class PlayerControllers : MonoBehaviour
{
    
    public StatsScriptableObject playerScriptable;

    Animator ani;
    Rigidbody rigid;
    float pressAtkTime;
    bool atkDown = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid= GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack(3);
    }
    private void FixedUpdate()
    {
        PlayerMovement();
        
    }
    public Vector3 GetInputMove()
    {
        float horizontal = 0;
        float vertical = 0;
        //Vector3 inputXZ = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            vertical = Input.GetAxisRaw("Vertical");
        }

        return new Vector3(horizontal, 0, vertical);
    }
    public float GetInputVertical()
    {
        float vertical = 0;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            vertical = Input.GetAxisRaw("Vertical");
        }
        return vertical;
    }
    public float GetInputHorizontal()
    {
        float horizontal = 0;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        return horizontal;
    }
    public KeyCode GetAttackKey()
    {
        return KeyCode.X;
    }


    // 공격을 몇 번 하는지 등, 차징 타이머
    public void PlayerAttack(float Timer)
    {
        if(Input.GetKeyDown(GetAttackKey()))
            atkDown = true;
        if (Input.GetKey(GetAttackKey()))
            pressAtkTime += Time.deltaTime;
        if (Input.GetKeyUp(GetAttackKey()))
        {
            Debug.Log(pressAtkTime);
            if (pressAtkTime < Timer)
            {
                Debug.Log("짧게 눌림");
            }
            else
            {
                Debug.Log("길게 눌림");
            }
            atkDown = false;
            pressAtkTime = 0.0f;
        }
    }

    void PlayerMovement()
    {
        PlayerDirection();
        PlayerVelocity();
    }
    void PlayerDirection()
    {
        // 1 == right, -1 == left

        // 방법 1 ( 좌 우 로만 움직임 )
        //if (InputManager.GetInputHorizontal() == 1)
        //{
        //    transform.LookAt(Vector3.right*100000);
        //}
        //else if(InputManager.GetInputHorizontal() == -1)
        //{
        //    transform.LookAt(Vector3.left*100000);
        //}

        // 방법 2 ( 키 입력된 Vector3값에 따라 바라봄 )
        if(InputManager.GetInputMove()!=Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(InputManager.GetInputMove());
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * playerScriptable.rotateSpd);
            ani.SetBool("isWalk", true);
        }
        else
        {
            ani.SetBool("isWalk", false);
        }
    }
    void PlayerVelocity()
    {
        rigid.velocity=
            InputManager.GetInputMove() * playerScriptable.moveSpd;
    }
}