using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 boundaryMin; 
    [SerializeField] private Vector2 boundaryMax;
    private void Update()
    {
        // Get player input
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // Move the player
        Vector3 movePlayer = transform.position + new Vector3(0, moveY, 0);

        // Clamp the background (cannt go past boundaries)
        movePlayer.y = Mathf.Clamp(movePlayer.y, boundaryMin.y, boundaryMax.y);

        // Apply the position
        transform.position = movePlayer;
    }
}
