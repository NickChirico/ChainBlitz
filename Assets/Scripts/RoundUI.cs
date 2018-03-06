using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundUI : MonoBehaviour
{
	public RoundManager rm;
	Text RoundText;

	// Use this for initialization
	void Start ()
	{
		RoundText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		RoundText.text = "Round "+rm.currentRound;
	}
}
