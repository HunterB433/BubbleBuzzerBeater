using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BubbleSpawn : MonoBehaviour
{
    // bubble spawms
    [SerializeField] private GameObject objectToSpawn;
    // num of objects
    [SerializeField] private int numOfObjects = 5;
    // position where object spawns
    [SerializeField] private Vector3 spawnPosition = new Vector3(0, 0, 0);
    // spawn range
    [SerializeField] private float spawnRange = 5f;

    private float timer;

    private void Start()
    {
        for (int i = 0; i < numOfObjects; i++)
        {
            // Calculate the spawn position for each object (adjusting by spacing)
            Vector3 spawnPos = spawnPosition + new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
        }
    }


}
