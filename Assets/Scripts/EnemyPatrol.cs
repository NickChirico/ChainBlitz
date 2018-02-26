using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
	public SpriteRenderer ChargeIndicator;
	SpriteRenderer spriteRenderer;
	Rigidbody2D rb;

	public float moveVelocity;
	public float speed = 0.5f;
	private int direction = 1;
	// +1 is facing right, -1 is facing left

	// Raycast Variables
	public float timeWait = 2f;
	public float LOS = 3f;
	public float LOS_behind = 1f;
	private float pathLOS = 3f;
	public float force = 100f;
	private bool facingRight = true;
	private bool charging = false;

	// Other Key Variables
	public int health = 50;


	// Use this for initialization
	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rb = GetComponent<Rigidbody2D> ();
		ChargeIndicator.enabled = false;

		StartCoroutine ("Patrol");
		Physics2D.queriesStartInColliders = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Line of sight - Player - for charge attack
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.localScale.x * Vector2.right, LOS);
		if (hit.collider != null && hit.collider.CompareTag ("Player"))
		{
			// Charge at Player
			rb.AddForce (Vector3.forward * force + (hit.collider.transform.position - this.transform.position) * force);
			charging = true;
		}
		else
			charging = false;

		RaycastHit2D hitBehind = Physics2D.Raycast (transform.position, transform.localScale.x * Vector2.left, LOS_behind);
		if (hitBehind.collider != null && hitBehind.collider.CompareTag ("Player"))
		{
			Turn ();
		}

		// Line of Sight - Obstacle - turn around on patrol
		RaycastHit2D path = Physics2D.Raycast (transform.position, transform.localScale.x * new Vector2 (1, direction * -1f) * pathLOS);
		if (path.collider != null && path.collider.tag == "Edge")
		{
			if (!charging)
			{
				Turn ();
			}
		}

		if (charging)
			ChargeIndicator.enabled = true;
		else
		if (!charging)
			ChargeIndicator.enabled = false;
	}


	IEnumerator Patrol ()
	{
		while (true)
		{
			// Patrol movement
			moveVelocity = speed * direction;
			rb.velocity = new Vector2 (moveVelocity, rb.velocity.y);


			// Dont get rid of this...ever
			yield return null;
		}
	}

	void Turn ()
	{
		if (direction == 1)
		{
			direction = -1;
			transform.localScale = new Vector3 (-1, 1, 0);
		}
		else
		if (direction == -1)
		{
			direction = 1;
			transform.localScale = new Vector3 (1, 1, 0);
		}
	}

	void Damaged(int damage)
	{
		
		Destroy (this.gameObject);
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, transform.position + transform.localScale.x * Vector3.right * LOS);
		Gizmos.DrawLine (transform.position, transform.position + transform.localScale.x * Vector3.left * LOS_behind);


		Gizmos.color = Color.blue;
		Gizmos.DrawRay (transform.position, transform.localScale.x * new Vector2 (1, direction * -1f) * pathLOS);
	}
}
