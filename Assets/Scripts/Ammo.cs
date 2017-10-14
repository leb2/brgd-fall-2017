﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
	public Color Color;
	public GameObject Player;

	public float InitialDisperseSpeed = 1F;
	public float Acceleration = 0.01F;
	public float DecelerationFactor = 1.2F;
	
	private float _speed = 0.0F;
	private Vector3 _velocity;
	

	// Use this for initialization
	void Start () {
		Vector2 dir = Random.insideUnitCircle.normalized;
		Vector3 initialDirection = new Vector3(dir.x, dir.y, 0);
		_velocity = initialDirection * InitialDisperseSpeed;
	}
	
	void Update ()
	{
		transform.position += _velocity;
		_velocity /= DecelerationFactor;
			
		Vector3 direction = Player.transform.position - transform.position;
		transform.position += direction.normalized * _speed;
		
		_speed += Acceleration;
		Debug.Log(_speed);
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
            PlayerMovement player = (PlayerMovement) other.gameObject.GetComponent(typeof(PlayerMovement));
			player.addAmmo(Color, 1);
            Destroy(this.gameObject);
		}
	}
}
