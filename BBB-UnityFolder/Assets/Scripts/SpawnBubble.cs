using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBubble : MonoBehaviour
{
    // Start is called before the first frame update
    public string anchorTag = "Anchor"; // Identifing tag
    public GameObject bubblePrefab; // Bubble Prefab
    void Start()
    {
        // Find all GO
        GameObject[] anchorObjects = GameObject.FindGameObjectsWithTag(anchorTag);
        Vector3 offset = new Vector3(0f, 0f, -1f);

        if(anchorObjects.Length == 0)
        {
            Debug.LogError("Anchor Tag Error");
            return;
        }

        if (bubblePrefab == null)
        {
            {
                // Debug
                Debug.LogError("PreFab Error");
                return;
            }
        }

        foreach (GameObject anchorObject in anchorObjects) // Finds Anchors in Kids
        {
            Instantiate(bubblePrefab, ((anchorObject.transform.position)+offset), anchorObject.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
