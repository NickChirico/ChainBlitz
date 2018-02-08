using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	private bool attacking = false;

	private int attackCounter = 0;
	// number of attacks for 1st basic attack. Starts at 1, goes up to 3

	private float attackTimer = 0f;
	private int attackSequence = 0;
	// Which part of the 3 hit combo are you on - 0 is idle
	public float chainWindow = 1f;
	// Seconds
	public float attackMovementDistance = 0.225f;

	public Collider2D attackTrigger;

	private Animator anim;

	public Vector2 attackMoveDistance = new Vector2 (4, 0);

	void Start ()
	{
		anim = gameObject.GetComponent<Animator> ();
		attackTrigger.enabled = false;
	}

	void Update ()
	{
		// First attack - X button 1st time pressed
		if (Input.GetKeyDown (KeyCode.JoystickButton2) && attackCounter < 3) //&& !attacking)
		{
			StartCoroutine (AttackMovement (attackMovementDistance));

			attackCounter++;
			if (attackCounter > 3)
				attackCounter = 1;
			attackTimer = 0;

			attacking = true;
			//attackTimer = attackCD;

			anim.SetInteger ("AttackState", attackCounter);
			attackTrigger.enabled = true;
		}

		if (attackCounter > 0)
		{
			attackTimer += Time.deltaTime;
			if (attackTimer > chainWindow)
			{
				anim.SetInteger ("AttackState", 0); // Back to Idle if you wait too long
				attackTrigger.enabled = false;
				attackCounter = 0;
			}
		}

			
	}


	IEnumerator AttackMovement (float attackDuration) // Attack Movement Coroutine
	{
		float time = 0f;
		//canDash = false;

		while (attackDuration > time)
		{ //while theres still time left in the dash according to the dashLength
			time += Time.deltaTime;
			if (GetComponent<Player_Controller> ().facingRight)
			{
				GetComponent<Player_Controller> ().rigidBody.velocity = attackMoveDistance; // Dash Right - no Y vel
			}
			else
			if (!GetComponent<Player_Controller> ().facingRight)
			{ // Same goes for left; 
				GetComponent<Player_Controller> ().rigidBody.velocity = -attackMoveDistance;
			}
			yield return 0; //go to next frame
		}
		Time.timeScale = 1;
		//yield return new WaitForSeconds (dashCD); //Cooldown time for being able to boost again, if you'd like.
		//canDash = true; //set back to true so that we can boost again.
	}
}
