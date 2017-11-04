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

	// Use this for initialization
	void Start () {
		Collider2D collider = GetComponent<Collider2D>();
		distToGround = collider.bounds.extents.y;
		rigidbody = GetComponent <Rigidbody2D>();
		_playerObj = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{

		float xSpeed = 0F;
		float ySpeed = rigidbody.velocity.y;
		
		if (transform.position.x + FollowDistance < target.transform.position.x)
		{
			xSpeed = Speed;
			
		} else if (transform.position.x - FollowDistance > target.transform.position.x)
		{
			xSpeed = -Speed;
		}
		if (transform.position.y + JumpThreshold < target.transform.position.y)
		{
			if (IsGrounded() && Time.time > nextJump)
			{
				nextJump = Time.time + jumpInterval;
				ySpeed = JumpSpeed;
			}
		}
        rigidbody.velocity = new Vector2(xSpeed, ySpeed);
	}
	
	private bool IsGrounded()
	{
		Vector2 top_left = new Vector2(transform.position.x - 0.1F, transform.position.y - distToGround);
		Vector2 bot_right = new Vector2(transform.position.x + 0.1F, transform.position.y - distToGround - 0.1F);
		return Physics2D.OverlapArea(top_left, bot_right, groundLayers);    
	}
}
