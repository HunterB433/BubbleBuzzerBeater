using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePop : MonoBehaviour
{
    
    private int score = 0;
    void OnTriggerEnter(Collider bubble)
    {
        // check if player is the collider
        if (bubble.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            score++;
        }
    }

    public int GetscoreBR()
    {
        return score;
    }



}
