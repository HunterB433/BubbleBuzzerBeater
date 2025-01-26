using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 boundaryMin; 
    [SerializeField] private Vector2 boundaryMax;
    

    private bool isSwimming;
    private void Update()
    {
        // Get player input
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        // Move the player
        Vector3 movePlayer = transform.position + new Vector3(moveX, moveY, 0);

        // Clamp the background (cannt go past boundaries)
        movePlayer.y = Mathf.Clamp(movePlayer.y, boundaryMin.y, boundaryMax.y);
        movePlayer.x = Mathf.Clamp(movePlayer.x, boundaryMin.x, boundaryMax.x);

        // Apply the position
        transform.position = movePlayer;

        // Get input (A/D or arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // If there's input, play the walk animation
        if (horizontalInput != 0 | verticalInput != 0) {
            isSwimming = true;
        } else {
           isSwimming = false;
        }



    }

    public bool IsSwimming() {
        return isSwimming;
    }
}
