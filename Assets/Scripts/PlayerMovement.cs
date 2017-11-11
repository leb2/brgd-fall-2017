using System;using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public float speed = 10F;
	public float jumpSpeed = 10F;
	public float bulletSpeed = 1F;
	
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
	public GameObject ammotail;
	
	public float MaxHealth = 100f;
	public float currentHealth; 
	public float healthDamage = 20f;
	
	public RectTransform healthBar;
	public List<Color> Ammo = new List<Color>();
	private int _tailSize = 0;
	
	// Points to last ammo in the ammo tail
	private GameObject _lastTail = null;
	
	private void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		Collider2D collider = GetComponent<Collider2D>();
		distToGround = collider.bounds.extents.y;
		
		_ammoRemaining = new Dictionary<Color, int>()
		{
			{Color.Red, 2},
			{Color.Green, 2},
			{Color.Blue, 2}
		};
		UpdateAmmoText();
		this.currentHealth = this.MaxHealth;
		GameManager.Instance.IsDead = false;
		initializeAmmoTail();
	}
	
	public void AddAmmo(Color color, int amount)
	{
		_ammoRemaining[color] += amount;
		_ammoRemaining[color] = Math.Min(_ammoRemaining[color], 10);
		for (int i = 0; i < amount; i++)
		{
            Ammo.Add(color);
		}

		if (color == _selectedColor)
		{
            Rigidbody2D connectedBody;
            Vector3 spawnLocation;
			GameObject target;
            _tailSize += 1;
			
			// Create a new tail element and have it "follow" the previous end of the tail
            if (_lastTail == null) // No last tail, follow player instead
            {
	            target = gameObject;
                spawnLocation = transform.position;
            }
            else
            {
	            target = _lastTail;
                spawnLocation = _lastTail.transform.position;
            }
            GameObject ammoTailUnit = (GameObject)(Instantiate (ammotail, spawnLocation, Quaternion.identity));
			AmmoTail ammoScript = ammoTailUnit.GetComponent(typeof(AmmoTail)) as AmmoTail;

			if (_lastTail != null) // Adding new tail element to existing tail
			{
				ammoScript.JumpThreshold = ammoScript.JumpThreshold / 2;
			}
			else
			{
				ammoScript.isHead = true;
			}
			ammoScript.target = target;
            _lastTail = ammoTailUnit;
		}
		
		UpdateAmmoText();
	}
	
	private void initializeAmmoTail()
	{
		// Destroy previous tail
		GameObject head = _lastTail;
		while (head != null && head != gameObject)
		{
			Destroy(head);
			AmmoTail ammoScript = head.GetComponent(typeof(AmmoTail)) as AmmoTail;
			head = ammoScript.target;
		}
		
		Color color = _selectedColor;
		int number = _ammoRemaining[color];
		Vector3 spawnLocation = transform.position;
		GameObject target = gameObject;
		bool first = true;
		for (int i = 0; i < number; i++)
		{
            GameObject ammoTailUnit = (GameObject)(Instantiate (ammotail, spawnLocation, Quaternion.identity));
			AmmoTail ammoScript = ammoTailUnit.GetComponent(typeof(AmmoTail)) as AmmoTail;
			ammoScript.target = target;

			if (!first)
			{
				ammoScript.JumpThreshold = ammoScript.JumpThreshold / 2;
			}
			else
			{
				ammoScript.isHead = true;
			}
			
			spawnLocation = new Vector3(spawnLocation.x - (i + 1), spawnLocation.y, spawnLocation.z);
			target = ammoTailUnit;
			_lastTail = ammoTailUnit;
            first = false;
		}
	}

	
	private void SwitchAmmo(Color color)
	{
		_selectedColor = color; 
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
		initializeAmmoTail();
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

		rigidbody.velocity = new Vector2(moveHorizontal * speed, yVelocity);
		// TODO: Change sprite rotation not transform rotation
		if (moveHorizontal < 0)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}
		else if (moveHorizontal > 0)
		{
			GetComponent<SpriteRenderer>().flipX = false;
		}
		

		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			SwitchAmmo(Enemy.AdvantageCircle[_selectedColor]);
		}

		//bullet functionality
		int currentAmmoRemaining = _ammoRemaining[_selectedColor];
		if (Input.GetKeyDown (KeyCode.Mouse0) && currentAmmoRemaining > 0)
		{
			shoot();
		}
	}

	private void shoot()
	{
        _ammoRemaining[_selectedColor] -= 1;
        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        
        GameObject b = (GameObject)(Instantiate (bullet, transform.position + direction * 1.2F, Quaternion.identity));
        Bullet bulletScript = b.GetComponent(typeof(Bullet)) as Bullet;
        bulletScript.Color = _selectedColor;
        Rigidbody2D bulletBody = b.GetComponent<Rigidbody2D>();
        bulletBody.velocity = direction * bulletSpeed;
        bulletScript.TargetTag = "Enemy";
        UpdateAmmoText();
		
		// Remove ammo from ammo tail
		AmmoTail ammoTailScript = _lastTail.GetComponent(typeof(AmmoTail)) as AmmoTail;
		Destroy(_lastTail);
		_tailSize -= 1;
		
		_lastTail = ammoTailScript.isHead ? null : ammoTailScript.target;
		
        Destroy(b, 2);
	}

	private void UpdateAmmoText()
	{
			AmmoText.text = "Blues: " + _ammoRemaining[Color.Blue].ToString() + "\n" +
							"Red: " + _ammoRemaining[Color.Red].ToString() + "\n" +
							"Green: " + _ammoRemaining[Color.Green].ToString();
	}


	public void takeDamage()
	{
		currentHealth -= healthDamage;
		if (currentHealth <= 0.0) {
			this.dies ();
		}
		healthBar.sizeDelta = new Vector2(currentHealth/100f, healthBar.sizeDelta.y);
	}

	public void dies()
	{
		Destroy (this.gameObject);
		GameManager.Instance.IsDead = true;
	}
}
