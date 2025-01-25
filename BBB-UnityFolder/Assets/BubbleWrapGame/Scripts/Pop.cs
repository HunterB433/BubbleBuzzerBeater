using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop : MonoBehaviour
{
    public Material mat1;
    public Material mat2;

    private Renderer objectRenderer;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        // Change
        if (objectRenderer != null) 
        {
          objectRenderer.material = mat2;

            StartCoroutine(ResetMaterialAfterDelay(1f));
        }
    }

    IEnumerator ResetMaterialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (objectRenderer != null)
        {
            objectRenderer.material = mat1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
