using UnityEngine;

public class HeartDisplay : MonoBehaviour
{
    public static HeartDisplay Instance;  // Singleton instance

    public GameObject heartPrefab;        // Prefab for the heart icon
    public Vector3 spawnPoint = Vector3.zero; // Position where the hearts will spawn (default is 0,0,0)
    public float xOffset = 2f;            // Offset for positioning hearts along the X-axis

    public GameController gameController;

    void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Destroy this duplicate
            return;
        }

        // Set the instance
        Instance = this;

        // Don't destroy this object on scene load
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Initialize hearts display
        //dUpdateHearts();
    }

    public void UpdateHearts()
    {
        Debug.Log("Hearts Triggered");
        // Destroy all hearts manually (not relying on hierarchy anymore)
        foreach (GameObject heart in GameObject.FindGameObjectsWithTag("Heart"))
        {
            Destroy(heart);
        }

        // Instantiate hearts based on the number of lives
        for (int i = 0; i < gameController.lives; i++)
        {
            // Compute position with offset starting at the spawnPoint
            Vector3 position = spawnPoint + new Vector3(i * xOffset, 0, 0);

            // Spawn the heart prefab at the specified position (no parent object specified)
            GameObject heart = Instantiate(heartPrefab, position, Quaternion.identity);
            heart.tag = "Heart"; // Ensure hearts have a specific tag for easier cleanup
        }
    }
}
