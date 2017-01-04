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
	int speed;

	// Use this for initialization
	void Start ()
	{
		testBoss = GetComponent<TestBoss> ();
		timer = GetComponent<Timer> ();
		rbody = GetComponent<Rigidbody2D> ();
		SwitchToRandomState ();
		timer.Begin ();
		rotation = 0;
		step = 0;
		speed = 20; // should this inherit from TestBoss.cs? MoveTo feels faster than our other speeds, not sure why.
	}
	
	// Update is called once per frame
	void Update ()
	{
		// stable, shoots bullets/missiles/bullets
		if (currentState == 0) {
			Vector2 topRight = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth - 20, Camera.main.pixelHeight - 20));
			Vector2 bottomRight = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth - 20, 20));

			// move to top of screen
			if (step == 0) {
				if (rbody.position != topRight) {
					rbody.position = Vector2.MoveTowards (rbody.position, topRight, this.speed * Time.deltaTime);
				}
				else {
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
					rbody.position = Vector2.MoveTowards (rbody.position, bottomRight, this.speed * Time.deltaTime);
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
					rbody.position = Vector2.MoveTowards (rbody.position, topRight, this.speed * Time.deltaTime);
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

		// slight move (uppper right), spin, slight move (center), spin, slight move (lower right), spin
		else if (currentState == 1) {
			Vector2 topRight = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth - 60, Camera.main.pixelHeight - 60));
			Vector2 bottomRight = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth - 60, 60));
			Vector2 center = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2));

			// move to upper right
			if (step == 0) {
				if (rbody.position != topRight) {
					rbody.position = Vector2.MoveTowards (rbody.position, topRight, this.speed * Time.deltaTime);
				}
				else {
					step++;
				}
			}

			// pause
			if (step == 1) {
				timer.Reset ().Begin ();
				step++;
			}

			// spin 360x2
			if (step == 2) {
				if (timer < 2f) {
					// wait
					return;
				}
				else if (rotation < 360 * 2) {
					testBoss.weapon.Shoot ();
					rotation += 5;
					transform.Rotate (new Vector3 (0, 0, 5));
				}
				else {
					rotation = 0;
					step++;
				}
			}

			// pause
			if (step == 3) {
				timer.Reset ().Begin ();
				step++;
			}

			// move to center
			if (step == 4) {
				if (timer < 2f) {
					// wait
					return;
				}
				else if (rbody.position != center) {
					rbody.position = Vector2.MoveTowards (rbody.position, center, this.speed * Time.deltaTime);
				}
				else {
					step++;
				}
			}

			// pause
			if (step == 5) {
				timer.Reset ().Begin ();
				step++;
			}

			// spin 360x2
			if (step == 6) {
				if (timer < 2f) {
					// wait
					return;
				}
				else if (rotation < 360 * 2) {
					testBoss.weapon.Shoot ();
					rotation += 5;
					transform.Rotate (new Vector3 (0, 0, 5));
				}
				else {
					rotation = 0;
					step++;
				}
			}

			// pause
			if (step == 7) {
				timer.Reset ().Begin ();
				step++;
			}

			// move to center
			if (step == 8) {
				if (timer < 2f) {
					// wait
					return;
				}
				else if (rbody.position != bottomRight) {
					rbody.position = Vector2.MoveTowards (rbody.position, bottomRight, this.speed * Time.deltaTime);
				}
				else {
					step++;
				}
			}

			// pause
			if (step == 9) {
				timer.Reset ().Begin ();
				step++;
			}

			// spin 360x2
			if (step == 10) {
				if (timer < 2f) {
					// wait
					return;
				}
				else if (rotation < 360 * 2) {
					testBoss.weapon.Shoot ();
					rotation += 5;
					transform.Rotate (new Vector3 (0, 0, 5));
				}
				else {
					rotation = 0;
					step++;
				}
			}

			// pause
			if (step == 11) {
				timer.Reset ().Begin ();
				step++;
			}

			// next state
			if (step == 12) {
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

		// slight rotations while shooting
		else if (currentState == 2) {
			Vector2 centerRight = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth - 60, Camera.main.pixelHeight / 2));
			Vector2 topRight = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth - 60, Camera.main.pixelHeight / 2 + 60));
			Vector2 bottomRight = Camera.main.ScreenToWorldPoint (new Vector2 (Camera.main.pixelWidth - 60, Camera.main.pixelHeight / 2 - 60));

			// move center right
			if (step == 0) {
				if (rbody.position != centerRight) {
					rbody.position = Vector2.MoveTowards (rbody.position, centerRight, this.speed * Time.deltaTime);
				}
				else {
					step++;
				}
			}

			// pause
			if (step == 1) {
				timer.Reset ().Begin ();
				step++;
			}

			// wiggle / shoot
			if (step == 2) {
				if (timer < 2f) {
					// wait
					return;
				}
				// 
				else if (rotation < 15) {
					testBoss.weapon.Shoot ();
					rotation += 3;
					transform.Rotate (new Vector3 (0, 0, 3));
				}
				else {
					rotation = 0;
					step++;
				}
			}
			if (step == 3) {
				if (rotation < 30) {
					testBoss.weapon.Shoot ();
					rotation += 3;
					transform.Rotate (new Vector3 (0, 0, -3));
				}
				else {
					rotation = 0;
					step++;
				}
			}
			if (step == 4) {
				if (rotation < 15) {
					testBoss.weapon.Shoot ();
					rotation += 3;
					transform.Rotate (new Vector3 (0, 0, 3));
				}
				else {
					rotation = 0;
					step++;
				}
			}

			// move top right
			if (step == 5) {
				if (rbody.position != topRight) {
					rbody.position = Vector2.MoveTowards (rbody.position, topRight, this.speed * Time.deltaTime);
				}
				else {
					step++;
				}
			}

			// wiggle / shoot
			if (step == 6) {
				if (rotation < 15) {
					testBoss.weapon.Shoot ();
					rotation += 3;
					transform.Rotate (new Vector3 (0, 0, 3));
				}
				else {
					rotation = 0;
					step++;
				}
			}
			if (step == 7) {
				if (rotation < 30) {
					testBoss.weapon.Shoot ();
					rotation += 3;
					transform.Rotate (new Vector3 (0, 0, -3));
				}
				else {
					rotation = 0;
					step++;
				}
			}
			if (step == 8) {
				if (rotation < 15) {
					testBoss.weapon.Shoot ();
					rotation += 3;
					transform.Rotate (new Vector3 (0, 0, 3));
				}
				else {
					rotation = 0;
					step++;
				}
			}

			// move bottom right
			if (step == 9) {
				if (rbody.position != bottomRight) {
					rbody.position = Vector2.MoveTowards (rbody.position, bottomRight, this.speed * Time.deltaTime);
				}
				else {
					step++;
				}
			}

			// wiggle / shoot
			if (step == 10) {
				if (rotation < 15) {
					testBoss.weapon.Shoot ();
					rotation += 3;
					transform.Rotate (new Vector3 (0, 0, 3));
				}
				else {
					rotation = 0;
					step++;
				}
			}
			if (step == 11) {
				if (rotation < 30) {
					testBoss.weapon.Shoot ();
					rotation += 3;
					transform.Rotate (new Vector3 (0, 0, -3));
				}
				else {
					rotation = 0;
					step++;
				}
			}
			if (step == 12) {
				if (rotation < 15) {
					testBoss.weapon.Shoot ();
					rotation += 3;
					transform.Rotate (new Vector3 (0, 0, 3));
				}
				else {
					rotation = 0;
					step++;
				}
			}

			// move center right
			if (step == 13) {
				if (rbody.position != centerRight) {
					rbody.position = Vector2.MoveTowards (rbody.position, centerRight, this.speed * Time.deltaTime);
				}
				else {
					step++;
				}
			}

			// pause
			if (step == 14) {
				timer.Reset ().Begin ();
				step++;
			}

			// next state
			if (step == 15) {
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
