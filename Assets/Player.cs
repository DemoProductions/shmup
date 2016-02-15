using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int speed = 100;
	public float xvelocity;
	public float yvelocity;
	public Vector2 movement;

	private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
		xvelocity = 0;
		yvelocity = 0;

		rbody = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		xvelocity = Input.GetAxis ("Horizontal");
		yvelocity = Input.GetAxis ("Vertical");
	}

	void FixedUpdate() {
		rbody.velocity = new Vector2 (xvelocity, yvelocity) * Time.deltaTime * speed;
	}
}
