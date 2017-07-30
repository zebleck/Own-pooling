using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    
    public float maxSpeed = 5.00f;
    public float jumpForce = 800f;
    public Transform groundCheck;

    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Solid"));

        anim.SetBool("grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
	}

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(h * maxSpeed, rb2d.velocity.y);

        if (jump)
        {
            rb2d.AddForce(Vector2.up * jumpForce);
            jump = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
