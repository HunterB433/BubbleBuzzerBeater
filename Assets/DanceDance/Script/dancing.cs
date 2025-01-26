using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dancing : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            player.GetComponent<Animator>().Play("wiggle");
        }
        else if (Input.GetKeyDown("e"))
        {
            player.GetComponent<Animator>().Play("dancingqueen");
        }
        else if (Input.GetKeyDown("q"))
        {
            player.GetComponent<Animator>().Play("kicking");
        }
    }
}
