using UnityEngine;

public class Weapon : MonoBehaviour
{

	public GameObject[] projectiles;
	public int projectileType = 0;
	public float refireTime;
	public float refireRate;
	public float xvelocity;
	public float yvelocity;

	public GameObject CurrentProjectile {
		get {
			return projectiles[projectileType];
		}

		set {
			projectiles [projectileType] = value;
		}
	}

	void Start ()
	{

	}

	void Update ()
	{
		if (this.refireTime < this.refireRate) this.refireTime += Time.deltaTime;
	}

	public bool Shoot ()
	{
		//GameObject projectile = this.projectiles[this.projectileType];

		if (this.refireTime >= this.refireRate)
		{
			GameObject newProjectile = Instantiate (this.projectiles [this.projectileType], this.transform.position + new Vector3 (xvelocity, yvelocity, 0), this.transform.rotation) as GameObject;
			newProjectile.GetComponent<Team> ().team = transform.parent.GetComponent<Team> ().team;

			TrackingAI newProjectileTrackingAI = newProjectile.GetComponent<TrackingAI> ();
			if (newProjectileTrackingAI) newProjectileTrackingAI.parent = this.transform.parent.gameObject;

			this.refireTime = 0;
			return true;
		}

		return false;
	}

	public bool SwitchProjectile (int projectileType)
	{
		if (projectileType >= 0 && projectileType < projectiles.Length)
		{
			this.projectileType = projectileType;
			return true;
		}

		return false;
	}
}