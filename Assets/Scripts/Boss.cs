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
				Vector3 direction = (_playerObj.transform.position - transform.position).normalized;
				
				
                bulletBody.velocity = direction * BulletSpeed;
				yield return new WaitForSeconds(TimeBetweenShots);
			}
			yield return new WaitForSeconds(TimeBetweenWaves);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
