using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthBar : MonoBehaviour
{
    static int healthpointOffset = 125;

    public GameObject healthbarCanvasPrefab;
    public GameObject healthpointImageFullPrefab;
    public GameObject healthpointImageEmptyPrefab;
    public Vector3 pos = new Vector3 (0, 0, 0);

    GameObject healthbarCanvas;
    GameObject[] healthpointImages;
    int healthpointCursor; // keeps track of how much health the healthbar is showing

	// Use this for initialization
	void Start ()
    {
        if (healthbarCanvasPrefab)
        {
            healthbarCanvas = Instantiate (healthbarCanvasPrefab, pos, Quaternion.identity) as GameObject;

            // initialize healthpointImages array and healthpointCursor
            Health health = gameObject.GetComponent<Health> ();
            healthpointImages = new GameObject[health.hp];
            healthpointCursor = healthpointImages.Length - 1;
            
            // instantiate healthpoint images and add them to the healthpoints array
            for (int i = 0; i < health.hp; i++)
            {
                GameObject healthpointImage = Instantiate (healthpointImageFullPrefab, pos + new Vector3(i * healthpointOffset, pos.y, 0), Quaternion.identity) as GameObject;
                healthpointImage.transform.SetParent (healthbarCanvas.transform);
                healthpointImages[i] = healthpointImage;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        Health health = gameObject.GetComponent<Health> ();

        // healthpointsCursor to match the player's health
        // TODO: current assumption is that health can only go down, best to also account for cases when the health goes up.
        // also should have a loop for this instead of relying on Update()
        if (health.hp - 1 < healthpointCursor)
        {
            healthpointCursor = healthpointCursor - 1;
            GameObject healthpointImage = healthpointImages[healthpointCursor + 1];
            GameObject newHealthpointImage = Instantiate (healthpointImageEmptyPrefab, pos + new Vector3(healthpointImage.transform.position.x, healthpointImage.transform.position.y, 0), Quaternion.identity) as GameObject;
            newHealthpointImage.transform.SetParent (healthbarCanvas.transform);
            healthpointImages[healthpointCursor + 1] = healthpointImage;
            Destroy (healthpointImage.gameObject);
        }
	}
}
