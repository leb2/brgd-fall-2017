using System.Collections;
using System.Collections.Generic;
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

	// Use this for initialization
	void Start ()
	{
		base.Start();
		_playerObj = GameObject.FindGameObjectWithTag("Player");
		StartCoroutine (ShootPlayer ());
	}


	IEnumerator ShootPlayer()
	{
		yield return new WaitForSeconds(TimeBetweenWaves);
		while (true)
		{
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
			yield return new WaitForSeconds(TimeBetweenWaves);
		}
	}
	
	// Update is called once per frame
	void Update () {
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
	}

}
