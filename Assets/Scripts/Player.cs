﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	SpriteRenderer spriteRenderer;
	Rigidbody2D rigidBody;

	public float speedX = 5f;
	public float jumpVelocity = 8f;
	public int numJumps = 2;
	private int jumps;
	//Value for double jumping
	private bool facingRight;
	private bool onGround;

	private bool canDash = true;
	//Check for dashing

	public Vector2 dashSpeedRight = new Vector2 (14, 0);
	public Vector2 dashSpeedLeft = new Vector2 (-14, 0);
	public Vector2 dashSpeedRightAIR = new Vector2 (14, 3);
	public Vector2 dashSpeedLeftAIR = new Vector2 (-14, 3);
	public float dashCD = 2f;
	// Dash Cooldown
	public float dashLength = 1f;


	public GameObject floorObj;

	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rigidBody = GetComponent<Rigidbody2D> ();
		jumps = numJumps;
	}

	void Update ()
	{ 
		// MOVE RIGHT - physics vel
		if (Input.GetKey (KeyCode.RightArrow))
		{
			rigidBody.velocity = new Vector2 (speedX, rigidBody.velocity.y);
			spriteRenderer.flipX = false;
			facingRight = true;
		} 
		// MOVE LEFT - phyics vel
		else if (Input.GetKey (KeyCode.LeftArrow))
		{
			rigidBody.velocity = new Vector2 (-speedX, rigidBody.velocity.y);
			spriteRenderer.flipX = true;
			facingRight = false;
		} else
			rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
		


		// BASIC JUMP (extensive jump in PlayerJump.cs)
		if (jumps > 0 && Input.GetKeyDown (KeyCode.Space))
		{
			rigidBody.velocity = Vector2.up * jumpVelocity;
			jumps--;
		}
			

		// DASH (Left Shift)
		if (Input.GetKeyDown (KeyCode.LeftShift) && canDash)
		{
			StartCoroutine (Dash (dashLength)); // Call coroutine to dash with dash duration parameter
		
		}
	}


	IEnumerator Dash (float dashDuration) // Dash Coroutine -> dashLength parameter
	{
		float time = 0f;
		canDash = false;

		while (dashDuration > time) //while theres still time left in the dash according to the dashLength
		{
			time += Time.deltaTime;
			if (facingRight)
			{
				if (!onGround)
					rigidBody.velocity = dashSpeedRightAIR; // Dash Right in air with Y velocity
				else
					rigidBody.velocity = dashSpeedRight; // Dash Right - no Y vel
				
			} else if (!facingRight) // Same goes for left; 
			{
				if (!onGround)
					rigidBody.velocity = dashSpeedLeftAIR; 
				else
					rigidBody.velocity = dashSpeedLeft; 
			}
			yield return 0; //go to next frame
		}
		Time.timeScale = 1;
		yield return new WaitForSeconds (dashCD); //Cooldown time for being able to boost again, if you'd like.
		canDash = true; //set back to true so that we can boost again.
	}



	void OnCollisionEnter2D (Collision2D collisionInfo)
	{
		// Touching the Ground resets double jump, and refreshes the dash
		//										   ** DOES NOT RESET DASH
		if (collisionInfo.gameObject.tag == "Ground")
		{
			jumps = numJumps;
			onGround = true;
		}
	}

	void OnCollisionExit2D (Collision2D collisionInfo)
	{
		onGround = false;
	}
}
