using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void Update() {
        // Get the horizontal input (left or right arrow, or A/D keys by default)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Only flip the sprite if the direction has changed
        if (horizontalInput != 0) {
            // if going right facing left
            if (horizontalInput > 0 && spriteRenderer.flipX) 
            {
                spriteRenderer.flipX = false;

            }
            // if going left facing right
            else if (horizontalInput < 0 && !spriteRenderer.flipX) 
              {
                spriteRenderer.flipX = true;
            }
        }
    }
}