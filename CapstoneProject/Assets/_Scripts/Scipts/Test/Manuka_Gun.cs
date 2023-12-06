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
    public int Dir { get { return g_dir; } }
    public bool g_isDash { get; set; }
    public bool g_isAttack { get; set; }
    public bool g_isJump { get; set; }
    public bool g_isWalk { get; set; }
    bool g_isWall = false;
    bool g_isFloor = false;
    float g_jumpTime = 0;
    public int g_curHP { get; set; }
    int g_maxJump = 1;
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
        if(!g_isDash&&!g_isJump&&g_isFloor&&Input.GetKeyDown(KeyCode.X))
        {
            transform.rotation = Quaternion.Euler(0, 90 * g_dir, 0);
            InputManager.SetIsCanInput(false);
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
        if (InputManager.GetIsCanInput()&&((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))))
        {
            g_isWalk = true;
        }
        if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)))
        {
            g_isWalk = false;
        }
        if (InputManager.GetIsCanInput()&&g_isWalk)
        {
            if (InputManager.GetHorizontal() != 0)
                g_dir = (int)InputManager.GetHorizontal();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(
                Vector3.right * g_dir), Time.deltaTime * 24);
            if (g_isFloor)
            {
                
                if (g_ani.GetBool("isAttack") == true)
                {
                    g_ani.Play("Run_Fast_Loop_RM");
                    g_ani.SetLayerWeight(0, 1);
                    g_ani.SetBool("isAttack", false);
                    g_ani.SetBool("isWalk", true);
                }
                else
                {
                    g_ani.SetBool("isWalk", true);
                    if (InputManager.GetIsCanInput() && Input.GetKey(InputManager.GetAttackKey()))
                    {
                        g_isAttack = true;
                        g_ani.SetBool("isAttack", true);
                        return;
                    }
                }


                //if (g_isAttack || InputManager.GetHorizontal() == 0)
                //{
                //    g_ani.SetBool("isWalk", false);
                //}
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else
        {
            g_ani.SetBool("isWalk", false);
        }
    }
    void G_PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            g_ani.SetBool("isAttack", false);
            g_isDash = true;
            g_ani.SetBool("isDash", true);
            g_rigid.velocity = Vector3.zero;
            gameObject.layer = 12;
            g_rigid.useGravity = false;
            InputManager.SetIsCanInput(false);
        }
        if (g_ani.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
        {

            if (g_ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                //Debug.Log("��");
                InputManager.SetIsCanInput(true);
                gameObject.layer = 10;
                g_isDash = false;
                g_rigid.useGravity = true;
                g_ani.SetBool("isDash", false);
            }
        }
        
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
        if (g_curJump ==0 &&!g_isDash && Input.GetKeyDown(InputManager.GetJumpKey()))
        {
            g_ani.SetBool("isAttack", false);
            g_rigid.velocity = jumpPow;
            Debug.Log(g_curJump);
            g_curJump+=1;
        }
        if (g_curJump < g_maxJump && g_jumpTime <= jumpTimer && !g_isDash && Input.GetKey(InputManager.GetJumpKey()))
        {
            g_rigid.velocity = jumpPow;
            g_jumpTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(InputManager.GetJumpKey()))
        {
            //g_rigid.velocity = g_rigid.velocity;
            //g_jumpTime = 0;
            g_curJump += 1;
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
            mp.Gun_Shoot_Bullet();

        }
    }
    public void IsCanInputTrue()
    {
        InputManager.SetIsCanInput(true);
    }
    public void isCanInputFalse()
    {
        InputManager.SetIsCanInput(false);
    }
    public void MP_2158_AttackEnd()
    {
        g_ani.SetBool("isAttack", false);
    }
    void G_CheckWallAndGroundCollision()
    {
        // �÷��̾��� ��ġ�� ������ ������� ����ĳ��Ʈ�� �߻��Ͽ� �浹 �˻�
        RaycastHit hitInfo;
        g_isFloor = Physics.Raycast(g_floorCheck.position, Vector3.down, out hitInfo, 0.1f);
        g_isWall = Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.5f);
        if (Physics.Raycast(g_floorCheck.position, Vector3.down, out hitInfo, 0.1f))
        {
            // �ٴڰ� �浹

        }
        if (g_isFloor)
        {
            //Debug.Log("�÷��̾ �ٴڿ� ����ֽ��ϴ�.");
            g_ani.SetBool("isJump", false);
            g_jumpTime = 0;
            g_curJump = 0;
            g_isJump = false;
        }
        else
        {
            g_isJump = true;
        }

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 0.5f))
        {
            // ���� �浹
            //Debug.Log("�÷��̾ ���� ����ֽ��ϴ�.");
        }
    }
}
