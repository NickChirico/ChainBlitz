using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

	public int healAmount;
	public float respawnTime;
	public Player_Controller player;
	SpriteRenderer spriteRenderer;	
	Rigidbody2D rb;
	Collider2D coll;
	//public RoundManager roundManager;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rb = GetComponent<Rigidbody2D> ();
		coll = GetComponent<Collider2D> ();
		player = FindObjectOfType<Player_Controller> ();
	}
	
	void Update () 
	{
		// If the health kit is disabled when you beat a round, they all respawn
		/*if (roundManager.canIncrementRound)
			this.enabled = true;*/
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		// You need to hit the health packs
		if (coll.gameObject.tag == "Player")
		{
			player.health.CurrentValue += healAmount;

			// Go on cooldown, Respawn after cooldown or at the start of a new round
			//StartCoroutine(cooldown(respawnTime));
			isUsable(false);
		}
	}

	public void isUsable(bool boolean)
	{
		this.spriteRenderer.enabled = boolean;
		Physics2D.IgnoreCollision(this.coll, player.GetComponent<Collider2D>(), !boolean);
	}

	IEnumerator cooldown(float cd)
	{
		isUsable (false);
		yield return new WaitForSeconds (cd);
		isUsable (true);
	}
}
