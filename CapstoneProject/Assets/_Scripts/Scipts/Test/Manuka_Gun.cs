using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Manuka_Gun : MonoBehaviour
{
    [SerializeField] int g_maxHp;
    [SerializeField]
    protected Transform g_floorCheck;
    [SerializeField]
    protected Transform g_wallCheck;
    [SerializeField]
    protected GameObject MP_2158;
    Animator g_ani;
    Rigidbody g_rigid;
    MP_2158 mp;
    
    public bool g_isDash { get; set; }
    public bool g_isAttack { get; set; }
    public bool g_isJump { get; set; }
    bool g_isWall = false;
    bool g_isFloor = false;
    float g_jumpTime = 0;
    public int g_curHP { get; set; }
    int g_maxJump = 0;
    int g_curJump = 0;
    int g_dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        g_ani= GetComponent<Animator>();
        g_rigid = GetComponent<Rigidbody>();
        mp=MP_2158.GetComponent<MP_2158>();
        g_curHP = g_maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        G_PlayerMove();
        G_PlayerDash();
        G_PlayerJump();
        G_CheckWallAndGroundCollision();
        if(Input.GetKeyDown(KeyCode.X))
        {
            transform.rotation = Quaternion.Euler(0, 90 * g_dir, 0);
            g_ani.SetBool("isAttack", true);
            //if (g_ani.GetCurrentAnimatorStateInfo(0).IsName("Attack_0"))
            //{
            //    mp.Gun_Shoot_Bullet(transform);
            //}
            ////MP_2158_Attack();
        }
    }


    void G_PlayerMove()
    {
        if (!g_isAttack && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            if (InputManager.GetHorizontal() != 0)
                g_dir = (int)InputManager.GetHorizontal();
            g_ani.SetBool("isWalk", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(
     Vector3.right * g_dir), Time.deltaTime * 24);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            if (Input.GetKeyDown(InputManager.GetAttackKey()))
            {
                //g_ani.SetBool("isWalk", false);
                //InputManager.SetIsCg_aninput(false);
                g_isAttack = true;
                g_ani.SetBool("isAttack", true);
                return;
            }
        }
        if (g_isAttack || InputManager.GetHorizontal() == 0)
        {
            g_ani.SetBool("isWalk", false);
        }
    }
    void G_PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            g_isDash = true;

            g_rigid.velocity = Vector3.zero;
            gameObject.layer = 12;
            g_rigid.useGravity = false;
            //InputManager.SetIsCanInput(false);
        }
        if (g_ani.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
        {

            if (g_ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                //Debug.Log("ㅁ");
                //InputManager.SetIsCanInput(true);
                gameObject.layer = 10;
                g_isDash = false;
                g_rigid.useGravity = true;
            }
        }
        g_ani.SetBool("isDash", g_isDash);
    }
    void G_PlayerJump()
    {
        float jumpTimer = 0.4f;
        //float minJump = 5;
        //float maxJump = 8;
        //if(Input.GetKeyDown(KeyCode.V))
        //{
        //    jumpPow = minJump;
        //}
        //if (!isJump && Input.GetKey(KeyCode.V))
        //{
        //    jumpTime += Time.deltaTime;
        //    jumpPow += 0.1f;
        //    Debug.Log(jumpPow);
        //    if (jumpPow >= maxJump)
        //        jumpPow = maxJump;
        //}
        //if (!isJump&&(Input.GetKeyUp(KeyCode.V)||jumpTime>=jumpTimer))
        //{
        //    rigid.AddForce(Vector2.up * jumpPow, ForceMode.Impulse);
        //    isJump = true;
        //    ++curJump;
        //    ani.SetBool("isJump", true);
        //    jumpTime = 0;
        //    jumpPow = minJump;

        //}
        //if (isJump)
        //{
        //    if(Input.GetKey(KeyCode.LeftArrow)||
        //        Input.GetKey(KeyCode.RightArrow))
        //        transform.Translate(0, 0, 4 * Time.deltaTime);

        //}
        Vector3 jumpPow = new Vector3(g_rigid.velocity.x, 6, g_rigid.velocity.z);
        if (!g_isDash && Input.GetKeyDown(InputManager.GetJumpKey()) && g_curJump <= g_maxJump)
        {
            g_rigid.velocity = jumpPow;
        }
        if (g_jumpTime <= jumpTimer && !g_isDash && Input.GetKey(InputManager.GetJumpKey()))
        {
            g_rigid.velocity = jumpPow;
            g_jumpTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(InputManager.GetJumpKey()))
        {
            g_rigid.velocity = g_rigid.velocity;
            g_jumpTime = 0;
        }
        if (!g_isWall && g_rigid.velocity.y != 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.RightArrow))
                transform.Translate(0, 0, 4 * Time.deltaTime);
        }
        g_ani.SetBool("isJump", g_isJump);
    }
    public void MP_2158_Attack()
    {
        if (MP_2158 == null)
        {
            Debug.Log("Don't Have Gun");
        }
        else
        {
            mp.Gun_Shoot_Bullet(transform);
        }
    }
    public void MP_2158_AttackEnd()
    {
        g_ani.SetBool("isAttack", false);
    }
    void G_CheckWallAndGroundCollision()
    {
        // 플레이어의 위치와 방향을 기반으로 레이캐스트를 발사하여 충돌 검사
        RaycastHit hitInfo;
        g_isFloor = Physics.Raycast(g_floorCheck.position, Vector3.down, out hitInfo, 0.1f);
        g_isWall = Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.5f);
        if (Physics.Raycast(g_floorCheck.position, Vector3.down, out hitInfo, 0.1f))
        {
            // 바닥과 충돌

        }
        if (g_isFloor)
        {
            //Debug.Log("플레이어가 바닥에 닿아있습니다.");
            g_ani.SetBool("isJump", false);
            g_jumpTime = 0;
            g_curJump = 0;
            g_isJump = false;
        }
        else
        {
            g_isJump = true;
            g_curJump++;
        }

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.5f))
        {
            // 벽과 충돌
            //Debug.Log("플레이어가 벽에 닿아있습니다.");
        }
    }
}
