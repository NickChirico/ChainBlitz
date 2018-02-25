using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

	public Transform[] patrolPoints;
	int currentPoint;

	public GameObject target;

	public float speed = 0.5f;
	public float timeWait = 2f;
	public float LOS = 3f;
	public float force = 100f;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine ("Patrol");
		Physics2D.queriesStartInColliders = false;
	}
	
	// Update is called once per frame
	void Update () 	
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.localScale.x * Vector2.right, LOS);
		if (hit.collider != null && hit.collider.CompareTag ("Player"))
		{
			this.GetComponent<Rigidbody2D> ().AddForce (Vector3.up * force + (hit.collider.transform.position - this.transform.position)*force);
		}
	}

	IEnumerator Patrol ()
	{
		while (true)
		{
			if (transform.position.x == patrolPoints [currentPoint].position.x)
			{
				currentPoint++;
				yield return new WaitForSeconds (timeWait);
			}

			if (currentPoint >= patrolPoints.Length)
				currentPoint = 0;
		
			transform.position = Vector2.MoveTowards (transform.position, new Vector2 (patrolPoints [currentPoint].position.x, transform.position.y), speed);
		

			if (transform.position.x > patrolPoints [currentPoint].position.x)
			{
				transform.localScale = new Vector3 (-1, 1, 1);
			}
			else if (transform.position.x < patrolPoints [currentPoint].position.x)
			{
				transform.localScale = Vector3.one;
			}
			yield return null;
		}
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, transform.position + transform.localScale.x * Vector3.right * LOS);
	}
}
