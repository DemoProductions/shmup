using UnityEngine;

public class Player : MonoBehaviour
{

	public enum PlayerEnum
	{
		player1,
		player2,
	};

	public PlayerEnum playerNumber;
	 
	public int speed = 100;
	public float xvelocity;
	public float yvelocity;

	Health health;
	public Weapon weapon;
	Rigidbody2D rbody;

	// Use this for initialization
	void Start ()
	{
		health = this.GetComponent<Health> ();

		if (weapon) {
			weapon = Instantiate (weapon, new Vector3 (this.transform.position.x, this.transform.position.y, 0), this.transform.rotation) as Weapon;
			weapon.transform.SetParent (this.transform);
		}

		rbody = this.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update ()
	{
		xvelocity = Input.GetAxis ("Horizontal");
		yvelocity = Input.GetAxis ("Vertical");

		if (Input.GetAxis ("Fire1") > 0 && weapon) {
			weapon.Shoot ();
		}
	}

	void FixedUpdate ()
	{
		rbody.velocity = new Vector2 (xvelocity, yvelocity) * Time.deltaTime * speed;
	}
}
