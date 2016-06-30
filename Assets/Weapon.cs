using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject[] bullets;
    public int bulletType = 0;
    public float refireTime;
    public float refireRate = 0.1f;
    public float xvelocity;
    public float yvelocity;

    void Start()
    {

    }

    void Update()
    {
		Projectile bullet = this.bullets[bulletType].GetComponent<Projectile> ();

		if (this.refireTime < this.refireRate) this.refireTime += Time.deltaTime;
    }

    public bool Shoot()
    {
		Projectile bullet = this.bullets[bulletType].GetComponent<Projectile>();

        if (this.refireTime >= this.refireRate)
        {
            Instantiate(this.bullets[bulletType], this.transform.position + new Vector3(xvelocity, yvelocity, 0), this.transform.rotation);
            this.refireTime = 0;
            return true;
        }

        return false;
    }
}