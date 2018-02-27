using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{

	public float playerRange;
	public GameObject projectile;
	public Player_Controller player;
	public Transform launcher;

	public int burstShots = 3;
	public float timeBetweenBurst = 0.5f;
	public float shootCooldown = 2f;
	private bool canShoot = true;


	// Use this for initialization
	void Start ()
	{
		player = FindObjectOfType<Player_Controller> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.DrawLine (new Vector3 (transform.position.x - playerRange, transform.position.y, transform.position.z), new Vector3 (transform.position.x + playerRange, transform.position.y, transform.position.z));

	}

	void OnTriggerStay2D (Collider2D coll)
	{
		// If the player is within shooting range
		if (coll.isTrigger != true && coll.CompareTag ("Player"))
		{
			if (canShoot)
			{
				Burst ();
			}
		}
	}

	void Burst ()
	{
		canShoot = false;

		StartCoroutine (shoot (timeBetweenBurst, shootCooldown));
		//bigPause (shootCooldown);
	}

	IEnumerator shoot (float shortPause, float longPause)
	{
		for (int i = 0; i < burstShots; i++)
		{
			Fire ();
			yield return new WaitForSeconds (shortPause);
		}
		yield return new WaitForSeconds (longPause);
		canShoot = true;
	}

	void Fire ()
	{
		Instantiate (projectile, launcher.position, launcher.rotation);
	}
}
