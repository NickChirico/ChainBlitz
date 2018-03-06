using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
	Player_Controller player;
	public int ChargerDamage;
	public int ProjectileDamage;

	// Use this for initialization
	void Start ()
	{
		player = GetComponent<Player_Controller> ();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnCollisionEnter2D (Collision2D collisionInfo)
	{
		if (collisionInfo.gameObject.tag == "FlyingEnemy")
		{
			//TakeDamage (15); Dont take damage. Instead, refresh double jump. Lets try it.
			player.jumps = player.numJumps;
		}

		if (collisionInfo.gameObject.tag == "ChargerEnemy")
		{
			//TakeDamage (15);
			player.TakeDamage (ChargerDamage);
		}

		if (collisionInfo.gameObject.tag == "Projectile")
		{
			//TakeDamage (6);
			player.TakeDamage (ProjectileDamage);
		}

	}
}
