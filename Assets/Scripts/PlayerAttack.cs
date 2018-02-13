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
	public float attackMovementDistance = 0.225f;

	public float attackDurationUpswing = 1f;
	public float upswingCD = 0.5f;
	private bool canUpswing = true;

	public Collider2D attackTrigger;
	public Collider2D attackTriggerUpswing;


	private Animator anim;

	public Vector2 attackMoveDistance = new Vector2 (4, 0);
	public Vector2 attackUpswingDistanceRIGHT = new Vector2 (6, 12);
	public Vector2 attackUpswingDistanceLEFT = new Vector2 (-6, 12);


	void Start ()
	{
		anim = gameObject.GetComponent<Animator> ();
		attackTrigger.enabled = false;
		attackTriggerUpswing.enabled = false;
	}

	void Update ()
	{
		// First attack - X button 1st time pressed
		if (Input.GetKeyDown (KeyCode.JoystickButton2) && attackCounter < 3) //&& !attacking)
		{
			StartCoroutine (AttackMovementX (attackMovementDistance));

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

		// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

		if (Input.GetKeyDown (KeyCode.JoystickButton3) && canUpswing)
		{
			anim.SetInteger ("AttackState", 4); // set animation to upswing
			attackTriggerUpswing.enabled = true;
			StartCoroutine (AttackMovementY (attackDurationUpswing));
		}
	}


	IEnumerator AttackMovementX (float attackDuration) // Attack Movement Coroutine
	{
		float time = 0f;
		Time.timeScale = 1;


		while (attackDuration > time)
		{ //while theres still time left in the attack according to the attackDuration
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

	}

	IEnumerator AttackMovementY (float attackDuration) // Attack Movement Coroutine
	{
		float time = 0f;
		Time.timeScale = 1;
		canUpswing = false;

		bool facingRightUpswing = GetComponent<Player_Controller> ().facingRight;
			
		while (attackDuration > time)
		{//while theres still time left in the upswing animation
			time += Time.deltaTime;
			if (facingRightUpswing)
			{
				GetComponent<Player_Controller> ().rigidBody.velocity = attackUpswingDistanceRIGHT;
			}
			else
			if (!facingRightUpswing)
			{ // Same goes for left; 
				GetComponent<Player_Controller> ().rigidBody.velocity = attackUpswingDistanceLEFT;
			}
			yield return 0; // go to next frame
		}
		GetComponent<Player_Controller> ().rigidBody.velocity = new Vector2 (0, 8);
		attackTriggerUpswing.enabled = false; // turn off hitbox
		anim.SetInteger ("AttackState", 0); // back to idle

		Time.timeScale = 1;
		yield return new WaitForSeconds (upswingCD); //Cooldown time for being able to attack again
		canUpswing = true; //set back to true so that we can boost again.
	}
}
