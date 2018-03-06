using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemCharger : MonoBehaviour
{
	public GameObject[] drops;
	EnemyPatrol enemy;
	private bool canDropItem = true;
	public float chanceToDrop;

	// Use this for initialization
	void Start ()
	{
		enemy = GetComponent<EnemyPatrol> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (enemy.health <= 0 && canDropItem)
			dropItem ();
	}

	void dropItem()
	{
		float randValue = Random.value;
		//Debug.Log (randValue);

		// If it will drop an item, pick a random item to drop
		if(randValue < chanceToDrop)
			Instantiate (drops[Random.Range(0, drops.Length)], this.transform.position, this.transform.rotation);
			//Instantiate (drops, this.transform.position, this.transform.rotation);

		canDropItem = false;
	}
}
