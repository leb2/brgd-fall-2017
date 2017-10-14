using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Color { Red, Blue, Green };

public class Enemy : MonoBehaviour
{
	// Maps each color the color that it has advantage over
	private IDictionary<Color, Color> advantageCircle;
	
	public Color color;
	public float maxHealth = 50F;
	public float advantageFactor = 2F;

	private float currentHealth;

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
