using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundEndUI : MonoBehaviour
{

	public RoundManager rm;
	Text t;
	public bool isLargeText;

	// Use this for initialization
	void Start ()
	{
		t = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (rm.canIncrementRound)
		{
			if (isLargeText)
			{
				t.text = "Round Complete!";
			}
			else
			{
				t.fontSize = 20;
				t.text = "Press 'Select' to go to the next round";
			}
		}
		else
			t.text = "";
	}
}
