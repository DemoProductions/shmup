using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPowerUp : MonoBehaviour {

	[Range(0f,1f)]
	public float dropChance;
	public GameObject[] powerUps;

	private bool isQuitting = false;

	void OnApplicationQuit() {
		isQuitting = true;
	}

	void OnDestroy () {
		if (!isQuitting) {
			float dropRoll = Random.value;
			if (dropRoll <= dropChance) {
				GameObject powerUp = powerUps [Random.Range (0, powerUps.Length)];
				Instantiate (powerUp, this.transform.position, this.transform.rotation);
			}
		}
	}
}
