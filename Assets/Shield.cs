using UnityEngine;
using System.Collections;

public class Shield : PowerUp {
	
	Flag flags;

	override protected void Setup () {
		flags = player.GetComponent<Flag> ();

		// do nothing and destroy if cannot set immunity
		if (flags == null) {
			Destroy (this.gameObject);
		}
		else {
			flags.isImmune = true;
		}
	}

	override protected void RemovePowerUp () {
		flags.isImmune = false;
	}
}
