using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ComboTextUI : MonoBehaviour
{

	public Player_Controller player;
	Text comboText;

	// Use this for initialization
	void Start ()
	{
		player = FindObjectOfType<Player_Controller> ();
		comboText = GetComponent<Text> ();	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (player.currentCombo <= 0)
		{
			comboText.text = "";
		}
		else if(player.currentCombo > 0 && !player.onGround)
			comboText.text = "Air Combo: " + player.currentCombo + "\n" + "Smash Damage: " + (20+(player.currentCombo*10));
	}
}
