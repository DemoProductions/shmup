using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : PowerUp {

	public GameObject powerUpProjectile;

	private Weapon weapon;
	private GameObject previousProjectile;
	private int previousProjectileType;

	override protected void Setup () {
		weapon = player.GetComponentInChildren<Weapon> () as Weapon;

		// do nothing and destroy if cannot set powerup projectile
		if (!weapon) {
			Destroy (this.gameObject);
		}
		else {
			previousProjectileType = weapon.projectileType;
			previousProjectile = weapon.projectiles[previousProjectileType];
			weapon.projectiles[previousProjectileType] = powerUpProjectile;
		}
	}

	override protected void RemovePowerUp () {
		weapon.projectiles[previousProjectileType] = previousProjectile;
	}
}
