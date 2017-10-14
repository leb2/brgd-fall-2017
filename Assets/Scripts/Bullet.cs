using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public float damage = 30F;
	private Color color = Color.Blue;

	// Use this for initialization
	void Start () {
		Debug.Log("Starting up");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
            Enemy otherEnemy = (Enemy) other.gameObject.GetComponent(typeof(Enemy));
            otherEnemy.takeDamage(this.damage, this.color);
		}
	}
}
