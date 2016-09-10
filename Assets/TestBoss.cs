﻿using UnityEngine;
using System.Collections;

public class TestBoss : MonoBehaviour
{

	Health health;
	public Weapon weapon;
	TestBossAI testBossAI;
	SpriteRenderer spriterenderer;

	bool spawned = false;

	// Use this for initialization
	void Start ()
	{
		health = gameObject.GetComponent<Health> ();
		health.enabled = false;

		if (weapon)
		{
			weapon = Instantiate (weapon, new Vector3 (this.transform.position.x, this.transform.position.y, 0), this.transform.rotation) as Weapon;
			weapon.transform.SetParent (this.transform);
			weapon.enabled = false;
		}

		testBossAI = this.GetComponent<TestBossAI> ();
		testBossAI.enabled = false;

		spriterenderer = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!spawned && spriterenderer.IsVisibleFrom (Camera.main))
		{
			Spawn ();
		}
	}

	void Spawn ()
	{
		health.enabled = true;
		testBossAI.enabled = true;
		weapon.enabled = true;
	}
}
