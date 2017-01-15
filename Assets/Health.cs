using UnityEngine;

public class Health : MonoBehaviour
{

	public int hp = 3;
	public GameObject explosionEffect;

	Flag flags;

	void Start ()
	{
		flags = GetComponent<Flag> ();
	}

	public void Damage (int damage)
	{
		if (flags == null || !flags.isImmune)
		{
			hp -= damage;
		}

		if (hp <= 0)
		{
			if (explosionEffect) {
				GameObject explosion = Instantiate (explosionEffect, transform.position, transform.rotation);
				explosion.transform.localScale = this.transform.localScale;
			}
			Destroy (this.gameObject);
		}
	}
}
