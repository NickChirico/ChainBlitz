using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

	public int damage1 = 20;
	public int damage2 = 25;
	public int damage3 = 35;
	public int damageUpswing = 30;



	void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.isTrigger != true && collision.CompareTag("ChargerEnemy"))
		{
			collision.SendMessageUpwards("Damage", damage1);

			// TO-DO
			Debug.Log("Enemy has been hit! for " + damage1 + " damage");
		}
	}
}
