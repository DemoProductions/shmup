using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject[] projectiles;
    public int projectileType = 0;
    public float refireTime;
    public float refireRate = 0.1f;
    public float xvelocity;
    public float yvelocity;

    void Start()
    {

    }

    void Update()
    {
        if (this.refireTime < this.refireRate) this.refireTime += Time.deltaTime;
    }

    public bool Shoot()
    {
        GameObject projectile = this.projectiles[this.projectileType];

        if (this.refireTime >= this.refireRate)
        {
            Instantiate(this.projectiles[this.projectileType], this.transform.position + new Vector3(xvelocity, yvelocity, 0), this.gameObject.transform.parent.transform.rotation);
            this.refireTime = 0;
            return true;
        }

        return false;
    }
}