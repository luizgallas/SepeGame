using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 30;
	private int damage = 1;
	public float destroyTime = 1.5f;

	// Use this for initialization
	void Start () {

		Destroy(gameObject, destroyTime);
		damage = GameManager.gameManager.damage;

	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate(Vector3.right * speed * Time.deltaTime);

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Enemy otherEnemy = other.GetComponent<Enemy>();
		Boss boss = other.GetComponent<Boss>();

		if(otherEnemy != null)
		{
			otherEnemy.TookDamage(damage);
		}

		if (boss != null)
		{
			boss.TookDamage(damage);
		}
		Destroy(gameObject);
	}
}
