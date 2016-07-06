using UnityEngine;
using System.Collections;
using UnityEditor;

public class Wave : MonoBehaviour
{
	[System.Serializable]
	public class Spawnable
	{
		public GameObject gameObject;
		public Vector2 position;
	}

	public Spawnable[] spawnables;

	// Use this for initialization
	void Start ()
	{
		foreach (Spawnable spawnable in spawnables)
		{
			Instantiate (spawnable.gameObject, new Vector3(spawnable.position.x + this.transform.position.x, spawnable.position.y), Quaternion.identity);
		}
	}
}