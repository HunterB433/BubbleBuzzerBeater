using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G3BubbleSpawner : MonoBehaviour
{
    [Header("Bubble Spawn Settings")]
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private int numOfBubbles = 5;
    [SerializeField] private Vector3 spawnPosition = Vector3.zero;
    [SerializeField] private float YspawnRange = 5f;
    [SerializeField] private float XspawnRange = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn Bubbles
        SpawnBubbles();
    }
    private void SpawnBubbles()
    {
        // Loop equal to
        // Find why this triggers one time too short
        for (int i = 0; i < numOfBubbles; i++)
        {
            //Random.Range
            Vector3 ranSpawn = spawnPosition + new Vector3(Random.Range(-XspawnRange, XspawnRange), Random.Range(-YspawnRange, YspawnRange),0);
            // Create
            Instantiate(bubblePrefab, ranSpawn, Quaternion.identity);
        }
    }
}


