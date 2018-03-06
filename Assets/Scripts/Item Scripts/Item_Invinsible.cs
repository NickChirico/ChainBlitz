using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Invinsible : MonoBehaviour
{

	public int duration;
	[HideInInspector]
	Player_Controller player;

	[HideInInspector]
	public bool wasCollected;

	//public RoundManager roundManager;

	void Start ()
	{
		player = FindObjectOfType<Player_Controller> ();

	}

	void Update ()
	{

	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		// You need to hit the health packs
		if (coll.gameObject.tag == "Player")
		{
			wasCollected = true;
			player.StartCoroutine (player.invincibility (duration));
			Destroy (this.gameObject);
		}
	}
}
