using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    Animator ani;
    float dir;

    // Start is called before the first frame update
    void Start()
    {
        ani=GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.GetHorizontal() != 0)
        {
            dir = InputManager.GetHorizontal();
            Debug.Log(dir);
            ani.SetFloat("Blend", -dir);
            ani.SetBool("isWalk", true);
        }
        else
        {
            ani.SetBool("isWalk", false);
        }
        ani.SetFloat("Blend", dir);
        ani.SetFloat("xDir", InputManager.GetMove().x);
        ani.SetFloat("yDir", InputManager.GetMove().y);
    }
}
