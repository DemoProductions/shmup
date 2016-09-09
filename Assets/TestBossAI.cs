using UnityEngine;
using System.Collections;

public class TestBossAI : MonoBehaviour
{

	int[] states = new int [3];
	public int currentState = 0;

	Rigidbody2D rbody;
	Timer timer;

	// Use this for initialization
	void Start ()
	{
		timer = GetComponent<Timer> ();
		rbody = GetComponent<Rigidbody2D> ();

		timer.Begin ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (currentState == 0)
		{
			int speed = 100;
			float x = 0f;
			float y = 0f;

			if (timer.time >= 0f && timer.time < 3f)
			{
				y = 1f;
				GetComponent<TestBoss> ().weapon.SwitchProjectile (0);
				GetComponent<TestBoss> ().weapon.Shoot ();
			}
			else if (timer.time >= 3f && timer.time < 5f)
			{
				y = 0f;
				GetComponent<TestBoss> ().weapon.SwitchProjectile (1);
				GetComponent<TestBoss> ().weapon.Shoot ();
			}
			else if (timer.time >= 5f && timer.time < 8f)
			{
				y = -1f;
				GetComponent<TestBoss> ().weapon.SwitchProjectile (0);
				GetComponent<TestBoss> ().weapon.Shoot ();
			}
			else if (timer.time >= 8f && timer.time < 10f)
			{
				y = 0f;
			}

			if (timer.time >= 10f)
			{
				timer. Reset ();
				timer. Begin ();
			}

			rbody.velocity = new Vector2 (x, y) * Time.deltaTime * speed;
		}
		else if (currentState == 1)
		{

		}
		else if (currentState == 2)
		{

		}

		/*if (timer.time >= 15f)
		{
			switchToRandomState ();
			timer.Reset ();
			timer.Begin ();
		}*/
	}

	// switch to random state. cannot be the same as current state
	void switchToRandomState ()
	{
		int r = Random.Range (0, states.Length);

		if (r == currentState)
		{
			r++;
			if (r > states.Length - 1) r = 0;
		}
	}
}
