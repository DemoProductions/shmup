using UnityEngine;

public class Health : MonoBehaviour
{

	public int hp = 3;

	void Start ()
	{
	}

	public void Damage (int damage)
	{
		hp -= damage;

		if (hp <= 0)
		{
			Destroy (this.gameObject);
		}
	}
}
