using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

	public int healthAmount;
	public Player_Controller player;

	void Start () {
		player = FindObjectOfType<Player_Controller> ();
	}
	
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		// You need to hit the health packs
		if (coll.isTrigger && coll.CompareTag ("Player"))
		{
			player.health += healthAmount;
			Destroy (this.gameObject);
		}
	}
}
