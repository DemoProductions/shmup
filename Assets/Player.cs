using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int speed = 100;
	public float xvelocity;
	public float yvelocity;
	public Vector2 movement;

	public GameObject bullet;
	public float bullettime;
	public const float BULLET_DELAY = 0.1f;

	private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
		xvelocity = 0;
		yvelocity = 0;

		bullettime = 0;

		rbody = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		xvelocity = Input.GetAxis ("Horizontal");
		yvelocity = Input.GetAxis ("Vertical");

		bullettime += Time.deltaTime;

		if (Input.GetAxis ("Fire1") > 0 && bullettime >= BULLET_DELAY) {
			//I would argue we need a better way to store the bullet prefab, allowing upgrades and such to change this.
			Instantiate (this.bullet, this.transform.position, this.transform.rotation * Quaternion.Euler(0, 0, 90));
			bullettime = 0;
		}
	}

	void FixedUpdate() {
		rbody.velocity = new Vector2 (xvelocity, yvelocity) * Time.deltaTime * speed;
	}
}
