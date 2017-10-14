using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Enemy : MonoBehaviour
{

    public enum Color { Red, Blue, Green };
	
	// Maps each color the color that it has advantage over
	private IDictionary<Color, Color> advantageCircle;
	
	public Color color;
	public float maxHealth = 50F;
	public float advantageFactor = 2F;

	private float currentHealth;

	public GameObject bullet;

	private Rigidbody2D rigidbody;

	//enemy movement 
	public Transform player;
	int MoveSpeed = 4;
	int MaxDist = 10;
	int MinDist = 20;

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

		rigidbody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
//		transform.LookAt(Player);
//
//		float distance = Vector3.Distance(transform.position, player.position);
//
//		if (distance <= MinDist) {
//			transform.position += transform.forward * MoveSpeed * Time.deltaTime;
//			transform.position = Vector3.SmoothDamp(transform.position, player.position, ref smoothVelocity, smoothTime);
//
//		}
	}
	
	float damageMultiplier(float baseDamage, Color sourceColor)
	{
		if (this.color == sourceColor) {
			return baseDamage;
			
		} else if (this.advantageCircle [sourceColor] == this.color) {
			return baseDamage * this.advantageFactor;
			
		} else {
			return baseDamage / this.advantageFactor;
		}
	}
		
}
