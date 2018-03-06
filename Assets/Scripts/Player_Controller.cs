using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
	public GameObject results;

	public static Player_Controller instance;
	SpriteRenderer spriteRenderer;
	public Rigidbody2D rigidBody;
	private Animator anim;

	[HideInInspector]
	public Collider2D[] colls;
	public PhysicsMaterial2D highFriction;

	// Movement variables
	public float speedX = 5f;
	public float moveVelocity;
	public float jumpVelocity = 8f;
	public int numJumps = 2;
	[HideInInspector]
	public int jumps;
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
	[HideInInspector]
	public bool isAlive = true;

	[SerializeField]
	public Stat health;
	//public int health = 100;
	//public int maxHealth = 100;
	[HideInInspector]
	public int lives;
	[HideInInspector]
	public int currentCombo = 0;
	public float invincibilityDuration = 2;

	public Vector2 flinchKnockbackRight = new Vector2 (-5, 3);
	public Vector2 flinchKnockbackLeft = new Vector2 (5, 3);

	public float flinchLength = 1f;


	// Buff Variables for HUD
	[HideInInspector]
	public bool isInvincible = false;
	[HideInInspector]
	public bool isInvincibleBuff = false;
	[HideInInspector]
	public bool isSpedUpBuff = false;



	private void Awake ()
	{
		health.Initialize ();
	}

	void Start ()
	{
		instance = this;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rigidBody = GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		colls = this.GetComponents<Collider2D> ();

		health.CurrentValue = health.MaxValue;

		this.transform.position = new Vector3 (0, -2, 0);
		lives = 1;
		jumps = numJumps;
	}

	void Update ()
	{

		// Movement with Xbox controller
		// Left Stick is movement
		if (isAlive)
		{
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

		if (isInvincible)
		{
			int enemyLayer = LayerMask.NameToLayer ("Enemy");
			int bulletLayer = LayerMask.NameToLayer ("Projectile");
			int playerLayer = LayerMask.NameToLayer ("Player");
			Physics2D.IgnoreLayerCollision (enemyLayer, playerLayer, true);
			Physics2D.IgnoreLayerCollision (bulletLayer, playerLayer, true);
		}
		else
		if (!isInvincible)
		{
			int enemyLayer = LayerMask.NameToLayer ("Enemy");
			int bulletLayer = LayerMask.NameToLayer ("Projectile");
			int playerLayer = LayerMask.NameToLayer ("Player");
			Physics2D.IgnoreLayerCollision (enemyLayer, playerLayer, false);
			Physics2D.IgnoreLayerCollision (bulletLayer, playerLayer, false);
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
		isInvincible = true;
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
			else
			if (!facingRightFlinch)
				rigidBody.velocity = flinchKnockbackLeft;

			yield return 0;			
		}
		anim.SetInteger ("AnimState", 0);

		//wait for invincibility to end
		yield return new WaitForSeconds (invincibilityDuration);

		//stop blinking animation and resume enemy collisions
		isInvincible = false;
		anim.SetLayerWeight (1, 0);

	}

	public void TakeDamage (int damage)
	{
		health.CurrentValue -= damage;
		if (health.CurrentValue <= 0)
		{
			Die ();
			anim.SetInteger ("AnimState", -1);
			spriteRenderer.color = Color.red;
		}
		else
		{
			StartCoroutine (DamageBlink (flinchLength));
		}
		currentCombo = 0;

	}

	void Die ()
	{
		isAlive = false;
		rigidBody.sharedMaterial = highFriction;
		StartCoroutine (showResults ());
	}

	IEnumerator showResults ()
	{
		yield return new WaitForSeconds (1);
		results.SetActive (true);
	}

	void OnCollisionEnter2D (Collision2D collisionInfo)
	{
		// Touching the Ground resets double jump, and refreshes the dash
		//										   ** DOES NOT RESET DASH
		if (collisionInfo.gameObject.tag == "Ground")
		{
			jumps = numJumps;
			currentCombo = 0;
			onGround = true;
			if (anim.GetInteger ("AttackState") == 5)
				anim.SetInteger ("AttackState", 0);
		}
	}

	void OnCollisionExit2D (Collision2D collisionInfo)
	{
		if (collisionInfo.gameObject.tag == "Ground")
		{
			onGround = false;
		}
	}

	// ITEMS BUFFS EFFECTS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

	public IEnumerator invincibility (float duration)
	{
		isInvincible = true;
		isInvincibleBuff = true;
		spriteRenderer.color = Color.yellow;
		anim.SetLayerWeight (1, 1);


		yield return new WaitForSeconds (duration);

		spriteRenderer.color = Color.white;

		anim.SetLayerWeight (1, 0);
		isInvincible = false;
		isInvincibleBuff = false;
	}

	public IEnumerator speedUp(float duration, float amount)
	{
		isSpedUpBuff = true;
		speedX *= amount;

		yield return new WaitForSeconds (duration);

		speedX /= amount;
		isSpedUpBuff = false;
	}
}
