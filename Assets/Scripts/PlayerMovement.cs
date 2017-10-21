using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public float speed = 10;
	public float jumpSpeed = 10;
	
	public LayerMask groundLayers;
	public Text AmmoText;
	public Text CurrentAmmoText;
	
	public Rigidbody2D rigidbody;
	public CompositeCollider2D collider;
	
	private float jumpInterval = 0.2F;
	private float nextJump = 0.0F;
	private float distToGround;
	private IDictionary<Color, int> _ammoRemaining;
	private Color _selectedColor = Color.Blue;

	//player health
	public GameObject bullet;
	public float MaxHealth = 100f;
	public float currentHealth; 
	public float healthDamage = 20f;
	public RectTransform healthBar;

	private void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		Collider2D collider = GetComponent<Collider2D>();
		distToGround = collider.bounds.extents.y;
		
		_ammoRemaining = new Dictionary<Color, int>()
		{
			{Color.Red, 100},
			{Color.Green, 100},
			{Color.Blue, 100}
		};
		UpdateAmmoText();

		this.currentHealth = this.MaxHealth;
	}

	public void AddAmmo(Color color, int amount)
	{
		_ammoRemaining[color] += amount;
	}

	private bool IsGrounded()
	{
		Vector2 top_left = new Vector2(transform.position.x - 0.1F, transform.position.y - distToGround);
		Vector2 bot_right = new Vector2(transform.position.x + 0.1F, transform.position.y - distToGround - 0.1F);
		return Physics2D.OverlapArea(top_left, bot_right, groundLayers);    
	}
	
	
	private void Update()
	{
		float moveVertical = Input.GetAxisRaw("Vertical");
		float moveHorizontal = Input.GetAxisRaw("Horizontal");

		float yVelocity = rigidbody.velocity.y;
		if (moveVertical == 1 && IsGrounded() && Time.time > nextJump)
		{
			nextJump = Time.time + jumpInterval;
			yVelocity = jumpSpeed;
		}

		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			_selectedColor = Enemy.AdvantageCircle[_selectedColor];
			switch (_selectedColor)
			{
				case Color.Blue:
					CurrentAmmoText.text = "Ammo: Blue";
					break;
				case Color.Green:
					CurrentAmmoText.text = "Ammo: Green";
					break;
                case Color.Red:
	                CurrentAmmoText.text = "Ammo: Red";
	                break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		rigidbody.velocity = new Vector2(moveHorizontal * speed, yVelocity);

		//bullet functionality
		int currentAmmoRemaining = _ammoRemaining[_selectedColor];
		
		if (Input.GetKeyDown (KeyCode.Space) && currentAmmoRemaining > 0)
		{
			_ammoRemaining[_selectedColor] -= 1;
			GameObject b = (GameObject)(Instantiate (bullet, transform.position, Quaternion.identity));

			b.GetComponent<Rigidbody2D> ().AddForce (transform.up * 1000);
			b.GetComponent<Rigidbody2D> ().AddForce (transform.right * 1000);

			UpdateAmmoText();

			Destroy(b, 2);
		}
	}

	private void UpdateAmmoText()
	{
			AmmoText.text = "Blues: " + _ammoRemaining[Color.Blue].ToString() + "\n" +
							"Red: " + _ammoRemaining[Color.Red].ToString() + "\n" +
							"Green: " + _ammoRemaining[Color.Green].ToString();
	}


	public void takeDamage()
	{
		Debug.Log ("taking damage");
		currentHealth -= healthDamage;
		if (currentHealth < 0.0) {
			this.dies ();
		}
		healthBar.sizeDelta = new Vector2(currentHealth/100f, healthBar.sizeDelta.y);
	}

	public void dies()
	{
		Destroy (this.gameObject);
	}
}
