using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubWinManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int score = 0;
    private AllManager allManager;

    void Start()
    {
        //score = 0;
        //Manually find the SceneTransitionManager in the scene
        allManager = FindObjectOfType<AllManager>();

        // Check if the manager is found
        if (allManager == null)
        {
            Debug.LogError("allManager not found in the scene.");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (score == 16)
        {
            allManager.LoadMainMenu();
        }
    }

    public void IncrementScore()
    {
        Debug.Log("Called Increment on" + score);
        score++;
        Debug.Log("Incremented" + score);
    }
}
