using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Boss : Enemy
{
	public int NumShots = 10;
	public float TimeBetweenWaves;
	public float TimeBetweenShots;
	public float BulletSpeed = 80F;

	private GameObject _playerObj;
	public GameObject bossPrefab;
	public float minSize = 0.20f;
	private bool _shieldUp = true;
	private float minX;
	private float maxX;
	private float startX;
	

	// Use this for initialization
	void Start ()
	{
		base.Start();
		speed = speed / 2;
		_playerObj = GameObject.FindGameObjectWithTag("Player");
		StartCoroutine (ShootPlayer ());
		startX = transform.position.x;
		maxX = startX + 5F;
		minX = startX - 5F;
	}
	

	IEnumerator ShootPlayer()
	{
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
		yield return new WaitForSeconds(TimeBetweenWaves);
		while (true)
		{
			_shieldUp = false;
			
			for (int i = 0; i < NumShots; i++)
			{
				GameObject bulletObj = Instantiate(bullet, transform.position, Quaternion.identity);
                Bullet bulletScript = bulletObj.GetComponent(typeof(Bullet)) as Bullet;
                Rigidbody2D bulletBody = bulletObj.GetComponent<Rigidbody2D>();
				
				bulletScript.TargetTag = "Player";
				Vector3 direction = (_playerObj.transform.position - transform.position + Vector3.up * 3).normalized;
				
				
                bulletBody.velocity = direction * BulletSpeed;
				yield return new WaitForSeconds(TimeBetweenShots);
			}
			_shieldUp = true;
			yield return new WaitForSeconds(TimeBetweenWaves);
		}
	}

	public override void TakeDamage(float baseDamage, Color sourceColor)
	{
		if (true || !_shieldUp)
		{
            base.TakeDamage(baseDamage, sourceColor);
		}
		Debug.Log("Testingladflaksjdf");
	}
	
	// Update is called once per frame
	public override void Update ()
	{

		float moveSpeed = _shieldUp ? -speed : speed;
		float x = transform.position.x;
		if (x > minX && x < maxX)
		{
            rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
		}
		if (GameManager.Instance.IsDead) {
			Destroy (this.gameObject);
		}
	}

	public override void Die()
	{
		if (transform.localScale.y > minSize) 
		{
			GameObject clone1 = Instantiate(bossPrefab, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Quaternion.identity);
			GameObject clone2 = Instantiate(bossPrefab, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Quaternion.identity);

			clone1.transform.localScale = new Vector3 (transform.localScale.x * 0.5f, transform.localScale.y * 0.5f, transform.localScale.z);
			clone2.transform.localScale = new Vector3 (transform.localScale.x * 0.5f, transform.localScale.y * 0.5f, transform.localScale.z);

		}

		Destroy(this.gameObject);
		for (int i = 0; i < 3; i++)
		{
			GameObject ammoObj = (GameObject)(Instantiate (Ammo, transform.position, Quaternion.identity));
			Ammo ammo = (Ammo) ammoObj.GetComponent(typeof(Ammo));
			ammo.Color = Color;
		}
	}

}
