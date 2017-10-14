using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{

    public enum Color { Red, Blue, Green };
	
	// Maps each color the color that it has advantage over
	private IDictionary<Color, Color> advantageCircle;
	
	public Color color;
	public float maxHealth = 50F;
	public float advantageFactor = 2F;

	private float currentHealth;

	// Use this for initialization
	void Start ()
	{
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
	
	float damageMultiplier(float baseDamage, Color sourceColor)
	{
		if (this.color == sourceColor) {
			return baseDamage;
			
		} else if (this.advantageCircle[sourceColor] == this.color) {
			return baseDamage * this.advantageFactor;
			
		} else {
			return baseDamage / this.advantageFactor;
		}
	}
}
