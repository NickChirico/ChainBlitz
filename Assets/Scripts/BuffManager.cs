using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
	public GameObject[] buffs;
	//GameObject[] activeBuffs;
	private List<GameObject> activeBuffs;

	Player_Controller player;
	PlayerAttack playerAttack;

	private List<bool> buffAllows;
	bool buff0;
	bool buff1;
	bool buff2;

	void Start ()
	{
		player = FindObjectOfType<Player_Controller> ();
		playerAttack = FindObjectOfType<PlayerAttack> ();

		activeBuffs = new List<GameObject> ();
		buffAllows = new List<bool> ();
	}

	void Update ()
	{
		CheckToAdd ();
		CheckToRemove ();
		DisplayBuffs ();

	}

	void DisplayBuffs()
	{
		for(int i = 0; i < activeBuffs.Count; i++)
		{
			Debug.Log ("Instanciated " + i);
			Instantiate (activeBuffs [i], this.transform.position, this.transform.rotation);
			//Instantiate (drops[Random.Range(0, drops.Length)], this.transform.position, this.transform.rotation);
		}
	}


	void CheckToAdd()
	{
		// Check which buffs are active and add to ArrayList, if they arent already

		if (/*!activeBuffs.Contains (buffs [0]) &&*/ player.isInvincibleBuff)
		{
			Debug.Log ("Instanciated invincible");
			activeBuffs.Add (buffs [0]);
			buff0 = true;
			buffAllows.Add (buff0);
		}
		if (!activeBuffs.Contains (buffs [1]) && playerAttack.damageIsBuffed)
		{
			activeBuffs.Add (buffs [1]);
			buff1 = true;
			buffAllows.Add (buff1);
		}
		if (!activeBuffs.Contains (buffs [2]) && player.isSpedUpBuff)
		{
			activeBuffs.Add (buffs [2]);
			buff2 = true;
			buffAllows.Add (buff2);
		}
	}
	void CheckToRemove()
	{
		// checks if any buffs are no longer active and removes them from the ArrayList
		if (activeBuffs.Contains (buffs [0]) && player.isInvincibleBuff)
		{
			activeBuffs.Remove (buffs [0]);
		}
		if (activeBuffs.Contains (buffs [1]) && playerAttack.damageIsBuffed)
		{
			activeBuffs.Remove (buffs [1]);
		}
		if (activeBuffs.Contains (buffs [2]) && player.isSpedUpBuff)
		{
			activeBuffs.Remove (buffs [2]);
		}
	}
}
