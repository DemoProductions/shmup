using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthBar : MonoBehaviour
{
    static int healthpointOffset = 25;

    public GameObject healthbarCanvasPrefab;

    // healthpoint images are anchored to the top left of the screen
    public GameObject healthpointImageFullPrefab;
    public GameObject healthpointImageEmptyPrefab;

    GameObject healthbarCanvas;
    GameObject[] healthpointImages;
    int healthpointCursor; // keeps track of how much health healthbarCanvas is showing

	// Use this for initialization
	void Start ()
    {
        if (healthbarCanvasPrefab)
        {
            healthbarCanvas = Instantiate (healthbarCanvasPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            // initialize healthpointImages array and healthpointCursor
            Health health = gameObject.GetComponent<Health> ();
            healthpointImages = new GameObject[health.hp];
            healthpointCursor = healthpointImages.Length - 1;
            
            // instantiate healthpoint images and add them to the healthpoints array
            for (int i = 0; i < health.hp; i++)
            {
                GameObject healthpointImage = Instantiate (healthpointImageFullPrefab, new Vector3(i * healthpointOffset, 0, 0), Quaternion.identity) as GameObject;
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
        if (health.hp - 1 < healthpointCursor)
        {
            while (health.hp - 1 != healthpointCursor)
            {
                healthpointCursor = healthpointCursor - 1;
                GameObject healthpointImage = healthpointImages[healthpointCursor + 1];
                GameObject newHealthpointImage = Instantiate(healthpointImageEmptyPrefab, new Vector3(healthpointImage.transform.position.x, healthpointImage.transform.position.y, 0), Quaternion.identity) as GameObject;
                newHealthpointImage.transform.SetParent(healthbarCanvas.transform);
                healthpointImages[healthpointCursor + 1] = newHealthpointImage;
                Destroy(healthpointImage.gameObject);
            }
        }
        else if (health.hp - 1 > healthpointCursor)
        {
            while (health.hp - 1 != healthpointCursor)
            {
                healthpointCursor = healthpointCursor + 1;
                GameObject healthpointImage = healthpointImages[healthpointCursor];
                GameObject newHealthpointImage = Instantiate(healthpointImageFullPrefab, new Vector3(healthpointImage.transform.position.x, healthpointImage.transform.position.y, 0), Quaternion.identity) as GameObject;
                newHealthpointImage.transform.SetParent(healthbarCanvas.transform);
                healthpointImages[healthpointCursor] = newHealthpointImage;
                Destroy(healthpointImage.gameObject);
            }
        }
	}
}
