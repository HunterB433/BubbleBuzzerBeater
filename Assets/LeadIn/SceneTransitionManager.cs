using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance; // Singleton instance

    public string[] miniGameScenes; // List of mini-game scene names
    public HeartDisplay heartDisplay;
    private int lastSceneIndex = -1;

    private void Awake()
    {
        // Singleton pattern to prevent duplicates
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        //
    }

    // Start the coroutine to load the next mini-game after a delay
    public void LoadNextMiniGameWithDelay(float delay)
    {
        StartCoroutine(LoadNextMiniGameAfterDelay(delay));
    }

    public IEnumerator LoadNextMiniGameAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Now load the next mini-game
        LoadNextMiniGame();
    }

    public void LoadNextMiniGame()
    {
        int nextSceneIndex = GetRandomSceneIndex();
        SceneManager.LoadScene(miniGameScenes[nextSceneIndex]);
        lastSceneIndex = nextSceneIndex;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("LeadIn"); // Replace with your main menu scene name
    }

    private int GetRandomSceneIndex()
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, miniGameScenes.Length);
        } while (randomIndex == lastSceneIndex);
        return randomIndex;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene {scene.name} has been loaded.");
        // Perform any initialization needed for the new scene

        if (scene.name == "LeadIn")
        {
            LoadNextMiniGameWithDelay(1f);
            heartDisplay.UpdateHearts();
        }
    }
}
