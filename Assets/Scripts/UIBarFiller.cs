using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIBarFiller : MonoBehaviour
{
	private float fillAmount = 0f;

	[SerializeField]
	private float lerpSpeed;

	[SerializeField]
	private Image content;

	[SerializeField]
	private Text valueText;

	public float MaxValue{ get; set; }

	public float Value
	{
		set
		{
			string[] temp = valueText.text.Split (':');
			valueText.text = temp [0] + ": " + value;
			fillAmount = Map (value, 0, MaxValue, 0, 1);
		}
	}

	void Start ()
	{
	}

	void Update ()
	{
		manageBar ();
	}

	private void manageBar ()
	{
		if (fillAmount != content.fillAmount)
		{
			
			content.fillAmount = Mathf.Lerp (content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
		}
	}

	private float Map (float value, float inMin, float inMax, float outMin, float outMax)
	{
		float toReturn;
		toReturn = (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
		return toReturn;
	}
}
