using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int health = 3;
	public int damage = 1;
	public float fireRate = 2f;

	public static GameManager gameManager;

	// Use this for initialization
	void Awake () {
		
		if(gameManager == null)
		{
			gameManager = this;
		}
		else
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
