using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDestroy : MonoBehaviour
{
public float speed = 2f; // Adjust speed as needed

    private void Update()
    {
        // Move the bubble downward
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Check if the bubble has reached the bottom of the screen (adjust Y value based on your camera view)
        if (transform.position.y < -6f) 
        {
            Destroy(gameObject); // Destroy the bubble if it's past the bottom
        }
    }
}
