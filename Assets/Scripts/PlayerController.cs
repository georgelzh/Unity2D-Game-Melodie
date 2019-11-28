using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {

    public float speed;             //Floating point variable to store the player's movement speed.
    public float jumpForce;     //add jump force to the character


    Animator anim;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private Rigidbody2D rb2d;

    private bool facingRight = true;

    //here we want to check if the character is grounded
    private bool grounded;      //check if the character is on the ground 
    public Transform groundCheck;   //
    public float checkRadius;
    public LayerMask WhatIsGround;  //define which layer is ground

    private int extraJump;
    //public int extraJumpValue;
    // Use this for initialization
    void Start()
    {
      //  extraJump = extraJumpValue;
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, WhatIsGround);

        //Store the current horizontal input in the float moveHorizontal.
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move)); // check this line 

        //Use the two store floats to create a new Vector2 variable movement.
        rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);
        if (facingRight == false && move > 0)
        {
            flip();
        }
        else if (facingRight == true && move < 0)
        {
            flip();
        }

        //if (grounded && Input.GetKeyDown(KeyCode.W))
        //{
        //    rb2d.AddForce(new Vector2(rb2d.velocity.x, jumpForce));
        //}
    }
    void Update()
    {
        if (grounded == true)
        {
            extraJump = 1;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJump > 0)  //if up arrow is down, check if there are still extra jump,
        {
            rb2d.velocity = Vector2.up * jumpForce;              //jump first, then subtract extra jump
            extraJump--;
        }

        //if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)  //if up arrow is down, check if there are still extra jump,
        //{
        //    rb2d.velocity = Vector2.up * jumpForce;              //jump first, then subtract extra jump
        //    extraJump--;
        //}

    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}