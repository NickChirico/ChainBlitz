using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	Player_Controller player;
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

	public float attackDurationSmash = 1f;
	public float smashWindup = 1f;
	public float smashCD = 1f;
	private bool canSmash = false;

	public Collider2D attackTrigger1;
	public Collider2D attackTrigger2;
	public Collider2D attackTrigger3;
	public Collider2D attackTriggerUpswing;
	public Collider2D attackTriggerSmash;


	private Animator anim;

	public Vector2 attackMoveDistance = new Vector2 (4, 0);
	public Vector2 attackUpswingDistanceRIGHT = new Vector2 (6, 12);
	public Vector2 attackUpswingDistanceLEFT = new Vector2 (-6, 12);
	public Vector2 attackSmashDistanceRIGHT = new Vector2 (4, -10);
	public Vector2 attackSmashDistanceLEFT = new Vector2 (-4, -10);




	void Start ()
	{		
		player = FindObjectOfType<Player_Controller> ();
		anim = gameObject.GetComponent<Animator> ();
		attackTrigger1.enabled = false;
		attackTrigger2.enabled = false;
		attackTrigger3.enabled = false;
		attackTriggerUpswing.enabled = false;
		attackTriggerSmash.enabled = false;
	}

	void Update ()
	{
		// Checking for which attack hitboxes should be active
		CheckHitboxes ();

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
			//attackTrigger1.enabled = true;

		}

		if (attackCounter > 0)
		{
			attackTimer += Time.deltaTime;
			if (attackTimer > chainWindow)
			{
				anim.SetInteger ("AttackState", 0); // Back to Idle if you wait too long
				attackCounter = 0;
			}
		}

		// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

		if (Input.GetKeyDown (KeyCode.JoystickButton3) && canUpswing)
		{
			anim.SetInteger ("AttackState", 4); // set animation to upswing
			StartCoroutine (AttackMovementY (attackDurationUpswing));
		}

		if (Input.GetKeyDown (KeyCode.JoystickButton1) && canSmash)
		{
			anim.SetInteger ("AttackState", 6); // set animation to Smash WINDUP
			StartCoroutine (AttackMovementB (attackDurationSmash));
		}
	}

	void CheckHitboxes ()
	{
		if (anim.GetInteger ("AttackState") == 1)
			attackTrigger1.enabled = true;
		if (anim.GetInteger ("AttackState") == 2)
			attackTrigger2.enabled = true;
		if (anim.GetInteger ("AttackState") == 3)
			attackTrigger3.enabled = true;
		if (anim.GetInteger ("AttackState") == 4)
			attackTriggerUpswing.enabled = true;
		if (anim.GetInteger ("AttackState") == 5)
			attackTriggerSmash.enabled = true;

		if (anim.GetInteger ("AttackState") != 1)
			attackTrigger1.enabled = false;
		if (anim.GetInteger ("AttackState") != 2)
			attackTrigger2.enabled = false;
		if (anim.GetInteger ("AttackState") != 3)
			attackTrigger3.enabled = false;
		if (anim.GetInteger ("AttackState") != 4)
			attackTriggerUpswing.enabled = false;
		if (anim.GetInteger ("AttackState") != 5)
			attackTriggerSmash.enabled = false;

		canSmash = !Player_Controller.instance.onGround;
		//if (!canSmash)
	}


	IEnumerator AttackMovementX (float attackDuration) // Attack Movement Coroutine
	{
		//Ignore collision while attacking
		int enemyLayer = LayerMask.NameToLayer ("Enemy");
		int playerLayer = LayerMask.NameToLayer ("Player");
		Physics2D.IgnoreLayerCollision (enemyLayer, playerLayer, true);
		foreach (Collider2D collider in Player_Controller.instance.colls)
		{
			collider.enabled = false;
			collider.enabled = true;
		}
			
		float time = 0f;
		Time.timeScale = 1;

		while (attackDuration > time)
		{ //while theres still time left in the attack according to the attackDuration
			time += Time.deltaTime;
			if (GetComponent<Player_Controller> ().facingRight)
			{
				GetComponent<Player_Controller> ().rigidBody.velocity = attackMoveDistance; // Briefly Dash Right - no Y vel
			}
			else
			if (!GetComponent<Player_Controller> ().facingRight)
			{ // Same goes for left; 
				GetComponent<Player_Controller> ().rigidBody.velocity = -attackMoveDistance;
			}
			yield return 0; //go to next frame
		}


		Physics2D.IgnoreLayerCollision (enemyLayer, playerLayer, false);
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
		GetComponent<Player_Controller> ().rigidBody.velocity = new Vector2 (0, 12);
		anim.SetInteger ("AttackState", 0); // back to idle

		yield return new WaitForSeconds (upswingCD); //Cooldown time for being able to attack again
		canUpswing = true; //set back to true so that we can boost again.
	}

	IEnumerator AttackMovementB (float attackDuration)
	{
		float time = 0f;
		Time.timeScale = 1;
		//canSmash = false;  Unnecessary since usses player.OnGround check
		bool facingRightSmash = GetComponent<Player_Controller> ().facingRight;

		//Windup
		GetComponent<Player_Controller> ().rigidBody.velocity = new Vector2 (0, 8);
		yield return new WaitForSeconds (smashWindup);
		anim.SetInteger ("AttackState", 5); // back to idle

		// Smash
		while (attackDuration > time)
		{
			time += Time.deltaTime;
			if (facingRightSmash)
			{
				GetComponent<Player_Controller> ().rigidBody.velocity = attackSmashDistanceRIGHT;
			}
			else
			if (!facingRightSmash)
			{ 
				GetComponent<Player_Controller> ().rigidBody.velocity = attackSmashDistanceLEFT;
			}
			//GetComponent<Player_Controller> ().rigidBody.velocity = new Vector2 (0, 8);
		}
		yield return new WaitForSeconds (smashCD);
		player.currentCombo = 0;
	}
}
