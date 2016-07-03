using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        weapon.Shoot();
    }

    void FixedUpdate()
    {
		
    }
}
