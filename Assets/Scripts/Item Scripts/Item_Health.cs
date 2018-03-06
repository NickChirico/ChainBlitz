using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Health : MonoBehaviour
{
	public int healAmount;
	Player_Controller player;

	//public RoundManager roundManager;

	void Start ()
	{
		player = FindObjectOfType<Player_Controller> ();

		// Initiate the despawn timer
	}

	void Update ()
	{
		
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		// You need to hit the health packs
		if (coll.gameObject.tag == "Player")
		{
			//Effect
			player.health.CurrentValue += healAmount;


			Destroy (this.gameObject);
		}
	}
}
