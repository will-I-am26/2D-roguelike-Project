using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    float horizontalMove = 0;
    public float runSpeed = 40;
    bool jump = false;
    bool crouch = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if(Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if(Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

    }

    private void FixedUpdate()
    {
        // move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void onLanding()
    {
        animator.SetBool("isJumping", false);
    }
}
