using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	public bool punch = false;               // Condition for whether the player should jump.

    public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
    public LayerMask groundLayer;
    public LayerMask enemy;

    private bool grounded = false;			// Whether or not the player is grounded.
    private int doublePunchCounter = 0;
	private Animator anim;					// Reference to the player's animator component.
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;

    //states
    private string punching = "TudorPunch";

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
        if (grounded)
        {
            anim.SetBool("Jump", false);
            if (Input.GetButtonDown("Jump"))
                jump = true;

            if (Input.GetButtonDown("Skill-1"))
            {
                if (!IsPunching())
                {
                    anim.SetTrigger("Punch");
                    StartDoublePunch();
                }
            }
        }
    }

	void FixedUpdate ()
	{
        if (IsPunching())
        {
            StopRunning();
            HandleDoublePunch();
        }
        else
        {
            HandleHorizontalMovement();

            if (jump)
            {
                anim.SetBool("Jump", true);

                rb2D.AddForce(new Vector2(0f, jumpForce));

                // Make sure the player can't jump again until the jump conditions from Update are satisfied.
                jump = false;
            }
        }
    }

    private void HandleHorizontalMovement()
    {
        float h = Input.GetAxis("Horizontal");

        //sudden stop
        if (h == 0)
        {
            StopRunning();
        }

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        anim.SetFloat("Speed", Mathf.Abs(rb2D.velocity.x));

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * rb2D.velocity.x < maxSpeed)
        {
            rb2D.AddForce(Vector2.right * h * moveForce);
        }

        if (Mathf.Abs(rb2D.velocity.x) > maxSpeed)
            rb2D.velocity = new Vector2(Mathf.Sign(rb2D.velocity.x) * maxSpeed, rb2D.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
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

    private void StartDoublePunch()
    {
        doublePunchCounter = 0;
    }

    private void HandleDoublePunch()
    {
        if(doublePunchCounter == 0
          || doublePunchCounter == 3)
        {
            Punch();
        }

        doublePunchCounter++;
    }

    private void Punch()
    {
        ContactFilter2D filter = new ContactFilter2D();
        Collider2D[] results = new Collider2D[2];
        Physics2D.OverlapCollider(boxCollider, filter, results);
        foreach(Collider2D collider in results)
        {
            if(collider != null)
            {
                if (collider.tag == "Enemy")
                {
                    collider.gameObject.GetComponent<Dummy>().Hit(-Math.Sign(transform.localScale.x));
                }
            }
        }
    }

    private void StopRunning()
    {
        rb2D.velocity = new Vector2(0, rb2D.velocity.y);
    }

    private bool IsPunching()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(punching);
    }
}
