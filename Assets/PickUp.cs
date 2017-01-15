using UnityEngine;
using System.Collections;
using System;

public class PickUp : MonoBehaviour {

	public GameObject powerUp;

	private Type powerUpType;

	void OnTriggerEnter2D(Collider2D other) {
		Player player = other.GetComponent<Player> ();
		if (player) {
			this.AddPowerup (player);
			Destroy (this.gameObject);
		}
	}

	// to be overridden by subclasses
	void AddPowerup(Player player) {
		PowerUp[] playerPowerUps = player.GetComponentsInChildren <PowerUp> ();
		powerUpType = powerUp.GetComponentInChildren<PowerUp> (true).GetType ();

		// if powerup of same type is found, reset timer
		foreach (PowerUp playerPowerUp in playerPowerUps) {
			if (ReferenceEquals(powerUpType, playerPowerUp.GetType())) {
				playerPowerUp.ResetTime ();
				return;
			}
		}

		// otherwise add new powerup
		PowerUp powerUpObject = (PowerUp)(Instantiate (this.powerUp, player.transform.position, player.transform.rotation) as GameObject).GetComponent<PowerUp>();
		powerUpObject.transform.parent = player.transform;
		powerUpObject.player = player;
	}
}
