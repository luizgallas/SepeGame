using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Enemy {

	public GameObject bulletPrefab;
	public float fireRate;
	public Transform shotSpawner;
	private float nextFire;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () {

		base.Update();
		
		if(targetDistance < 0)
		{
			if (!facingRight)
			{
				Flip();
			}
		}
		else
		{
			if (facingRight)
			{
				Flip();
			}
		}

		if(Mathf.Abs(targetDistance) < attackDistance && Time.time > nextFire)
		{
			anim.SetTrigger("Shooting");
			nextFire = Time.time + fireRate;
		}

	}

	public void Shooting()
	{
		GameObject tempBullet = Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
		if (!facingRight)
		{
			tempBullet.transform.eulerAngles = new Vector3(0, 0, 180);
		}
	}

}
