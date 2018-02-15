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


	// Use this for initialization
	void Start ()
	{
		//player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void fixedUpdate()
	{
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		this.transform.position = new Vector3 (posX, posY, transform.position.z);
	}
}
