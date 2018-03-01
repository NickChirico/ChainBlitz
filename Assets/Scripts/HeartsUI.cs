
using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour {

	public Sprite[] HeartSprites;
	public Image HeartUI;
	private Player_Controller player;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player_Controller>();
	}
	
	// Update is called once per frame
	void Update () {
		HeartUI.sprite = HeartSprites [player.health];
	}
}
