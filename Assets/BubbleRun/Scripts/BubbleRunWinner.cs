using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleRunWinner : MonoBehaviour
{
    private AllManager allManager;

    public int scoreBR;

    void Start()
    {
        allManager = FindObjectOfType<AllManager>();
        scoreBR = 0;
        Debug.Log("Initial score: " + scoreBR);

        if (allManager == null)
        {
            Debug.LogError("AllManager not found in the scene.");
        }
    }

    void Update()
    {
        if (scoreBR >= 5)
        {
            Debug.Log("END GAME");
            if (allManager != null)
            {
                allManager.LoadMainMenu();
            }
            else
            {
                Debug.LogError("Cannot load main menu: AllManager is null.");
            }
        }
    }
}
