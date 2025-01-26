using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dancing : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to the player
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the W key is pressed
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Trigger the "MoveUp" animation
            animator.SetTrigger("Dance2_Frame1");
        }
    }
}
