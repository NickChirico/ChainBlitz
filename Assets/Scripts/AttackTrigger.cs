using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

	Player_Controller player;

	PlayerAttack attacker;

	public float additionalDamage = 20;
	private float baseDamage;
	private float damage;

	public bool countsForCombo = true;

	void Start()
	{
		player = FindObjectOfType<Player_Controller> ();	
		attacker = FindObjectOfType<PlayerAttack> ();

	}

	void Update()
	{
		baseDamage = attacker.baseDamage;
		damage = baseDamage + additionalDamage;

		if (!countsForCombo)
		{
			// If it's the smash attack,
			damage = attacker.baseDamage*2 + (player.currentCombo * 10);
			// 20 + (combo * 10)

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
