using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public float Damage = 30F;
	private Color Color = Color.Blue;

	// Use this for initialization
	void Start () {
		Debug.Log("Starting up");
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
            Enemy otherEnemy = (Enemy) other.gameObject.GetComponent(typeof(Enemy));
            otherEnemy.takeDamage(this.Damage, this.Color);
		}
	}
}
