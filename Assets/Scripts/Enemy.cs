using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Color { Red, Blue, Green };

public class Enemy : MonoBehaviour
{
	// Maps each color the color that it has advantage over
	private IDictionary<Color, Color> _advantageCircle;
	
	public Color Color;
	public float MaxHealth = 50F;
	public float AdvantageFactor = 2F;
	public int NumAmmoDrop = 3;

	public GameObject bullet;

	private Rigidbody2D rigidbody;

	//enemy movement 
	public Transform player;
	int MoveSpeed = 4;
	int MaxDist = 10;
	int MinDist = 20;

	private float _currentHealth;
	public GameObject Ammo;
	
	// Use this for initialization
	void Start () {
		this._currentHealth = this.MaxHealth;

		this._advantageCircle = new Dictionary<Color, Color>()
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
	
	public void takeDamage(float baseDamage, Color sourceColor)
	{
		Debug.Log("Taking damage: " + baseDamage);
		Debug.Log("Health remaining: " + this._currentHealth);
		float damage;
		if (this.Color == sourceColor) {
			damage = baseDamage;
		} else if (this._advantageCircle[sourceColor] == this.Color) {
			damage = baseDamage * this.AdvantageFactor;
			
		} else {
			damage = baseDamage / this.AdvantageFactor;
		}

		this._currentHealth -= damage;
		if (this._currentHealth <= 0)
		{
			this.die();
		}
	}

	public void die()
	{
		Destroy(this.gameObject);
		for (int i = 0; i < NumAmmoDrop; i++)
		{
			GameObject ammoObj = (GameObject)(Instantiate (Ammo, transform.position, Quaternion.identity));
			Ammo ammo = (Ammo) ammoObj.GetComponent(typeof(Ammo));
			ammo.Color = Color;
		}
	}
		
}
