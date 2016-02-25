using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int speed;

	private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
		speed = 500;

		rbody = gameObject.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update() {

	}

	void FixedUpdate () {
		rbody.velocity = this.transform.up * Time.deltaTime * speed;
	}
}
