using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Health))]
public class HealthBar : MonoBehaviour
{

	public class HealthNode
	{
		public GameObject healthNodeImageEmpty;
		public GameObject healthNodeImageFull;
	}

	public Vector2 pos;

	public GameObject healthNodeImageFullPrefab;
	public GameObject healthNodeImageEmptyPrefab;

	HealthNode[] healthNodes;
	int healthNodeCursor;

	const string healthBarCanvasName = "HealthBarCanvas";
	const string mainCameraTag = "MainCamera";

	// Use this for initialization
	void Start ()
	{
		if (healthNodeImageFullPrefab && healthNodeImageEmptyPrefab) 
		{
			// initialize canvas for healthbar
			GameObject healthBarCanvas = new GameObject (healthBarCanvasName, typeof (RectTransform));
			healthBarCanvas.GetComponent<RectTransform> ().position = Vector3.zero;
			healthBarCanvas.AddComponent<Canvas> ();
			healthBarCanvas.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceCamera;
			healthBarCanvas.GetComponent<Canvas> ().worldCamera = GameObject.FindGameObjectWithTag (mainCameraTag).GetComponent<Camera> ();
			healthBarCanvas.AddComponent<CanvasScaler> ();
			healthBarCanvas.AddComponent<GraphicRaycaster> ();

			// initialize healthNodes array and healthpointCursor
			Health health = GetComponent<Health> ();
			healthNodes = new HealthNode [health.hp];
			healthNodeCursor = healthNodes.Length - 1;

			// instantiate healthpoint images and add them to the healthNodes array
			for (int i = 0; i < health.hp; i++)
			{
				GameObject healthpointImageEmpty = Instantiate (healthNodeImageEmptyPrefab, new Vector3 (pos.x + i, pos.y, 0), Quaternion.identity) as GameObject;
				GameObject healthpointImageFull = Instantiate (healthNodeImageFullPrefab, new Vector3 (pos.x + i, pos.y, 0), Quaternion.identity) as GameObject;

				healthpointImageEmpty.transform.SetParent (healthBarCanvas.transform);
				healthpointImageFull.transform.SetParent (healthBarCanvas.transform);

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
		Health health = GetComponent<Health> ();

		if (healthNodeImageFullPrefab && healthNodeImageEmptyPrefab)
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
