using UnityEngine;

[RequireComponent (typeof(Health))]
public class HealthBar : MonoBehaviour
{

	public class HealthNode
	{
		public GameObject healthNodeImageEmpty;
		public GameObject healthNodeImageFull;
	}

	// y positions of healthbars depend on the playerNumber
	public int player1YOffset = 0;
	public int player2YOffset = -50;

	public int healthNodeOffset = 25; // amount of space between health nodes

	public GameObject healthBarCanvasPrefab;

	// healthNode images are anchored to some position in the screen
	public GameObject healthNodeImageFullPrefab;
	public GameObject healthNodeImageEmptyPrefab;

	GameObject healthbarCanvas;
	HealthNode[] healthNodes;
	int healthNodeCursor; // keeps track of how much health healthbarCanvas is showing
	int yOffset; // y position of this healthbar

	// Use this for initialization
	void Start ()
	{
		if (healthBarCanvasPrefab && healthNodeImageFullPrefab && healthNodeImageEmptyPrefab) 
		{
			healthbarCanvas = Instantiate (healthBarCanvasPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;

			// initialize healthNodes array and healthpointCursor
			Health health = gameObject.GetComponent<Health> ();
			healthNodes = new HealthNode[health.hp];
			healthNodeCursor = healthNodes.Length - 1;

			// location of healthNodes depend on the playerNumber set in Flag
			Player player = this.gameObject.GetComponent<Player> ();
			if (player.playerNumber == Player.PlayerEnum.player1)
			{
				yOffset = player1YOffset;
			} 
			else if (player.playerNumber == Player.PlayerEnum.player2)
			{
				yOffset = player2YOffset;
			}

			// instantiate healthpoint images and add them to the healthNodes array
			for (int i = 0; i < health.hp; i++)
			{
				GameObject healthpointImageEmpty = Instantiate (healthNodeImageEmptyPrefab, new Vector3 (i * healthNodeOffset, yOffset, 0), Quaternion.identity) as GameObject;
				GameObject healthpointImageFull = Instantiate (healthNodeImageFullPrefab, new Vector3 (i * healthNodeOffset, yOffset, 0), Quaternion.identity) as GameObject;

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
		else 
		{
			Debug.Log ("HealthBar prefab(s) not assigned in the editor");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		Health health = gameObject.GetComponent<Health> ();

		if (healthBarCanvasPrefab && healthNodeImageFullPrefab && healthNodeImageEmptyPrefab)
		{
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
}
