using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Color { Red, Blue, Green };

public class Enemy : MonoBehaviour
{
	// Maps each color the color that it has advantage over
	private IDictionary<Color, Color> advantageCircle;
	
	public Color color;
	public float maxHealth = 50F;
	public float advantageFactor = 2F;

	private float currentHealth;

	public GameObject bullet;

	public Transform target;
	public float chaseRange;
	public float speed;

	//enemy movement 

	// Use this for initialization
	void Start () {
		this.currentHealth = this.maxHealth;

		this.advantageCircle = new Dictionary<Color, Color>()
		{
			{Color.Red, Color.Green},
			{Color.Green, Color.Blue},
			{Color.Blue, Color.Red}
		};

	} 

	// Update is called once per frame
	void Update () {
		//Get the distance to the target & check if its close enough to chase
		float distToTarget = Vector3.Distance (transform.position, target.position);

		if ((distToTarget < chaseRange) & (distToTarget > 0.7f)) { 
			//turn towards target and chase it
			Vector3 targetDirection = target.position - transform.position;
			float angle = Mathf.Atan2 (targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
			//quaternion to find desired rotation
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, q, 180);
			//move enemy
			transform.Translate( Vector3.up * Time.deltaTime * speed);
		}
	}

	void FixedUpdate() {

	}
	
	public void takeDamage(float baseDamage, Color sourceColor)
	{
		Debug.Log("Taking damage: " + baseDamage);
		Debug.Log("Health remaining: " + this.currentHealth);
		float damage;
		if (this.color == sourceColor) {
			damage = baseDamage;
			
		} else if (this.advantageCircle[sourceColor] == this.color) {
			
			damage = baseDamage * this.advantageFactor;
			
		} else {
			
			damage = baseDamage / this.advantageFactor;
		}

		this.currentHealth -= damage;
		if (this.currentHealth <= 0) {
			Destroy(this.gameObject);
		}
	}
		
}
