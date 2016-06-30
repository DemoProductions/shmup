using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject[] bullets;
    public int bulletType = 0;
    public float refireTime;
    public float refireRate = 0.1f;
    public bool canShoot;
    public float xvelocity;
    public float yvelocity;

    void Start()
    {

    }

    void Update()
    {
		Projectile bullet = this.bullets[bulletType].GetComponent<Projectile> ();

		if (this.refireTime < this.refireRate) this.refireTime += Time.deltaTime;

		if (this.refireTime >= this.refireRate) {
			canShoot = true;
		}
		else
		{
			canShoot = false;
		}
    }

    public void Shoot()
    {
		Projectile bullet = this.bullets[bulletType].GetComponent<Projectile>();

        //I would argue we need a better way to store the bullet prefab, allowing upgrades and such to change this.
        Instantiate(this.bullets[bulletType], this.transform.position + new Vector3(xvelocity, yvelocity, 0), this.transform.rotation);
		this.refireTime = 0;
    }
}