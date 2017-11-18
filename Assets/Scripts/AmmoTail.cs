using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmmoTail : MonoBehaviour
{
	
	public GameObject target;
	public float Speed = 10F;
	public float FollowDistance = 1;
	public float JumpThreshold = 5F;
	public LayerMask groundLayers;
	public float jumpInterval = 0.2F;
	public float JumpSpeed = 25F;
	
	private Rigidbody2D rigidbody;
	private float distToGround;
	private float nextJump = 0.0F;
	private GameObject _playerObj;
	public bool isHead = false;
	public Color color;

	// Use this for initialization
	void Start () {
		Collider2D collider = GetComponent<Collider2D>();
		distToGround = collider.bounds.extents.y;
		rigidbody = GetComponent <Rigidbody2D>();
		_playerObj = GameObject.FindGameObjectWithTag("Player");

		Animator animator = GetComponent<Animator>();
		if (color == Color.Red)
		{
            animator.Play("Red Animation");
		} else if (color == Color.Green)
		{
			animator.Play("Green Animation");
		}
		else
		{
			animator.Play("Blue Animation");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.y < -4F)
		{
			GetComponent<Collider2D>().isTrigger = false;
		}

		float xSpeed = 0F;
		float ySpeed = rigidbody.velocity.y;
		
		if (transform.position.x + FollowDistance < target.transform.position.x)
		{
			GetComponent<SpriteRenderer>().flipX = false;
			xSpeed = Speed;
			
		} else if (transform.position.x - FollowDistance > target.transform.position.x)
		{
			GetComponent<SpriteRenderer>().flipX = true;
			xSpeed = -Speed;
		}
		if (transform.position.y + JumpThreshold < target.transform.position.y)
		{
			Debug.Log("Trying to jump");
			Debug.Log("Is grounded: " + IsGrounded());
			if (IsGrounded() && Time.time > nextJump)
			{
				nextJump = Time.time + jumpInterval;
				ySpeed = JumpSpeed;
			}
		}

		// Fall through platforms when low enough
		if (transform.position.y - JumpThreshold / 2 > target.transform.position.y && transform.position.y > -4F && IsGrounded())
		{
			GetComponent<Collider2D>().isTrigger = true;
		}
		
        rigidbody.velocity = new Vector2(xSpeed, ySpeed);
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		GetComponent<Collider2D>().isTrigger = false;
	}
	
	private bool IsGrounded()
	{
		Vector2 top_left = new Vector2(transform.position.x - 0.3F, transform.position.y - distToGround);
		Vector2 bot_right = new Vector2(transform.position.x + 0.3F, transform.position.y - distToGround - 0.3F);
		return Physics2D.OverlapArea(top_left, bot_right, groundLayers);    
	}
}
