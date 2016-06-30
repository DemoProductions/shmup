using UnityEngine;

public class LinearEnemy : MonoBehaviour
{

    public int speed = 100;
    public float xvelocity = -1;
    public float yvelocity;

    Health health;
    Weapon weapon;
    Rigidbody2D rbody;

    // Use this for initialization
    void Start ()
    {
        health = gameObject.GetComponent<Health> ();
        weapon = gameObject.GetComponent<Weapon> ();
        rbody = gameObject.GetComponent<Rigidbody2D> ();
    }
    
    // Update is called once per frame
    void Update ()
    {
        weapon.Shoot();
    }

    void FixedUpdate()
    {
        rbody.velocity = new Vector2 (xvelocity, yvelocity) * Time.deltaTime * speed;
    }
}
