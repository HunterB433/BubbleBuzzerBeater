using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;

    public PopupManager popupManager;
    public SceneTransitionManager sceneTransitionManager;
    public int score = 0;
    public int lives = 3;

    private HeartDisplay heartDisplay;

    void Awake()
    {
        // Check if an instance already exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Make this GameObject persist across scenes
    }

    private void Start()
    {
        // Find the HeartDisplay in the scene
        heartDisplay = FindObjectOfType<HeartDisplay>();

        // Initialize the heart display
        //heartDisplay.UpdateHearts();
    }

    public void EndGame(bool didWin)
    {
        if (didWin)
        {
            score++;
            popupManager.ShowPopup("You Win!", () =>
            {
                sceneTransitionManager.LoadNextMiniGame();
            });
        }
        else
        {
            lives--;
            //heartDisplay.UpdateHearts(); // Update hearts when lives change
            popupManager.ShowPopup("You Lose!", () =>
            {
                if (lives > 0)
                {
                    sceneTransitionManager.LoadNextMiniGame();
                }
                else
                {
                    popupManager.ShowPopup("Game Over!", () =>
                    {
                        sceneTransitionManager.LoadMainMenu();
                    });
                }
            });
        }
    }
}
