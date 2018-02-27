using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
	public static Player_Controller instance;
	SpriteRenderer spriteRenderer;
	public Rigidbody2D rigidBody;
	private Animator anim;

	[HideInInspector]
	public Collider2D[] colls;

	// Movement variables
	public float speedX = 5f;
	public float moveVelocity;
	public float jumpVelocity = 8f;
	public int numJumps = 2;
	private int jumps;
	// Value for double jumping
	public bool facingRight = true;
	[HideInInspector]
	public bool onGround;
	private bool canDash = true;
	// Dash Vectors
	public Vector2 dashSpeedRight = new Vector2 (14, 0);
	public Vector2 dashSpeedLeft = new Vector2 (-14, 0);
	public Vector2 dashSpeedRightAIR = new Vector2 (14, 3);
	public Vector2 dashSpeedLeftAIR = new Vector2 (-14, 3);
	public float dashCD = 2f;
	// Dash Cooldown
	public float dashLength = 1f;


	// Combat Variables
	private bool isAlive = true;
	public int health = 100;
	public float invincibilityDuration = 2;

	public Vector2 flinchKnockbackRight = new Vector2 (-5, 3);
	public Vector2 flinchKnockbackLeft = new Vector2 (5, 3);

	public float flinchLength = 1f;





	void Start ()
	{
		instance = this;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rigidBody = GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		colls = this.GetComponents<Collider2D> ();

		jumps = numJumps;
	}

	void Update ()
	{

		// Movement with Xbox controller
		// Left Stick is movement

		moveVelocity = speedX * Input.GetAxisRaw ("Horizontal");

		// Flip the sprite to where he's facing accordingly.
		if (moveVelocity > 0)
		{
			transform.localScale = new Vector3 (3, 2.05f, 3);
			facingRight = true;
		}
		else
		if (moveVelocity < 0)
		{
			transform.localScale = new Vector3 (-3, 2.05f, 3);
			facingRight = false;
		}

		rigidBody.velocity = new Vector2 (moveVelocity, rigidBody.velocity.y);


		// BASIC JUMP (extensive jump details in PlayerJump.cs)
		if (jumps > 0 && Input.GetKeyDown (KeyCode.JoystickButton0))
		{
			rigidBody.velocity = Vector2.up * jumpVelocity;
			jumps--;
		}
			
		// DASH (Left Shift)
		if ((Input.GetKeyDown (KeyCode.JoystickButton4) || Input.GetKeyDown (KeyCode.JoystickButton5)) && canDash)
		{
			StartCoroutine (Dash (dashLength)); // Call coroutine to dash with dash duration parameter

		}
	}



	// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

	IEnumerator Dash (float dashDuration) // Dash Coroutine -> dashLength parameter
	{
		float time = 0f;
		canDash = false;

		bool facingRightDash = facingRight; // cannot change direction while dashing

		while (dashDuration > time)
		{ //while theres still time left in the dash according to the dashLength
			time += Time.deltaTime;
			if (facingRightDash)
			{
				if (onGround)
					rigidBody.velocity = dashSpeedRight; // Dash Right in air with Y velocity
				else
					rigidBody.velocity = dashSpeedRightAIR; // Dash Right - no Y vel

			}
			else
			if (!facingRightDash)
			{ // Same goes for left; 
				if (onGround)
					rigidBody.velocity = dashSpeedLeft;
				else
					rigidBody.velocity = dashSpeedLeftAIR; 
			}
			yield return 0; //go to next frame
		}
		Time.timeScale = 1;
		yield return new WaitForSeconds (dashCD); //Cooldown time for being able to boost again
		canDash = true; //set back to true so that we can boost again.
	}

	IEnumerator DamageBlink (float flinchDuration)
	{
		//temporarily ignore collision with enemies
		int enemyLayer = LayerMask.NameToLayer ("Enemy");
		int flyingEnemyLayer = LayerMask.NameToLayer ("FlyingEnemy");
		int projectileLayer = LayerMask.NameToLayer ("Projectile");
		int playerLayer = LayerMask.NameToLayer ("Player");
		Physics2D.IgnoreLayerCollision (enemyLayer, playerLayer, true);
		Physics2D.IgnoreLayerCollision (flyingEnemyLayer, playerLayer, true);
		Physics2D.IgnoreLayerCollision (projectileLayer, playerLayer, true);
		foreach (Collider2D collider in colls)
		{
			collider.enabled = false;
			collider.enabled = true;
		}

		//Start blinking animation
		anim.SetLayerWeight (1, 1);

		//flinch
		float time = 0f;
		bool facingRightFlinch = facingRight;

		while (flinchDuration > time)
		{
			anim.SetInteger ("AnimState", 1);
			time += Time.deltaTime;
			if (facingRightFlinch)
				rigidBody.velocity = flinchKnockbackRight;
			else if(!facingRightFlinch)
				rigidBody.velocity = flinchKnockbackLeft;

			yield return 0;			
		}
		anim.SetInteger ("AnimState", 0);

		//wait for invincibility to end
		yield return new WaitForSeconds (invincibilityDuration);

		//stop blinking animation and resume enemy collisions
		Physics2D.IgnoreLayerCollision (enemyLayer, playerLayer, false);
		Physics2D.IgnoreLayerCollision (flyingEnemyLayer, playerLayer, false);
		Physics2D.IgnoreLayerCollision (projectileLayer, playerLayer, false);
		anim.SetLayerWeight (1, 0);

	}

	void TakeDamage (int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			isAlive = false;
			//DIE DEATH DEAD function
			spriteRenderer.color = Color.red;
		}
		else
		{
			StartCoroutine (DamageBlink (flinchLength));
		}
	}

	void OnCollisionEnter2D (Collision2D collisionInfo)
	{
		// Touching the Ground resets double jump, and refreshes the dash
		//										   ** DOES NOT RESET DASH
		if (collisionInfo.gameObject.tag == "Ground")
		{
			jumps = numJumps;
			onGround = true;
			if (anim.GetInteger ("AttackState") == 5)
				anim.SetInteger ("AttackState", 0);
		}

		if (collisionInfo.gameObject.tag == "FlyingEnemy")
		{
			//TakeDamage (15); Dont take damage. Instead, refresh double jump. Lets try it.
			jumps = numJumps;
		}

		if (collisionInfo.gameObject.tag == "ChargerEnemy")
		{
			TakeDamage (15);
		}

		if (collisionInfo.gameObject.tag == "Projectile")
		{
			TakeDamage (6);
		}

	}

	void OnCollisionExit2D (Collision2D collisionInfo)
	{
		if (collisionInfo.gameObject.tag == "Ground")
		{
			onGround = false;
		}
	}
}
