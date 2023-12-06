using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob08Ctrl : MonoBehaviour
{
    public float speed = 1.0f;
    public Flamethrower [] Flamethrowers;
    public float bulletSpeed = 5;
    public float particleDalay = 0.5f;

    private int i = 0;

   
    Animator anim;
    //CharacterController controller;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //controller = GetComponent<CharacterController>();
        anim.SetFloat("speedMultiplier", speed);
        StopShoot();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                anim.SetBool("turnLeft", true);
                anim.SetBool("turnRight", false);
            }
            if (Input.GetAxis("Horizontal") < -0.1f)
            {
                anim.SetBool("turnRight", true);
                anim.SetBool("turnLeft", false);
            }
        }
        else
        {
            anim.SetBool("turnLeft", false);
            anim.SetBool("turnRight", false);
        }

            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            {
                anim.SetFloat("Turns", Input.GetAxis("Horizontal"));
            }
            else
            {
                anim.SetFloat("Turns", 0);
            }

            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
            {
                anim.SetFloat("Speed", Input.GetAxis("Vertical"));
            }
            else
            {
                anim.SetFloat("Speed", 0);
            }

            if (Input.GetKeyDown("e"))
            {
               // anim.SetBool("shoot", true);
                Shoot();

           
            }
            if (Input.GetKeyUp("e"))
            {
             //   anim.SetBool("shoot", false);
                StopShoot();
            }

            if ((Input.GetKeyDown("p"))) //death
            {
                anim.SetBool("die", true);
                this.enabled = false;
            } 

            if ((Input.GetKeyDown("u"))) //look around
            {
                anim.SetBool("look1", true);
            } else { anim.SetBool("look1", false); }

            if ((Input.GetKeyDown("o"))) //take damage 1
            {
                anim.SetBool("hitLeft", true);
            } else { anim.SetBool("hitLeft", false); }

            if ((Input.GetKeyDown("i"))) //take damage 2
            {
                anim.SetBool("hitRight", true);
            } else { anim.SetBool("hitRight", false); }        
    }

    void Shoot ()
    {
        for (int i = 0; i < Flamethrowers.Length; i++)
        {
            Flamethrowers[i].StartEmission();
        }
    }
    void StopShoot()
    {
        for (int i = 0; i < Flamethrowers.Length; i++)
        {
            Flamethrowers[i].StopEmission();
        }
    }
}

