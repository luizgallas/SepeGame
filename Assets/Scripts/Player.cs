using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public float speed = 5f;
	public float jumpForce = 600;
	public GameObject bulletPrefab;
	public Transform shotSpawner;
	public float damageTime = 1f;
	public bool canFire = true;

	private Animator anim;
	private Rigidbody2D rb2d;
	private bool facingRight = true;
	private bool jump;
	private bool onGround = false;
	private Transform groundCheck;
	private float hForce = 0;
	private bool crouched;
	private bool lookingUp;
	private bool reloading;
	private float fireRate = 0.5f;
	private float nextFire;
	private bool tookDamage = false;

	private float reloadTime;
	private int health;
	private int maxHealth;

	private bool isDead = false;

	GameManager gameManager;

	// Use this for initialization
	void Start () {

		rb2d = GetComponent<Rigidbody2D>();
		groundCheck = gameObject.transform.Find("GroundCheck");
		anim = GetComponent<Animator>();

		gameManager = GameManager.gameManager;

		SetPlayerStatus();
		health = maxHealth;

		UpdateHealthUI();
	}
	
	// Update is called once per frame
	void Update () {

		if (!isDead)
		{
			onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

			
			if (onGround)
			{
				anim.SetBool("Jump", false);
			}

			if (Input.GetButtonDown("Jump") && onGround && !reloading)
			{
				jump = true;
			}
			else if (Input.GetButtonUp("Jump"))
			{
				if(rb2d.velocity.y > 0)
				{
					rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
				}
			}

			if (Input.GetButtonDown("Fire1") && Time.time > nextFire && !reloading && canFire)
			{
				nextFire = Time.time + fireRate;
				anim.SetTrigger("Shoot");
				GameObject tempBullet = Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
				if (!facingRight && !lookingUp)
				{
					tempBullet.transform.eulerAngles = new Vector3(0, 0, 180);
				}
				else if(!facingRight && lookingUp)
				{
					tempBullet.transform.eulerAngles = new Vector3(0, 0, 90);
				}
				if(crouched && !onGround)
				{
					tempBullet.transform.eulerAngles = new Vector3(0, 0, -90);
				}				
			}
			else if(Input.GetButtonDown("Fire1") && onGround)
			{
				StartCoroutine(Reloading());
			}

			lookingUp = Input.GetButton("Up");
			crouched = Input.GetButton("Down");

			anim.SetBool("LookingUp", lookingUp);
			anim.SetBool("Crouched", crouched);

			if (Input.GetButtonDown("Reload") && onGround)
			{
				StartCoroutine(Reloading());
			}

			if(Input.GetButtonDown("Fire2"))
			{
				// Rigidbody2D tempBomb = Instantiate(bomb, transform.position, transform.rotation);
				// if (facingRight)
				// {
				// 	tempBomb.AddForce(new Vector2(8, 10), ForceMode2D.Impulse);
				// }
				// else
				// {
				// 	tempBomb.AddForce(new Vector2(-8, 10), ForceMode2D.Impulse);
				// }
			}

			if((crouched || lookingUp || reloading) && onGround)
			{
				hForce = 0;
			}
		}

	}

	private void FixedUpdate()
	{
		if (!isDead)
		{
			if(!crouched && !lookingUp && !reloading)
			hForce = Input.GetAxisRaw("Horizontal");

			anim.SetFloat("Speed", Mathf.Abs(hForce));

			rb2d.velocity = new Vector2(hForce * speed, rb2d.velocity.y);

			if(hForce > 0 && !facingRight)
			{
				Flip();
			}
			else if(hForce < 0 && facingRight)
			{
				Flip();
			}

			if (jump)
			{
				anim.SetBool("Jump", true);
				jump = false;
				rb2d.AddForce(Vector2.up * jumpForce);
			}

		}
	}

	IEnumerator Reloading()
	{
		reloading = true;
		anim.SetBool("Reloading", true);
		yield return new WaitForSeconds(reloadTime);
		reloading = false;
		anim.SetBool("Reloading", false);
	}

	void Flip()
	{
		facingRight = !facingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	public void SetPlayerStatus()
	{
		fireRate = gameManager.fireRate;
		reloadTime = gameManager.reloadTime;
		maxHealth = gameManager.health;
	}

	void UpdateHealthUI()
	{
		FindObjectOfType<UIManager>().UpdateHealthUI(health);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Enemy") && !tookDamage)
		{
			StartCoroutine(TookDamage());
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Enemy") && !tookDamage)
		{
			StartCoroutine(TookDamage());
		}
	}

	IEnumerator TookDamage()
	{
		tookDamage = true;
		health--;
		UpdateHealthUI();
		if(health <= 0)
		{
			isDead = true;
			anim.SetTrigger("Death");
			Invoke("ReloadScene", 2f);
		}
		else
		{
			Physics2D.IgnoreLayerCollision(9, 10);
			for (float i = 0; i < damageTime; i+= 0.2f)
			{
				GetComponent<SpriteRenderer>().enabled = false;
				yield return new WaitForSeconds(0.1f);
				GetComponent<SpriteRenderer>().enabled = true;
				yield return new WaitForSeconds(0.1f);
			}
			Physics2D.IgnoreLayerCollision(9, 10, false);
			tookDamage = false;
		}
	}

	void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void SetHealth(int life)
	{
		health += life;
		if(health >= maxHealth)
		{
			health = maxHealth;
		}
		UpdateHealthUI();
	}
}
