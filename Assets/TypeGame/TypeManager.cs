using UnityEngine;
using TMPro;
using System.Collections;

public class TypeManager : MonoBehaviour
{
    private int score = 0;
    private AllManager allManager;

    // Reference to the Text or TMP_Text component
    public Canvas canvas;
    public TMP_Text textBox;  // Use 'TMP_Text' if you're using TextMeshPro instead of 'Text'
    public GameObject objectToSpawn;  // Reference to the GameObject you want to spawn

    // Variable for the spawn location
    public Vector3 spawnLocation = Vector3.zero;  // Default location is (0, 0, 0)

    public float checkDelay = 5f;  // Time to wait before checking the score (default is 5 seconds)

    void Start()
    {
        // Manually find the SceneTransitionManager in the scene
        allManager = FindObjectOfType<AllManager>();

        // Check if the manager is found
        if (allManager == null)
        {
            Debug.LogError("allManager not found in the scene.");
        }

        // Check if textBox is assigned
        if (textBox == null)
        {
            Debug.LogError("Text box is not assigned in the inspector!");
        }

        // Initially disable the Canvas
        canvas.gameObject.SetActive(false);

        // Start the coroutine to enable the Canvas after 2 seconds
        StartCoroutine(EnableCanvasAfterDelay(2f));
    }

    private IEnumerator EnableCanvasAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Re-enable the Canvas
        canvas.gameObject.SetActive(true);

        // Start the second coroutine to spawn an object and check score after a delay
        StartCoroutine(SpawnObjectAndCheckScore());
    }

    private IEnumerator SpawnObjectAndCheckScore()
    {
        Debug.Log("Here");

        // Spawn the object at the specified spawn location
        if (objectToSpawn != null)
        {
            Instantiate(objectToSpawn, spawnLocation, Quaternion.identity); // Use spawnLocation
            Debug.Log("Object spawned at: " + spawnLocation);
        }

        // Wait for the specified check delay before spawning the object
        yield return new WaitForSeconds(checkDelay);

        // Now, check the score
        if (textBox != null)
        {
            score = textBox.text.Length;  // Update score based on text box length
            Debug.Log("Character count: " + score);

            // Trigger action if score reaches 80
            if (score >= 80)
            {
                Debug.Log("Win");
                allManager.winned = true;
                allManager.LoadMainMenu();
            }
            else
            {
                Debug.Log("Failed");
                allManager.winned = false;
                allManager.LoadMainMenu();
            }
        }
    }
}
