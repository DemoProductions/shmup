﻿using UnityEngine;

public class Player : MonoBehaviour
{

    public int speed = 100;
    public float xvelocity;
    public float yvelocity;

    Health health;
    public GameObject weapon;
    Rigidbody2D rbody;

    // Use this for initialization
    void Start ()
    {
        health = gameObject.GetComponent<Health> ();

        weapon = Instantiate(weapon, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0), Quaternion.identity) as GameObject;
        weapon.transform.SetParent(this.gameObject.transform);

        rbody = gameObject.GetComponent<Rigidbody2D> ();
    }
    
    // Update is called once per frame
    void Update ()
    {
        xvelocity = Input.GetAxis ("Horizontal");
        yvelocity = Input.GetAxis ("Vertical");

        if (Input.GetAxis ("Fire1") > 0)
        {
            weapon.GetComponent<Weapon>().Shoot();
        }
    }

    void FixedUpdate()
    {
        rbody.velocity = new Vector2 (xvelocity, yvelocity) * Time.deltaTime * speed;
    }
}
