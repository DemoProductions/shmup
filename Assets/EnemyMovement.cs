using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public int speed = 100;

//	[Header("X Variables")]
	public float xVelocity = -1;
	public bool xSin = false;
	public float xSinRate = 1;

//	[Header("Y Variables")]
	public float yVelocity;
	public bool ySin = false;
	public float ySinRate = 1;

	Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
		rbody = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = xVelocity;
		float y = yVelocity;

		if (xSin) {
			x *= Mathf.Sin (Time.time * xSinRate);
		}
		if (ySin) {
			y *= Mathf.Sin (Time.time * ySinRate);
		}

		rbody.velocity = new Vector2 (x, y) * Time.deltaTime * speed;
	}
}
