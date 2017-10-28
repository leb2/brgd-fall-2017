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

	public Rigidbody2D rigidbody;

	private PlayerMovement _playerScript;
	public bool isFlyingEnemy;

	//enemy movement 

	private float _currentHealth;
	public GameObject Ammo;
	private float _distToGround;
	public LayerMask groundLayers;
	private Collider2D _collider;
	
	// The time after attacking the player which it can move again
	public float MoveDelay = 0.5F;
	private float _canMoveTimer = 0F;
	
	// Use this for initialization
	void Start () {
		_currentHealth = this.MaxHealth;
		_playerObj = GameObject.FindGameObjectWithTag("Player");
		_target = _playerObj.transform;
		_collider = GetComponent<Collider2D>();
		_distToGround = _collider.bounds.extents.y;
		
		_playerScript = _playerObj.GetComponent(typeof(PlayerMovement)) as PlayerMovement;
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	private bool IsGrounded()
	{
		float xRange = _collider.bounds.extents.x;
		Vector2 top_left = new Vector2(transform.position.x - xRange, transform.position.y - _distToGround);
		Vector2 bot_right = new Vector2(transform.position.x + xRange, transform.position.y - _distToGround - 0.1F);
		return Physics2D.OverlapArea(top_left, bot_right, groundLayers);    
	}

	// Update is called once per frame
	void Update()
	{

		_canMoveTimer -= Time.deltaTime;
		if (_canMoveTimer > 0) return;
			
		//Get the distance to the target & check if its close enough to chase
		float distToTarget = Vector3.Distance(transform.position, _target.position);
        Vector3 targetDirection = _target.position - transform.position;

		if ((distToTarget < chaseRange) & (distToTarget > 0.7f))
		{

			if (isFlyingEnemy)
			{
				//turn towards target and chase it
				float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
				//quaternion to find desired rotation
				Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180);
				//move enemy
				transform.Translate(Vector3.up * Time.deltaTime * speed);
			}
			else
			{
				if (Mathf.Abs(transform.position.y - _target.position.y) < 1.5F)
				{
                    if (IsGrounded())
                    {
                        float direction = Mathf.Sign(targetDirection.x);
                        rigidbody.velocity = Vector2.right * speed * direction;
                    }
				}
			}
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
			ammo.Color = Color;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			_canMoveTimer = MoveDelay;
			
			_playerScript.takeDamage();
			
			// Add recoil on collision
			Vector2 difference = (transform.position - other.transform.position).normalized;
			rigidbody.AddForce(difference * 45F, ForceMode2D.Impulse);
		}
	}
}
