using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DamageUp : MonoBehaviour
{
	public float duration;
	public float damageMultiplier;
	PlayerAttack player;

	[HideInInspector]
	public bool wasCollected;

	void Start ()
	{
		player = FindObjectOfType<PlayerAttack> ();
		
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
			player.StartCoroutine (player.damageUp (duration, damageMultiplier));
			Destroy (this.gameObject);
		}
	}
}
