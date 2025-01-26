using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab; // Assign your bubble prefab here
    public TextMeshProUGUI bubbleCountText; // UI Text to display the spawn count
    public TextMeshProUGUI targetText; // UI Text to display the target count
    public TextMeshProUGUI keyDisplayText; // UI Text to show the current key to press

    private int bubbleCount = 0; // Counter for bubbles spawned
    private int targetBubbleCount; // Target number of bubbles
    private string currentKey; // Current key that the user needs to press

    private readonly string[] keys = { "W", "Q", "E", "R" }; // Array of the possible keys

    void Start()
    {
        // Randomly choose target bubble count between 20 and 50
        targetBubbleCount = Random.Range(10, 21);
        targetText.text = targetBubbleCount.ToString();

        // Display the current bubble count
        bubbleCountText.text = bubbleCount.ToString();

        // Set the initial key to be pressed
        SetRandomKey();
    }

    void Update()
    {
        // Check if the player presses the correct key and spawn the bubble if correct
        if (Input.GetKeyDown(KeyCode.W) && currentKey == "W")
        {
            SpawnBubble();
            SetRandomKey(); // Set the next random key
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currentKey == "Q")
        {
            SpawnBubble();
            SetRandomKey();
        }
        else if (Input.GetKeyDown(KeyCode.E) && currentKey == "E")
        {
            SpawnBubble();
            SetRandomKey();
        }
        else if (Input.GetKeyDown(KeyCode.R) && currentKey == "R")
        {
            SpawnBubble();
            SetRandomKey();
        }
    }

    void SpawnBubble()
    {
        // Generate a random X position within your screen's width
        float randomX = Random.Range(-10f, 10f);
        Vector3 spawnPosition = new Vector3(randomX, 5f, 0f); // Set spawn height to 5f above the screen

        // Spawn the bubble
        Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);

        // Increment the bubble count
        bubbleCount++;

        // Update the display of how many bubbles are spawned
        bubbleCountText.text = bubbleCount.ToString();
    }

    void SetRandomKey()
    {
        // Randomly select a key (W, Q, E, R)
        currentKey = keys[Random.Range(0, keys.Length)];

        // Update the on-screen text to show the new key to be pressed
        keyDisplayText.text = "Press: " + currentKey;
    }
}
