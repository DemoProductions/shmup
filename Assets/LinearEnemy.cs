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
        Bullet bullet = weapon.bullets[weapon.bulletType].GetComponent<Bullet> ();
        weapon.bullettime += Time.deltaTime;

        /*if (Input.GetAxis ("Fire1") > 0 && bullettime >= BULLET_DELAY) {
            //I would argue we need a better way to store the bullet prefab, allowing upgrades and such to change this.
            Instantiate (this.bullet, this.transform.position, this.transform.rotation);
            bullettime = 0;
        }*/

        if (weapon.canShoot)
        {
            weapon.Shoot();
        }
    }

    void FixedUpdate()
    {
        rbody.velocity = new Vector2 (xvelocity, yvelocity) * Time.deltaTime * speed;
    }
}
