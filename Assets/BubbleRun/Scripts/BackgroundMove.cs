using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.01f;
    [SerializeField] private bool horizontalScrolling = true;

    private Material backgroundMaterial;

    private void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
    }
    private void Update()
    {
        float offset = Time.time * scrollSpeed;

        // check for errors
        if (horizontalScrolling)
        {
            backgroundMaterial.mainTextureOffset = new Vector2(-offset, 0);
        }
        
    }
}
