using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DanceWinManager : MonoBehaviour
{
    private BubbleSpawner bubbleSpawner; // Reference to the BubbleSpawner to get the counts

    void Start()
    {


        // Find the BubbleSpawner to access the bubble counts
        bubbleSpawner = FindObjectOfType<BubbleSpawner>();

        // Check if the BubbleSpawner is found
        if (bubbleSpawner == null)
        {
            Debug.LogError("BubbleSpawner not found in the scene.");
        }
    }

    void Update()
    {
        // Check if the bubbleCount matches the targetBubbleCount from BubbleSpawner
        if (bubbleSpawner != null && bubbleSpawner.bubbleCount >= bubbleSpawner.targetBubbleCount)
        {
            // Player has reached the target bubble count
            WinCondition();
        }
    }

    // Called when the win condition is met (i.e., bubble count reaches the target)
    private void WinCondition()
    {
        // Load the next scene (main menu, or another scene of your choice)
        SceneManager.LoadScene("LeadIn");
    }
}
