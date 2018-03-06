using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
	[HideInInspector]
	public int currentRound;

	private GameObject[] chargerEnemies;
	private GameObject[] flyingEnemies;
	public int numEnemies;

	private Spawner[] spawners;
	private bool endOfSpawn = false;

	[HideInInspector]
	public bool canIncrementRound = false;

	private HealthPack[] healthPacks;

	void Start ()
	{
		currentRound = 1;

		healthPacks = FindObjectsOfType<HealthPack> ();
	}

	void Update ()
	{
		chargerEnemies = GameObject.FindGameObjectsWithTag ("ChargerEnemy");
		flyingEnemies = GameObject.FindGameObjectsWithTag ("FlyingEnemy");
		// Add any other enemies in the future
		numEnemies = chargerEnemies.Length + flyingEnemies.Length;

		spawners = FindObjectsOfType<Spawner> ();

		//Debug.Log (currentRound);

		endOfSpawn = checkSpawners ();
		if (numEnemies <= 0 && endOfSpawn)
		{
			IncrementRound ();
		}

		if (canIncrementRound)
		{
			if (Input.GetKeyDown (KeyCode.JoystickButton6))
			{
				startNewRound ();
			}
		}

		// WHEN THERE ARE FEW 'x' ENEMIES LEFT, SHOW INDICATORS
		// foreach enemy, ScreenIndicator.enabled = true;
	}

	public bool checkSpawners ()
	{
		foreach (Spawner s in spawners)
		{
			if (s.leftToSpawn > 0)
				return false;
		}
		return true;
	}

	public void IncrementRound ()
	{
		//StartCoroutine (roundCountdown ());
		//Debug.Log ("Press \"Select\" to go to the next round");
		canIncrementRound = true;

	}

	void startNewRound ()
	{
		currentRound++;
		canIncrementRound = false;

		Debug.Log ("Next Round!");
		foreach (Spawner s in spawners)
		{
			// TODO: CHANGE THIS to a real increment for how many more enemies should be spawned
			s.StartOfRound (currentRound);
			if (s.spawnRate > 2f)
				s.spawnRate -= 0.25f;
		}

		// Respawn all healthpacks at the start of a new round.
		foreach (HealthPack h in healthPacks)
			h.isUsable (true);
	}

	//

	/*IEnumerator roundCountdown ()
	{
		float duration = 10f;
		float time = 0f;
		Time.timeScale = 1;


		//StartCoroutine (countdown ());

		if (Input.GetKeyDown (KeyCode.JoystickButton6))
		{
			startNewRound ();
			yield break;
		}


		yield return 0;
	}

	IEnumerator countdown()
	{
		float duration = 5f;
		float normalizedTime = 0f;
		while (normalizedTime < 1f)
		{
			normalizedTime += Time.deltaTime / duration;
			//Debug.Log (normalizedTime);
			yield return null;
		}
	}*/

}
