# -*- coding: utf-8 -*-

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRender; // use to filp player left & right...

    private float runSpeed = 2f;
    private float jumpSpeed = 5f;

    bool isGrounded;

    [SerializeField]
    Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))// if the player hits the ground
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //Mapping keyboard

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.velocity = new Vector2(runSpeed, rb.velocity.y); // Added velocity

            if(isGrounded)
            animator.Play("Player_run");// If the player presses the right arrow then run

            spriteRender.flipX = false;
        }
        else if(Input.GetKey("a") || Input.GetKey("left"))
        {
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);


                animator.Play("Player_run");

            spriteRender.flipX = true; // if left arrow is pressed the face left
        }

        else // If player is not pressing right or left then back to Idle
        {
            if (isGrounded)
                animator.Play("Player_idle");

            rb.velocity = new Vector2(0, rb.velocity.y); // Velocity is 0 if its not moving
        }


        if (Input.GetKey("space") && isGrounded)// Jump Movement
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

            animator.Play("Player_jump");
        }

        //crouching

    }
}