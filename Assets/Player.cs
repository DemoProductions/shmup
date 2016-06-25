using UnityEngine;

public class Player : MonoBehaviour
{

	public int speed = 100;
	public float xvelocity;
	public float yvelocity;

	public GameObject bullet;
	public float bullettime;
	public const float BULLET_DELAY = 0.1f;

    Health health;

	Rigidbody2D rbody;

	// Use this for initialization
	void Start ()
    {
        health = gameObject.GetComponent<Health> ();
		rbody = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
		xvelocity = Input.GetAxis ("Horizontal");
		yvelocity = Input.GetAxis ("Vertical");

		if (bullettime < BULLET_DELAY) bullettime += Time.deltaTime;

		if (Input.GetAxis ("Fire1") > 0 && bullettime >= BULLET_DELAY)
        {
			//I would argue we need a better way to store the bullet prefab, allowing upgrades and such to change this.
			Instantiate (this.bullet, this.transform.position + new Vector3(0.5f, 0, 0), this.transform.rotation);
			bullettime = 0;
		}
	}

	void FixedUpdate()
    {
		rbody.velocity = new Vector2 (xvelocity, yvelocity) * Time.deltaTime * speed;
	}
}
