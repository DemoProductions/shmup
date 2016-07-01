using UnityEngine;

public class Player : MonoBehaviour
{

    public int speed = 100;
    public float xvelocity;
    public float yvelocity;

    Health health;
    Weapon weapon;
    Rigidbody2D rbody;

    // Use this for initialization
    void Start ()
    {
        health = gameObject.GetComponent<Health> ();
        weapon = gameObject.GetComponentInChildren<Weapon> ();
        rbody = gameObject.GetComponent<Rigidbody2D> ();
    }
    
    // Update is called once per frame
    void Update ()
    {
        xvelocity = Input.GetAxis ("Horizontal");
        yvelocity = Input.GetAxis ("Vertical");

        if (Input.GetAxis ("Fire1") > 0)
        {
            weapon.Shoot();
        }
    }

    void FixedUpdate()
    {
        rbody.velocity = new Vector2 (xvelocity, yvelocity) * Time.deltaTime * speed;
    }
}
