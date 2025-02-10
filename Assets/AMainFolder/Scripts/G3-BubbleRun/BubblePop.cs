using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePop : MonoBehaviour
{
    public BubbleRunWinner bubbleRunWinner;

    void Start()
    {
        bubbleRunWinner = FindObjectOfType<BubbleRunWinner>();
        if (bubbleRunWinner == null)
        {
            Debug.LogError("BubbleRunWinner not found in the scene.");
        }
    }

    void OnTriggerEnter(Collider bubble)
    {
        Debug.Log("Bonk");
        if (bubble.gameObject.CompareTag("Player"))
        {
            bubbleRunWinner.scoreBR++;
            Debug.Log("Score updated to: " + bubbleRunWinner.scoreBR);
            Destroy(gameObject);
            Debug.Log("Bubble destroyed");
        }
    }
}
