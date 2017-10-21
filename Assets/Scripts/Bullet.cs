using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public float Damage = 30F;
	public Color Color = Color.Blue;

	public Sprite blueSprite;
	public Sprite redSprite;
	public Sprite greenSprite;

	// Use this for initialization
	void Start () {
		Debug.Log("Starting up");
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if (Color == Color.Blue)
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
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
            Enemy otherEnemy = (Enemy) other.gameObject.GetComponent(typeof(Enemy));
            otherEnemy.TakeDamage(this.Damage, this.Color);
		}
	}
}
