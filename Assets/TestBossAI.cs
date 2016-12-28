using UnityEngine;
using System.Collections;

public class TestBossAI : MonoBehaviour
{

	int[] states = new int [3];
	public int currentState = 0;

	TestBoss testBoss;
	Rigidbody2D rbody;
	Timer timer;

	int rotation;
	public int step;

	// Use this for initialization
	void Start ()
	{
		testBoss = GetComponent<TestBoss> ();
		timer = GetComponent<Timer> ();
		rbody = GetComponent<Rigidbody2D> ();
//		SwitchToRandomState ();
		currentState = 0;
		timer.Begin ();
		rotation = 0;
		step = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		int speed = 100;
		float x = 0f;
		float y = 0f;

		// stable, shoots bullets/missiles/bullets
		if (currentState == 0)
		{
//			if (timer.time >= 0f && timer.time < 3f)
//			{
//				y = 1f;
//				GetComponent<TestBoss> ().weapon.SwitchProjectile (0);
//				GetComponent<TestBoss> ().weapon.Shoot ();
//			}
//			else if (timer.time >= 3f && timer.time < 5f)
//			{
//				y = 0f;
//				GetComponent<TestBoss> ().weapon.SwitchProjectile (1);
//				GetComponent<TestBoss> ().weapon.Shoot ();
//			}
//			else if (timer.time >= 5f && timer.time < 8f)
//			{
//				y = -1f;
//				GetComponent<TestBoss> ().weapon.SwitchProjectile (0);
//				GetComponent<TestBoss> ().weapon.Shoot ();
//			}
//			else if (timer.time >= 8f && timer.time < 10f)
//			{
//				y = 0f;
//			}
//
//			if (timer.time >= 10f)
//			{
//				timer.Reset ();
//				timer.Begin ();
//				SwitchToRandomState ();
//			}
			// move to top of screen
			Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth - 20, Camera.main.pixelHeight - 20));
			Vector2 bottomRight = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth - 20, 20));
			if (step == 0) {
				if (rbody.position != topRight) {
					rbody.position = Vector2.MoveTowards (rbody.position, topRight, 20 * Time.deltaTime);
				} else {
					step++;
				}
			}
				
			// pause
			if (step == 1) {
				timer.Reset ().Begin ();
				step++;
			}

			// shoot and move down
			if (step == 2) {
				if (timer < 2f) {
					// wait
					return;
				}
				else if (rbody.position != bottomRight) {
					testBoss.weapon.Shoot ();
					rbody.position = Vector2.MoveTowards (rbody.position, bottomRight, 20 * Time.deltaTime);
				}
				else {
					step++;
				}
			}

			// pause
			if (step == 3) {
				timer.Reset ().Begin ();
				step++;
			}

			// missiles
			if (step == 4) {
				if (timer < 2f) {
					// wait
					return;
				}
				else if (timer >= 2f && timer < 3f) {
					testBoss.weapon.SwitchProjectile (1);
					testBoss.weapon.Shoot ();
				}
				else {
					testBoss.weapon.SwitchProjectile (0);
					step++;
				}
			}

			// pause
			if (step == 5) {
				timer.Reset ().Begin ();
				step++;
			}

			// shoot and move up
			if (step == 6) {
				if (timer < 2f) {
					// wait
					return;
				}
				else if (rbody.position != topRight) {
					testBoss.weapon.Shoot ();
					rbody.position = Vector2.MoveTowards (rbody.position, topRight, 20 * Time.deltaTime);
				}
				else {
					step++;
				}
			}

			// pause
			if (step == 7) {
				timer.Reset ().Begin ();
				step++;
			}

			// next state
			if (step == 8) {
				if (timer < 2f) {
					// wait
					return;
				}
				else {
					step = 0;
					timer.Reset ().Begin ();
					SwitchToRandomState ();
				}
			}
			return;
		}

		// slight move, spin, slight move, spin, slight move, spin
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

		// slight rotations while shooting
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
