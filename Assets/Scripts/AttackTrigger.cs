using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

	public int damage = 20;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.isTrigger == true && collider.CompareTag ("Enemy"))
		{
			// WHAT HAPPENS WHEN YOU ATTACK AN ENEMY

			// TO-DO
			Debug.Log("Enemy has been hit!");
		}
	}
}
