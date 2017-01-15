using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour {

	public Player player;
	public float powerUpTime = 10;

	private float currentTime;

	// Use this for initialization
	void Start () {
		currentTime = 0;
		Setup ();
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;

		if (currentTime >= powerUpTime) {
			RemovePowerUp ();
			Destroy (this.gameObject);
		}
	}

	public void ResetTime () {
		currentTime = 0;
	}


	abstract protected void Setup ();
	abstract protected void RemovePowerUp ();
}
