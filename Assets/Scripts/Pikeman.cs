using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pikeman : Enemy {

	public float walkDistance;

	private bool walk;
	private bool attack = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () {

		base.Update();

		anim.SetBool("Walk", walk);
		anim.SetBool("Attack", attack);

		if(Mathf.Abs(targetDistance) < walkDistance)
		{
			walk = true;
		}

		if(Mathf.Abs(targetDistance) < attackDistance)
		{
			attack = true;
			walk = false;
		}

		

	}

	private void FixedUpdate()
	{
		if(walk && !attack)
		{
			if(targetDistance < 0)
			{
				rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
				if (!facingRight)
				{
					Flip();
				}
			}
			else
			{
				rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
				if (facingRight)
				{
					Flip();
				}
			}
		}
	}

	public void ResetAttack()
	{
		attack = false;
	}

	
}
