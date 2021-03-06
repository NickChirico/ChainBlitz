using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	Player_Controller player;
	public GameObject enemyToSpawn;
	SpriteRenderer spriteRenderer;
	float x;
	Vector2 whereToSpawn;

	public float DetectionRange = 20;
	public float spawnRate = 2f;
	// spawn every 'x' seconds;
	float nextSpawn = 0.0f;
	public int EnemiesToSpawn;
	public int leftToSpawn;

	[HideInInspector]
	public bool isEnabled;


	// Use this for initialization
	void Start ()
	{
		player = FindObjectOfType<Player_Controller> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		StartOfRound (0);
		isEnabled = true;
	}

	public void StartOfRound(int additional)
	{
		leftToSpawn = EnemiesToSpawn + additional;
		spriteRenderer.enabled = true;
		isEnabled = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Vector3.Distance (transform.position, player.transform.position) <= DetectionRange)
		{

			if (Time.time > nextSpawn)
			{
				nextSpawn = Time.time + spawnRate;
				x = transform.position.x; // can change to a range if want to, or keep it static.
				whereToSpawn = new Vector2 (x, transform.position.y);

				if (leftToSpawn > 0)
				{
					Instantiate (enemyToSpawn, whereToSpawn, Quaternion.identity);
					leftToSpawn--;
				}
			}

			if (leftToSpawn <= 0)
			{
				spriteRenderer.enabled = false;
				isEnabled = false;
			}

		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine (new Vector3 (transform.position.x - DetectionRange, transform.position.y, transform.position.z), new Vector3 (transform.position.x + DetectionRange, transform.position.y, transform.position.z));
		Gizmos.DrawLine (new Vector3 (transform.position.x, transform.position.y - DetectionRange, transform.position.z), new Vector3 (transform.position.x, transform.position.y + DetectionRange, transform.position.z));

	}
}
