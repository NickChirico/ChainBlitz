using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

	public int damage = 20;

	void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.isTrigger != true && collision.CompareTag("ChargerEnemy"))
		{
			collision.SendMessageUpwards("Damage", damage);

			// TO-DO
			Debug.Log("Charger Enemy has been hit! for " + damage + " damage");
		}

		if (collision.isTrigger != true && collision.CompareTag("FlyingEnemy"))
		{
			collision.SendMessageUpwards("Damage", damage);

			// TO-DO
			Debug.Log("Flying Enemy has been hit! for " + damage + " damage");
		}
	}
}
