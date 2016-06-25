using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject[] bullets;
    public int bulletType = 0;
    public float bullettime;
    public float bullet_delay = 0.1f;
    public bool canShoot;
    public float xvelocity;
    public float yvelocity;

    void Start()
    {

    }

    void Update()
    {
        Bullet bullet = this.bullets[bulletType].GetComponent<Bullet> ();

        if (this.bullettime < this.bullet_delay) this.bullettime += Time.deltaTime;

        if (this.bullettime >= this.bullet_delay)
            canShoot = true;
        else
            canShoot = false;
    }

    public void Shoot()
    {
        Bullet bullet = this.bullets[bulletType].GetComponent<Bullet>();

        //I would argue we need a better way to store the bullet prefab, allowing upgrades and such to change this.
        Instantiate(this.bullets[bulletType], this.transform.position + new Vector3(xvelocity, yvelocity, 0), this.transform.rotation);
        this.bullettime = 0;
    }
}