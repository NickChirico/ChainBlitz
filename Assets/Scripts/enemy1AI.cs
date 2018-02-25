using Pathfinding;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]


public class enemy1AI : MonoBehaviour
{

	public GameObject DesignatedArea;
	public Transform target;
	public float updateRate = 2f;
	//updates x times per second.
	private Seeker seeker;
	private Rigidbody2D rb;

	//public bool isSeeking = false;

	//store the calculated path
	public Path path;

	// AI speed per second
	public float speed = 300f;
	public ForceMode2D fMode;

	[HideInInspector]
	public bool pathIsEnded = false;

	public float nextWaypointDistance = 3f;
	//max distance from AI to a waypoint
	private int currentWaypoint = 0;
	// waypoint currently moving towards


	void Start ()
	{
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if (target == null)
		{
			Debug.LogError ("No Target Found!");
			return;
		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath ());

	}

	IEnumerator UpdatePath ()
	{
		//if there is no target;
		if (target == null)
		{
			//TODO: Player search? if need be
			yield break;
		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);	

		yield return new WaitForSeconds (1f / updateRate);
		StartCoroutine (UpdatePath ()); // call itself again every 'x' seconds

	}

	public void OnPathComplete (Path p)
	{
		Debug.Log ("got a path. Is there an error? " + p.error);

		if (!p.error)
		{
			path = p;
			currentWaypoint = 0;
		}
	}

	
	void FixedUpdate ()
	{
		if (target == null)
		{
			//TODO: Player search
			return;
		}

		//TODO: always look at player;


		if (path == null)
			return;

		if (currentWaypoint >= path.vectorPath.Count)
		{
			if (pathIsEnded)
				return;

			Debug.Log ("Path has been reached");
			pathIsEnded = true;
			return;
		}


		pathIsEnded = false;

		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized; //return normalized direction vector
		dir *= speed * Time.fixedDeltaTime;

		//Now we have direction, so we actually move the Ai in the direction;

		// CHANGE eventually
		rb.AddForce (dir, fMode);

		float dist = Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]);
		if (dist < nextWaypointDistance)
		{
			currentWaypoint++;
			return;
		}

	}



}
