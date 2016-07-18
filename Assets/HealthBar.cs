using UnityEngine;

[RequireComponent (typeof(Health))]
public class HealthBar : MonoBehaviour
{

	public class HealthNode
	{
		public GameObject healthpointImageEmpty;
		public GameObject healthpointImageFull;
	}

	static int healthpointOffset = 25;

	public GameObject healthbarCanvasPrefab;

	// healthpoint images are anchored to the top left of the screen
	public GameObject healthpointImageFullPrefab;
	public GameObject healthpointImageEmptyPrefab;

	GameObject healthbarCanvas;
	HealthNode[] healthNodes;
	int healthpointCursor;
	// keeps track of how much health healthbarCanvas is showing

	// Use this for initialization
	void Start ()
	{
		if (healthbarCanvasPrefab)
		{
			healthbarCanvas = Instantiate (healthbarCanvasPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;

			// initialize healthNodes array and healthpointCursor
			Health health = gameObject.GetComponent<Health> ();
			healthNodes = new HealthNode[health.hp];
			healthpointCursor = healthNodes.Length - 1;

			// instantiate healthpoint images and add them to the healthNodes array
			for (int i = 0; i < health.hp; i++)
			{
				GameObject healthpointImageEmpty = Instantiate (healthpointImageEmptyPrefab, new Vector3 (i * healthpointOffset, 0, 0), Quaternion.identity) as GameObject;
				GameObject healthpointImageFull = Instantiate (healthpointImageFullPrefab, new Vector3 (i * healthpointOffset, 0, 0), Quaternion.identity) as GameObject;

				healthpointImageEmpty.transform.SetParent (healthbarCanvas.transform);
				healthpointImageFull.transform.SetParent (healthbarCanvas.transform);

				// initialize healthNodes to have full activated and empty deactivated
				HealthNode healthNode = new HealthNode ();
				healthNode.healthpointImageEmpty = healthpointImageEmpty;
				healthNode.healthpointImageFull = healthpointImageFull;

				healthNode.healthpointImageEmpty.gameObject.SetActive (false);

				healthNodes [i] = healthNode;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		Health health = gameObject.GetComponent<Health> ();

		// healthpointsCursor to match the player's health
		if (health.hp - 1 < healthpointCursor)
		{
			while (health.hp - 1 != healthpointCursor)
			{
				healthpointCursor = healthpointCursor - 1;
				healthNodes [healthpointCursor + 1].healthpointImageEmpty.SetActive (true);
				healthNodes [healthpointCursor + 1].healthpointImageFull.SetActive (false);
			}
		}
		else if (health.hp - 1 > healthpointCursor)
		{
			while (health.hp - 1 != healthpointCursor)
			{
				healthpointCursor = healthpointCursor + 1;
				healthNodes [healthpointCursor].healthpointImageFull.SetActive (true);
				healthNodes [healthpointCursor].healthpointImageEmpty.SetActive (false);
			}
		}
	}
}
