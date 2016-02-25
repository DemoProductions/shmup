using UnityEngine;
using System.Collections;

public class LinearEnemy : MonoBehaviour {

	public int speed;
	public float xvelocity;
	public float yvelocity;
	public Vector2 movement;

	public GameObject bullet;
	public float bullettime;
	public const float BULLET_DELAY = 1f;

	public int hp = 100;
	
	private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
		speed = 100;
		
		xvelocity = -1;
		yvelocity = 0;
		
		bullettime = 0;
		
		rbody = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		bullettime += Time.deltaTime;
		
		/*if (Input.GetAxis ("Fire1") > 0 && bullettime >= BULLET_DELAY) {
			//I would argue we need a better way to store the bullet prefab, allowing upgrades and such to change this.
			Instantiate (this.bullet, this.transform.position, this.transform.rotation);
			bullettime = 0;
		}*/

		if (bullettime >= BULLET_DELAY) {
			Instantiate (this.bullet, this.transform.position - new Vector3(0.5f, 0, 0), this.transform.rotation);
			bullettime = 0;
		}
	}

	void FixedUpdate() {
		rbody.velocity = new Vector2 (xvelocity, yvelocity) * Time.deltaTime * speed;
	}
}
