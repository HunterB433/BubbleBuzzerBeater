using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 boundaryMin;
    [SerializeField] private Vector2 boundaryMax;
    private void Update()
    {
        // get player input
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float moveY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(-(inputVector.x), -(inputVector.y),0f);
        
        // move background
        //Vector3 moveBackground = transform.position + new Vector3(moveX, moveY, 0f);

        // clamp background (cannot move outside boundries)
        //transform.position += moveBackground
    }
}
