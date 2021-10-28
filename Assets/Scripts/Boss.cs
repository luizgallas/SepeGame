using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {

	public Rigidbody2D bullet;
	public Transform[] shotSpawners;
	public float minYForce, maxYForce;
	public float fireRateMin, fireRateMax;

	public GameObject enemy;
	public Transform enemySpawn;
	public float minEnemyTime, maxEnemyTime;

	public GameObject laser;
	public Transform laserSpawn;
	public float minLaserTime, maxLaserTime;

	public int health;

	private bool isDead = false;
	private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {

		sprite = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ActivateBoss()
	{
		GetComponent<PolygonCollider2D>().enabled = true;
		Invoke("Fire", Random.Range(fireRateMin, fireRateMax));
		Invoke("InstantiateEnemies", Random.Range(minEnemyTime, maxEnemyTime));
		Invoke("FireLaser", Random.Range(minLaserTime, maxLaserTime));
	}

	void FireLaser()
	{
		if (!isDead)
		{
			Instantiate(laser, laserSpawn.position, laserSpawn.rotation);
			Invoke("FireLaser", Random.Range(minLaserTime, maxLaserTime));
		}
	}

	void InstantiateEnemies()
	{
		if (!isDead)
		{
			Instantiate(enemy, enemySpawn.position, enemySpawn.rotation);
			Invoke("InstantiateEnemies", Random.Range(minEnemyTime, maxEnemyTime));
		}
	}

	void Fire()
	{
		if (!isDead)
		{
			Rigidbody2D tempBullet = Instantiate(bullet, shotSpawners[Random.Range(0, shotSpawners.Length)].position, Quaternion.identity);
			tempBullet.AddForce(new Vector2(0, Random.Range(minYForce, maxYForce)), ForceMode2D.Impulse);
			Invoke("Fire", Random.Range(fireRateMin, fireRateMax));
		}
	}

	public void TookDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			isDead = true;
			Enemy[] enemies = FindObjectsOfType<Enemy>();
			foreach (Enemy enemy in enemies)
			{
				enemy.gameObject.SetActive(false);
			}
			Bullet[] bullets = FindObjectsOfType<Bullet>();
			foreach (Bullet bullet in bullets)
			{
				bullet.gameObject.SetActive(false);
			}

			Invoke("LoadScene", 2f);
			
		}
		else
		{
			StartCoroutine(TookDamageCoRoutine());
		}
	}

	IEnumerator TookDamageCoRoutine()
	{
		sprite.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		sprite.color = Color.white;
	}

	void LoadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
