using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        weapon.GetComponent<Weapon>().Shoot();
    }

    void FixedUpdate()
    {
		
    }
}
