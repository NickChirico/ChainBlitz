using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

	private bool attacking = false;

	public float attackTimer = 0f;
	public float attackCD = 0.3f;

	public Collider2D attackTrigger;

	private Animator anim;


	void Start ()
	{
		anim = gameObject.GetComponent<Animator> ();
		attackTrigger.enabled = false;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.JoystickButton2) && !attacking)
		{
			attacking = true;
			attackTimer = attackCD;

			attackTrigger.enabled = true;
		}

		if (attacking)
		{
			if (attackTimer > 0)
				attackTimer -= Time.deltaTime;
			else
			{
				attacking = false;
				attackTrigger.enabled = false;
			}
		}

		anim.SetBool ("Attacking", attacking);
	}
}
