using Pathfinding;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]


public class EnemyFlying: MonoBehaviour
{

	public GameObject DesignatedArea;
	public Vector3 target;
	public Player_Controller player;
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


	public int health = 40;


	void Start ()
	{
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();
		player = FindObjectOfType<Player_Controller> ();

		if (target == null)
		{
			Debug.LogError ("No Target Found!");
			return;
		}

		seeker.StartPath (transform.position, target, OnPathComplete);

		StartCoroutine (UpdatePath ());

	}

	void Update()
	{
		if (health <= 0)
		{
			Destroy (this.gameObject);
		}
	}

	public void Damage (int damage)
	{
		health -= damage;
		//StartCoroutine (Flinch (flinchLength));
	}


	IEnumerator UpdatePath ()
	{
		target = new Vector3(player.transform.position.x, player.transform.position.y+5, player.transform.position.z);


		seeker.StartPath (transform.position, target, OnPathComplete);	

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
