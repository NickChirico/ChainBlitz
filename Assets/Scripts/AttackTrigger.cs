using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

	public int damage1 = 20;
	public int damage2 = 25;
	public int damage3 = 35;
	public int damageUpswing = 30;



	void OnTriggerEnter2D(Collider2D collider)
	{
		GameObject enemy;
		if (collider.isTrigger == true && collider.CompareTag ("Enemy"))
		{
			// WHAT HAPPENS WHEN YOU ATTACK AN ENEMY
			//Destroy(

			// TO-DO
			Debug.Log("Enemy has been hit! for " + damage1 + " damage");
		}
	}
}
