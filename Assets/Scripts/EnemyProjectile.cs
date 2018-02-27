using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

	public float speed;
	public float force = 50;
	public Player_Controller player;
	//public GameObject impactEffect;
	public float rotationSpeed;
	public int damage;
	private Rigidbody2D rb;

	Vector3 angle;

	// Use this for initialization
	void Start ()
	{
		rb = this.GetComponent<Rigidbody2D> ();
		player = FindObjectOfType<Player_Controller> ();

		angle = (player.transform.position - this.transform.position).normalized;
	}
	
	// Update is called once per frame
	void Update ()
	{
		rb.AddForce (angle * force);
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		/*if (coll.tag == "Player")
		{
			
		}*/

		//Instanciate(impactEffect, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.isTrigger && coll.CompareTag ("Player"))
		{
			// If the player attacks the projectile, it breaks

			//Instanciate(impactEffect, transform.position, transform.rotation);
			Destroy (this.gameObject);
		}

	}
}
