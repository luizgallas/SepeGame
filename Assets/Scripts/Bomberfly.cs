using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomberfly : Enemy {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () {

		base.Update();

		if(Mathf.Abs(targetDistance) < attackDistance)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
		}

	}
}
