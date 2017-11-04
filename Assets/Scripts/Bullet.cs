using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public float Damage = 30F;
	public Color Color = Color.Blue;

	public Sprite blueSprite;
	public Sprite redSprite;
	public Sprite greenSprite;
	public Sprite EnemyBulletSprite;

	public String TargetTag = "Enemy";
	private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		rigidbody = GetComponent<Rigidbody2D>();
		
		if (TargetTag == "Player")
		{
			sr.sprite = EnemyBulletSprite;
		}
		else if (Color == Color.Blue)
		{
			sr.sprite = blueSprite;
		}
		else if (Color == Color.Red)
		{
			sr.sprite = redSprite;
		}
		else if (Color == Color.Green)
		{
			sr.sprite = greenSprite;
		}
	}
	
	void Update ()
	{
		transform.localRotation = Quaternion.Euler(rigidbody.velocity);
	}
	
	// void OnCollisionEnter2D(Collision2D other)
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag(TargetTag))
		{
			if ((TargetTag) == "Enemy")
			{
				Enemy otherEnemy = (Enemy) other.gameObject.GetComponent(typeof(Enemy));
                otherEnemy.TakeDamage(this.Damage, this.Color);
			}
			else if ((TargetTag) == "Player")
			{
				PlayerMovement player = (PlayerMovement) other.gameObject.GetComponent(typeof(PlayerMovement));
				player.takeDamage();
			}
			Destroy(gameObject);
		}
	}
}
