using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	bool facingRight = true;
	public float maxSpeed = 10f;
	Animator anim;
	public float jumpForce = 700f;

	bool grounded = true;
	public Transform groundCheck;
	float groundRadius = 1f;
	public LayerMask whatIsGround;


	void Start () 
	{
		anim = GetComponent<Animator> ();
	}
	void Update ()
	{
		if (grounded && Input.GetKeyDown (KeyCode.Space)) 
		{
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}
	}
	// Update is called once per frame
	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("Vspeed", rigidbody2D.velocity.y);




		float movementSpeed = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (movementSpeed));
		rigidbody2D.velocity = new Vector2 (movementSpeed * maxSpeed, rigidbody2D.velocity.y);
		if (movementSpeed > 0 && !facingRight)
			Flip ();
		else if (movementSpeed < 0 && facingRight) 
			Flip ();
	}
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
