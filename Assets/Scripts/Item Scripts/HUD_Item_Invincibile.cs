using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Item_Invincibile : MonoBehaviour
{

	Player_Controller player;
	public Item_Invinsible theItem;
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
		// This was a check to refresh the icon if you collect the buff again
		// But that functionality doesnt work in the buff itself
		//checkForNewBuff ();

		if (player.isInvincibleBuff)
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

	/*void checkForNewBuff()
	{
		if (theItem.wasCollected)
		{
			// Reset values
			timeLeft = duration;
		}
	}*/
}
