using UnityEngine;

[RequireComponent (typeof(Health))]
public class HealthBar : MonoBehaviour
{

	public class HealthNode
	{
		public GameObject healthNodeImageEmpty;
		public GameObject healthNodeImageFull;
	}

	public int healthNodeOffset = 25;

	public GameObject healthbarCanvasPrefab;

	// healthpoint images are anchored to the top left of the screen
	public GameObject healthNodeImageFullPrefab;
	public GameObject healthNodeImageEmptyPrefab;

	GameObject healthbarCanvas;
	HealthNode[] healthNodes;
	int healthNodeCursor; // keeps track of how much health healthbarCanvas is showing

	// Use this for initialization
	void Start ()
	{
		if (healthbarCanvasPrefab)
		{
			healthbarCanvas = Instantiate (healthbarCanvasPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;

			// initialize healthNodes array and healthpointCursor
			Health health = gameObject.GetComponent<Health> ();
			healthNodes = new HealthNode[health.hp];
			healthNodeCursor = healthNodes.Length - 1;

			// instantiate healthpoint images and add them to the healthNodes array
			for (int i = 0; i < health.hp; i++)
			{
				GameObject healthpointImageEmpty = Instantiate (healthNodeImageEmptyPrefab, new Vector3 (i * healthNodeOffset, 0, 0), Quaternion.identity) as GameObject;
				GameObject healthpointImageFull = Instantiate (healthNodeImageFullPrefab, new Vector3 (i * healthNodeOffset, 0, 0), Quaternion.identity) as GameObject;

				healthpointImageEmpty.transform.SetParent (healthbarCanvas.transform);
				healthpointImageFull.transform.SetParent (healthbarCanvas.transform);

				// initialize healthNodes to have full activated and empty deactivated
				HealthNode healthNode = new HealthNode ();
				healthNode.healthNodeImageEmpty = healthpointImageEmpty;
				healthNode.healthNodeImageFull = healthpointImageFull;

				healthNode.healthNodeImageEmpty.gameObject.SetActive (false);

				healthNodes [i] = healthNode;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		Health health = gameObject.GetComponent<Health> ();

		// healthpointsCursor to match the player's health
		if (healthNodeCursor > health.hp - 1)
		{
			while (healthNodeCursor != health.hp - 1)
			{
				healthNodes [healthNodeCursor].healthNodeImageEmpty.SetActive (true);
				healthNodes [healthNodeCursor].healthNodeImageFull.SetActive (false);
				healthNodeCursor--;
			}
		}
		else if (healthNodeCursor < health.hp - 1)
		{
			while (healthNodeCursor != health.hp - 1)
			{
				healthNodes [healthNodeCursor + 1].healthNodeImageFull.SetActive (true);
				healthNodes [healthNodeCursor + 1].healthNodeImageEmpty.SetActive (false);
				healthNodeCursor++;
			}
		}
	}
}
