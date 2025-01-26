using UnityEngine;
using UnityEngine.UI; // Add this if you're using Unity's built-in UI Text
// or 
using TMPro; // Add this if you're using TextMeshPro

using System;
using System.Collections;
using System.Collections.Generic;

public class TypeManager : MonoBehaviour
{
    private int score = 0;
    private AllManager allManager;

    // Reference to the Text or TMP_Text component
    public Canvas canvas;
    public TMP_Text textBox;  // Use 'TMP_Text' if you're using TextMeshPro instead of 'Text'

    void Start()
    {
        // Manually find the SceneTransitionManager in the scene
        allManager = FindObjectOfType<AllManager>();

        // Check if the manager is found
        if (allManager == null)
        {
            Debug.LogError("allManager not found in the scene.");
        }

        // Check if textBox is assigned
        if (textBox == null)
        {
            Debug.LogError("Text box is not assigned in the inspector!");
        }

        canvas.gameObject.SetActive(false);

        StartCoroutine(EnableCanvasAfterDelay(2f));
    }

    // Update is called once per frame
    void Update()
    {
        if (textBox != null)
        {
            // Update score based on the character count in the text box
            score = textBox.text.Length;

            // Log or use score as needed (e.g., display it in the UI)
            Debug.Log("Character count: " + score);

            // Trigger action if score reaches 80
            if (score >= 80)
            {
                allManager.LoadMainMenu();
            }
        }
    }

    private IEnumerator EnableCanvasAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Re-enable the Canvas
        canvas.gameObject.SetActive(true);
    }
}
