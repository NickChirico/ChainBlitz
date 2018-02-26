using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerSpawner : MonoBehaviour
{

	public GameObject enemyCharger;
	float x;
	Vector2 whereToSpawn;
	public float spawnRate = 2f;
	// spawn every 'x' seconds;
	float nextSpawn = 0.0f;


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > nextSpawn)
		{
			nextSpawn = Time.time + spawnRate;
			x = transform.position.x; // can change to a range if want to, or keep it static.
			whereToSpawn = new Vector2(x, transform.position.y);

			Instantiate (enemyCharger, whereToSpawn, Quaternion.identity);

		}
	}
}
