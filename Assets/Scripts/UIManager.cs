using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text healthText;
	public Slider healthBar;

	// Use this for initialization
	void Awake () {
		UpdateHealthBar();
	}

	public void UpdateHealthUI(int health)
	{
		healthText.text = health.ToString();
		healthBar.value = health;
	}

	public void UpdateHealthBar()
	{
		healthBar.maxValue = GameManager.gameManager.health;
	}
}
