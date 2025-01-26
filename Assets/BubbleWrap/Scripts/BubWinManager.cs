using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubWinManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int score = 0;
    private AllManager allManager;

    public GameObject objectToSpawn;  // Reference to the GameObject you want to spawn

    // Variable for the spawn location
    public Vector3 spawnLocation = Vector3.zero;  // Default location is (0, 0, 0)

    public float checkDelay = 5f;  // Time to wait before checking the score (default is 5 seconds)

    void Start()
    {
        //score = 0;
        //Manually find the SceneTransitionManager in the scene
        allManager = FindObjectOfType<AllManager>();

        // Check if the manager is found
        if (allManager == null)
        {
            Debug.LogError("allManager not found in the scene.");
        }
        StartCoroutine(SpawnObjectAfterDelay(2f));
    }

    private IEnumerator SpawnObjectAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        Debug.Log("Here");

        // Spawn the object at the specified spawn location
        if (objectToSpawn != null)
        {
            Instantiate(objectToSpawn, spawnLocation, Quaternion.identity); // Use spawnLocation
            Debug.Log("Object spawned at: " + spawnLocation);
        }

        // Start the second coroutine to spawn an object and check score after a delay
        StartCoroutine(DelayCheckScore());
    }

    private IEnumerator DelayCheckScore()
    {

        // Wait for the specified check delay before spawning the object
        yield return new WaitForSeconds(checkDelay);

        // Trigger action if score reaches 80
        if (score >= 16)
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


    // Update is called once per frame
    void Update()
    {
        if (score == 16)
        {
            allManager.LoadMainMenu();
        }
    }

    public void IncrementScore()
    {
        Debug.Log("Called Increment on" + score);
        score++;
        Debug.Log("Incremented" + score);
    }
}
