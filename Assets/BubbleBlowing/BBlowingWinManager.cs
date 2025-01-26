using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBlowingWinManager : MonoBehaviour
{
    private AllManager allManager;

    public GameObject clock;

    // Variable for the spawn location
    public Vector3 spawnLocation;
    public float checkDelay = 5f;

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
        if (clock != null)
        {
            Instantiate(clock, spawnLocation, Quaternion.identity); // Use spawnLocation
            Debug.Log("Object spawned at: " + spawnLocation);
        }

        // Start the second coroutine to spawn an object and check score after a delay
        StartCoroutine(DelayCheckScore());
    }

    private IEnumerator DelayCheckScore()
    {

        // Wait for the specified check delay before spawning the object
        yield return new WaitForSeconds(checkDelay);

        allManager.winned = true;
        allManager.LoadMainMenu();
    }
}
