using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
	public float fallMultiplier = 3.25f;
	public float lowJumpMultiplier = 3.1f;

	Rigidbody2D rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		if (rb.velocity.y < 0)
		{
			//Maximum jump/normal jump
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}
		//if you let go of jump early, you jump shorter
		else if (rb.velocity.y > 0 && !Input.GetKey (KeyCode.Space))
		{
			rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
	}
}
