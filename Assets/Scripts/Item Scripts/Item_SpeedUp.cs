using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_SpeedUp : MonoBehaviour
{

	public float duration;
	public float speedMultiplier;

	[HideInInspector]
	Player_Controller player;

	[HideInInspector]
	public bool wasCollected;

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
			if (!player.isSpedUpBuff)
				player.StartCoroutine (player.speedUp (duration, speedMultiplier));
			Destroy (this.gameObject);
		}
	}
}
