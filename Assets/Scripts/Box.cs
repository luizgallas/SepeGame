using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	public int health;

	private void OnTriggerEnter2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();

		if(player != null)
		{
			player.SetHealth(health);
			Destroy(gameObject);
		}
	}
}
