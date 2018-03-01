using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

	public Player_Controller player;
	public int damage = 20;
	public bool countsForCombo = true;

	void Start()
	{
		player = FindObjectOfType<Player_Controller> ();	
	}

	void Update()
	{
		if (!countsForCombo)
		{
			// If it's the smash attack,
			damage = 30 + (player.currentCombo * 10);

		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.isTrigger != true && collision.CompareTag("ChargerEnemy"))
		{
			collision.SendMessageUpwards("Damage", damage);

			Debug.Log("Charger Enemy has been hit! for " + damage + " damage");

			if(countsForCombo)
				player.currentCombo++;
		}

		if (collision.isTrigger != true && collision.CompareTag("FlyingEnemy"))
		{
			collision.SendMessageUpwards("Damage", damage);

			Debug.Log("Flying Enemy has been hit! for " + damage + " damage");

			if(countsForCombo)
				player.currentCombo++;
		}

		if (collision.isTrigger != true && collision.CompareTag("Projectile"))
		{
			Debug.Log("Projectile has been hit! for " + damage + " damage");

			if(countsForCombo)
				player.currentCombo++;
		}
	}
}
