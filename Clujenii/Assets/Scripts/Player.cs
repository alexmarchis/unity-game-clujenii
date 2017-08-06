﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.

	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
    public LayerMask groundLayer;

	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;

    void Awake()
	{
		anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }


	void Update()
	{
        Vector3 feetPosition = new Vector3();
        feetPosition.x = transform.position.x;
        feetPosition.z = transform.position.z;
        feetPosition.y = transform.position.y - (transform.localScale.y * 1.1f);
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, feetPosition, groundLayer);  

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
	}


	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");

        //sudden stop
        if(h == 0)
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat("Speed", Mathf.Abs(rb2D.velocity.x));

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rb2D.velocity.x < maxSpeed)
        {
            rb2D.AddForce(Vector2.right * h * moveForce);
        }

        if (Mathf.Abs(rb2D.velocity.x) > maxSpeed)
            rb2D.velocity = new Vector2(Mathf.Sign(rb2D.velocity.x) * maxSpeed, rb2D.velocity.y);

		if(h > 0 && !facingRight)
			Flip();
		else if(h < 0 && facingRight)
			Flip();

		if(jump)
		{
			anim.SetTrigger("Jump");

            rb2D.AddForce(new Vector2(0f, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
	}
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
