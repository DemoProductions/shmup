using UnityEngine;
using System.Collections;
using System.Linq;

public class TrackingAI : MonoBehaviour {

	static LinearEnemy[] enemies;

	public float degreesPerSecond = 180;
	public float trackingRadius = 2;
//	public Team team;

	LinearEnemy target = null;

	// Use this for initialization
	void Start () {
		// replace with something like this in future and filter for other team
		//		Object.FindObjectOfType<Team>();
		// set once and is static. All enemies exist on spawn so we don't want to call this slow function repeatedly
		if (enemies == null)
			enemies = Object.FindObjectsOfType<LinearEnemy>();
		Debug.Log (enemies);
		UpdateTarget ();
	}

	void UpdateTarget() {
		// I love me some lambdas. Get enemies withing tracking range then sort for the closest
		LinearEnemy[] trackableEnemies = enemies.Where ((enemy) => (enemy.transform.position - this.transform.position).sqrMagnitude < trackingRadius * trackingRadius).ToArray();
		if (trackableEnemies.Length == 0) {
			target = null;
		} else {
			System.Array.Sort (trackableEnemies, (enemy1, enemy2) => (int)(enemy1.transform.position - this.transform.position).sqrMagnitude - (int)(enemy2.transform.position - this.transform.position).sqrMagnitude);
			target = trackableEnemies [0];
		}
	}

	
	// Update is called once per frame
	void Update () {
		UpdateTarget ();

		// when target is null, don't apply AI (continue flying in a straight line)
		if (target != null) {
			int sign = Vector3.Cross (target.transform.position - this.transform.position, this.transform.up).z < 0 ? 1 : -1;
			float angle = Vector3.Angle (target.transform.position - this.transform.position, this.transform.up);
			float rate = degreesPerSecond * Time.deltaTime;

			// if remaining angle is less than turn rate, use remaining angle (won't overshoot)
			rate = angle < rate ? angle : rate;

			transform.Rotate (Vector3.forward, sign * rate);
		}
	}

	void OnDrawGizmos() {
		if (target != null) {
			// draw line to target
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine (this.transform.position, target.transform.position);

			// draw ray in facing direction
			Gizmos.color = Color.red;
			Gizmos.DrawRay (this.transform.position, this.transform.up);
		}

		// draw tracking radius
		Gizmos.color = Color.yellow;
		float theta = 0;
		float x = trackingRadius * Mathf.Cos(theta);
		float y = trackingRadius * Mathf.Sin(theta);
		Vector3 pos = transform.position + new Vector3(x, y, 0);
		Vector3 newPos = pos;
		Vector3 lastPos = pos;
		for(theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f){
			x = trackingRadius * Mathf.Cos(theta);
			y = trackingRadius * Mathf.Sin(theta);
			newPos = transform.position + new Vector3(x, y, 0);
			Gizmos.DrawLine(pos, newPos);
			pos = newPos;
		}
		Gizmos.DrawLine(pos,lastPos);
	}
}
