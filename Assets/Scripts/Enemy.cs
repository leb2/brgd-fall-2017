using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Color { Red, Blue, Green };

public class Enemy : MonoBehaviour
{
	// Maps each color the color that it has advantage over
	public static IDictionary<Color, Color> AdvantageCircle =new Dictionary<Color, Color>()
	{
		{Color.Red, Color.Green},
		{Color.Green, Color.Blue},
		{Color.Blue, Color.Red}
	};
	
	public Color Color;
	public float MaxHealth = 50F;
	public float AdvantageFactor = 2F;
	public int NumAmmoDrop = 3;

	public GameObject bullet;
	
	private GameObject _playerObj;
	private Transform _target;
	
	public float chaseRange;
	public float speed;

	//enemy movement 

	private float _currentHealth;
	public GameObject Ammo;
	
	// Use this for initialization
	void Start () {
		_currentHealth = this.MaxHealth;
		_playerObj = GameObject.FindGameObjectWithTag("Player");
		_target = _playerObj.transform;

		//rigidbody = GetComponent<Rigidbody2D>();


	}

	// Update is called once per frame
	void Update () {
		//Get the distance to the target & check if its close enough to chase
		float distToTarget = Vector3.Distance (transform.position, _target.position);

		Debug.Log("Enemy movement");

		if ((distToTarget < chaseRange) & (distToTarget > 0.7f)) { 
			//turn towards target and chase it
			Vector3 targetDirection = _target.position - transform.position;
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
	
	public void TakeDamage(float baseDamage, Color sourceColor)
	{
		Debug.Log("Taking damage: " + baseDamage);
		Debug.Log("Health remaining: " + this._currentHealth);
		float damage;
		if (this.Color == sourceColor) {
			damage = baseDamage;
		} else if (Enemy.AdvantageCircle[sourceColor] == this.Color) {
			damage = baseDamage * this.AdvantageFactor;
			
		} else {
			damage = baseDamage / this.AdvantageFactor;
		}

		this._currentHealth -= damage;
		if (this._currentHealth <= 0)
		{
			this.Die();
		}
	}

	public void Die()
	{
		Destroy(this.gameObject);
		for (int i = 0; i < NumAmmoDrop; i++)
		{
			GameObject ammoObj = (GameObject)(Instantiate (Ammo, transform.position, Quaternion.identity));
			Ammo ammo = (Ammo) ammoObj.GetComponent(typeof(Ammo));
			ammo.Player = _playerObj;
			ammo.Color = Color;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			Debug.Log("Player detected");
		}
	}
		
}
