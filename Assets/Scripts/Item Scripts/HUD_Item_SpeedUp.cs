using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Item_SpeedUp : MonoBehaviour
{
	Player_Controller player;
	public Item_SpeedUp theItem;
	public Image staticImage;
	Image icon;
	float duration;

	float timeLeft;
	float counter;

	void Start ()
	{
		player = FindObjectOfType<Player_Controller> ();
		icon = this.GetComponent<Image> ();
		duration = theItem.duration;
		timeLeft = duration;
	}

	void Update ()
	{
		if (player.isSpedUpBuff)
		{
			icon.enabled = true;
			staticImage.enabled = true;

			timeLeft -= Time.deltaTime;
			counter = timeLeft / duration;

			icon.fillAmount = counter;

			//deplete (duration);
		}
		else
		{
			icon.enabled = false;
			staticImage.enabled = false;
			timeLeft = duration;
		}
	}
}
