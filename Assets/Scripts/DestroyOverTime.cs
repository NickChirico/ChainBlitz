using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

	public float timeToDestroy;
	// Use this for initialization
	void Start () {
		StartCoroutine (destroySelf (timeToDestroy));
	}
	
	// Update is called once per frame
	IEnumerator destroySelf(float countdown)
	{
		yield return new WaitForSeconds (countdown);
		Destroy (this.gameObject);
	}
}
