using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			FindObjectOfType<Boss>().ActivateBoss();
			FindObjectOfType<UnityStandardAssets._2D.CameraFollow>().minXAndY = new Vector2(73.4f, 0);
			gameObject.SetActive(false);
		}
	}
}
