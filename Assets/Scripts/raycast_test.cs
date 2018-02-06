using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class raycast_test : MonoBehaviour
{
	SpriteRenderer sprite;

	public Text guiText;

	// Use this for initialization
	void Start ()
	{
		sprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit2D raycastHit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

		if (raycastHit.collider != null)
		{
			if (raycastHit.collider.CompareTag ("test"))
			{
				sprite.color = Color.blue;
				guiText.text = raycastHit.collider.name + " is blue";
			}
			else
			{
				sprite.color = Color.white;
				guiText.text = guiText.text = raycastHit.collider.name + " is white";
			}
		}
	}
}
