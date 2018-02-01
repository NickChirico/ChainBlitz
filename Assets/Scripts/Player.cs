using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	SpriteRenderer spriteRenderer;
	Rigidbody2D rigidBody;


	public float speedX = 5f;
	public float jumpVelocity = 8f;
	public int numJumps = 2;
	private int jumps; //Value for double jumping


	//public float accel = 1000;

	//public float minSpeed = 3.5f;
	//public float maxSpeed = 5f;
	//public float duration = 0.1f;
	private float startTime;

	public GameObject floorObj;

	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rigidBody = GetComponent<Rigidbody2D> ();
		jumps = numJumps;

		startTime = Time.time;
	}

	void Update ()
	{ 
		// MOVE RIGHT - physics vel
		if (Input.GetKey (KeyCode.RightArrow))
		{
			rigidBody.velocity = new Vector2 (speedX, rigidBody.velocity.y);
			spriteRenderer.flipX = false;
		} 
		// MOVE LEFT - phyics vel
		else if (Input.GetKey (KeyCode.LeftArrow))
		{
			rigidBody.velocity = new Vector2 (-speedX, rigidBody.velocity.y);
			spriteRenderer.flipX = true;
		} 
		else
			rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
		


		// BASIC JUMP (extensive jump in PlayerJump.cs)
		if (jumps > 0 && Input.GetKeyDown (KeyCode.Space))
		{
			rigidBody.velocity = Vector2.up * jumpVelocity;
			jumps--;
		}
			




	}

	void OnCollisionEnter2D (Collision2D collisionInfo)
	{
		// Touching a Ground resets double jump
		if (collisionInfo.gameObject.tag == "Ground")
			jumps = numJumps;
	}
}
