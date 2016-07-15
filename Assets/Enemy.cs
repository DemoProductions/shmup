using UnityEngine;


[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Health))]
[RequireComponent (typeof(EnemyMovement))]
[RequireComponent (typeof(BoundingsTimeout2D))]
[RequireComponent (typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    Health health;
    public Weapon weapon;
    Rigidbody2D rbody;
    EnemyMovement movement;
    BoundingsTimeout2D boundingstimeout;
    SpriteRenderer spriterenderer;

    bool spawned = false;

    // Use this for initialization
    void Start ()
    {
        health = gameObject.GetComponent<Health> ();

        if (weapon)
        {
            weapon = Instantiate (weapon, new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0), Quaternion.identity) as Weapon;
            weapon.transform.SetParent (this.gameObject.transform);
        }

        rbody = this.GetComponent<Rigidbody2D> ();

        movement = this.GetComponent<EnemyMovement> ();
        movement.enabled = false;

        boundingstimeout = this.GetComponent<BoundingsTimeout2D> ();
        boundingstimeout.enabled = false;

        spriterenderer = this.GetComponent<SpriteRenderer> ();
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (!spawned & spriterenderer.IsVisibleFrom (Camera.main))
        {
            Spawn ();
        }
        else
        {
            if (weapon) weapon.Shoot ();
        }
    }

    void Spawn ()
    {
        movement.enabled = true;
        boundingstimeout.enabled = true;
    }
}