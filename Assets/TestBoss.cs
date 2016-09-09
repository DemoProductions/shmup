using UnityEngine;
using System.Collections;

public class TestBoss : MonoBehaviour
{

	Health health;
	public Weapon weapon;
	Rigidbody2D rbody;
	TestBossAI testBossAI;
	SpriteRenderer spriterenderer;

	bool spawned = false;

	// Use this for initialization
	void Start ()
	{
		health = gameObject.GetComponent<Health> ();

		if (weapon)
		{
			weapon = Instantiate (weapon, new Vector3 (this.transform.position.x, this.transform.position.y, 0), this.transform.rotation) as Weapon;
			weapon.transform.SetParent (this.transform);
			weapon.enabled = false;
		}

		rbody = this.GetComponent<Rigidbody2D> ();

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
		testBossAI.enabled = true;
		weapon.enabled = true;
	}
}
