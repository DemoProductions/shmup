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
		GetComponent<TestBoss> ().weapon.refireRate = 0.01f;
		SwitchToRandomState ();
		timer.Begin ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		int speed = 100;
		float x = 0f;
		float y = 0f;

		if (currentState == 0)
		{
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
				timer.Reset ();
				timer.Begin ();
				SwitchToRandomState ();
			}
		}
		else if (currentState == 1)
		{
			if (timer.time >= 0f && timer.time < 2f)
			{
				x = -1;
				y = -1;
			}
			else if (timer.time >= 2f && timer.time < 6f)
			{
				transform.Rotate(new Vector3 (0, 0, 5));
				GetComponent<TestBoss> ().weapon.SwitchProjectile (0);
				GetComponent<TestBoss> ().weapon.Shoot ();
			}
			else if (timer.time >= 6f && timer.time < 10f)
			{
				x = 1;
			}
			else if (timer.time >= 10f && timer.time < 14f)
			{
				transform.Rotate(new Vector3 (0, 0, 5));
				GetComponent<TestBoss> ().weapon.SwitchProjectile (0);
				GetComponent<TestBoss> ().weapon.Shoot ();
			}
			else if (timer.time >= 14f && timer.time < 16f)
			{
				x = -1;
				y = 1;
			}
			else if (timer.time >= 16f && timer.time < 20f)
			{
				transform.Rotate(new Vector3 (0, 0, 5));
				GetComponent<TestBoss> ().weapon.SwitchProjectile (0);
				GetComponent<TestBoss> ().weapon.Shoot ();
			}

			if (timer.time >= 20f)
			{
				timer.Reset ();
				timer.Begin ();
				SwitchToRandomState ();
			}
		}
		else if (currentState == 2)
		{
			if (timer.time >= 0f && timer.time < 0.5f)
			{
				transform.Rotate(new Vector3 (0, 0, 3));
				GetComponent<TestBoss> ().weapon.SwitchProjectile (0);
				GetComponent<TestBoss> ().weapon.Shoot ();
			}
			else if (timer.time >= 0.5f && timer.time < 1f)
			{
				transform.Rotate(new Vector3 (0, 0, -3));
				GetComponent<TestBoss> ().weapon.SwitchProjectile (0);
				GetComponent<TestBoss> ().weapon.Shoot ();
			}
			else if (timer.time >= 1f && timer.time < 3f)
			{
				x = 1;
				y = -1;
			}
			else if (timer.time >= 3f && timer.time < 3.5f)
			{
				transform.Rotate(new Vector3 (0, 0, 3));
				GetComponent<TestBoss> ().weapon.SwitchProjectile (0);
				GetComponent<TestBoss> ().weapon.Shoot ();
			}
			else if (timer.time >= 3.5f && timer.time < 4f)
			{
				transform.Rotate(new Vector3 (0, 0, -3));
				GetComponent<TestBoss> ().weapon.SwitchProjectile (0);
				GetComponent<TestBoss> ().weapon.Shoot ();
			}
			else if (timer.time >= 4f && timer.time < 6f)
			{
				x = 1;
				y = 1;
			}
			else if (timer.time >= 6f && timer.time < 6.5f)
			{
				transform.Rotate(new Vector3 (0, 0, 3));
				GetComponent<TestBoss> ().weapon.SwitchProjectile (0);
				GetComponent<TestBoss> ().weapon.Shoot ();
			}
			else if (timer.time >= 6.5f && timer.time < 7f)
			{
				transform.Rotate(new Vector3 (0, 0, -3));
				GetComponent<TestBoss> ().weapon.SwitchProjectile (0);
				GetComponent<TestBoss> ().weapon.Shoot ();
			}
			else if (timer.time >= 7f && timer.time < 11f)
			{
				x = -1;
			}

			if (timer.time >= 11f)
			{
				timer.Reset ();
				timer.Begin ();
				SwitchToRandomState ();
			}
		}

		rbody.velocity = new Vector2 (x, y) * Time.deltaTime * speed;
	}

	// switch to random state. cannot be the same as current state
	void SwitchToRandomState ()
	{
		int r = Random.Range (0, states.Length);

		if (r == currentState)
		{
			r++;
			if (r > states.Length - 1) r = 0;
		}

		currentState = r;
	}
}
