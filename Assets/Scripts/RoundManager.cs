using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

	private int currentRound;

	private GameObject[] chargerEnemies;
	private GameObject[] flyingEnemies;
	public int numEnemies;

	private Spawner[] spawners;
	private int leftToSpawn = 10;


	private bool canIncrementRound = false;


	void Start ()
	{
		currentRound = 1;


	}

	void Update ()
	{
		chargerEnemies = GameObject.FindGameObjectsWithTag ("ChargerEnemy");
		flyingEnemies = GameObject.FindGameObjectsWithTag ("FlyingEnemy");
		// Any other enemies in the future
		numEnemies = chargerEnemies.Length + flyingEnemies.Length;

		spawners = FindObjectsOfType<Spawner> ();


		//Debug.Log (currentRound);



		if (numEnemies <= 0) //&& leftToSpawn == 0)
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
	}

	public void IncrementRound ()
	{
		//StartCoroutine (roundCountdown ());
		Debug.Log ("Press \"Select\" to go to the next round");
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
		}
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
