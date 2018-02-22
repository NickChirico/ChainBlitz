using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	//	public Collider2D rightEdge;
	//	public Collider2D leftEdge;
	//	public Collider2D topEdge;
	//	public Collider2D bottomEdge;

	private Vector2 velocity;

	public float smoothTimeX;
	public float smoothTimeY;

	public GameObject player;

	void Start ()
	{
		//player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update ()
	{
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		//float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y+2.5f, ref velocity.y, smoothTimeY); AT BOTTOM, NOT CENTER
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);


		this.transform.position = new Vector3 (posX, posY, transform.position.z);
	}
}
