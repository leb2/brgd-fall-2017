﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 10;
	public float jumpSpeed = 10;
	
	public LayerMask groundLayers;
	
	private Rigidbody2D rigidbody;
	
	private float jumpInterval = 0.2F;
	private float nextJump = 0.0F;
	private float distToGround;

	public GameObject bullet;

	private void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		Collider2D collider = GetComponent<Collider2D>();
		distToGround = collider.bounds.extents.y;
		Debug.Log("Distance to ground " + distToGround.ToString());
	}

	private bool IsGrounded()
	{
		Vector2 top_left = new Vector2(transform.position.x - 0.1F, transform.position.y - distToGround);
		Vector2 bot_right = new Vector2(transform.position.x + 0.1F, transform.position.y - distToGround - 0.1F);
		return Physics2D.OverlapArea(top_left, bot_right, groundLayers);    
	}
	
	
	private void Update()
	{
		float moveVertical = Input.GetAxisRaw("Vertical");
		float moveHorizontal = Input.GetAxisRaw("Horizontal");

		float yVelocity = rigidbody.velocity.y;
		if (moveVertical == 1 && IsGrounded() && Time.time > nextJump)
		{
			nextJump = Time.time + jumpInterval;
			yVelocity = jumpSpeed;
		}

		rigidbody.velocity = new Vector2(moveHorizontal * speed, yVelocity);

		//bullet functionality
		if (Input.GetKeyDown (KeyCode.Space))
		{
			GameObject b = (GameObject)(Instantiate (bullet, transform.position + transform.up*1.5f, Quaternion.identity));
			b.GetComponent<Rigidbody2D> ().AddForce (transform.up * 1000);
		}
	}
}
