using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
	public Color Color;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Debug.Log("Collided with player", other.gameObject);
            PlayerMovement player = (PlayerMovement) other.gameObject.GetComponent(typeof(PlayerMovement));
			player.addAmmo(Color, 1);
            Destroy(this.gameObject);
		}
	}
}
