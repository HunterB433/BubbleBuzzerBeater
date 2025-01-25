using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;

    public PopupManager popupManager;
    public SceneTransitionManager sceneTransitionManager;
    public int score = 0;
    public int lives = 3;

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

    // Add your other GameController logic here

    private void Start()
    {
        // Initialize game state
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
