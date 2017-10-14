using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
	public Color Color;
	public GameObject Player;

	public float Acceleration = 0.05F;
	private float _speed = 0;
	

	// Use this for initialization
	void Start () {
		
	}
	
	void Update ()
	{
		Rigidbody2D body = this.gameObject.GetComponent<Rigidbody2D>();
		Vector3 direction = Player.transform.position - transform.position;
		
		transform.position = transform.position + direction.normalized * _speed;
		_speed += Acceleration;
		
		Debug.Log(Player.transform.position);
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
            PlayerMovement player = (PlayerMovement) other.gameObject.GetComponent(typeof(PlayerMovement));
			player.addAmmo(Color, 1);
            Destroy(this.gameObject);
		}
	}
}
