using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartButton : MonoBehaviour
{
          public void LoadNextScene()
    {
        // Assuming the next scene is in the build settings with index 1 or the scene name
        SceneManager.LoadScene("BubbleWrap");  // Replace with the actual name of your next scene
    }
}
