using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;

public class AllManager : MonoBehaviour
{
    public static AllManager Instance; // Singleton instance

    // Scene Management
    public string[] miniGameScenes;
    private int lastSceneIndex = -1;

    // Heart Display
    public GameObject heartPrefab;
    public Vector3 spawnPoint = Vector3.zero;
    public float xOffset = 2f;

    // Popup Management
    public GameObject popupPanel;
    public Text popupText;
    public float popupDisplayTime = 2f;

    // Game Controller
    public int score = 0;
    public int lives = 3;

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
        // Unsubscribe from events to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene {scene.name} has been loaded.");
        if (scene.name == "LeadIn")
        {
            LoadNextMiniGameWithDelay(1f);
            UpdateHearts();
        }
    }

    // Scene Management
    public void LoadNextMiniGameWithDelay(float delay)
    {
        StartCoroutine(LoadNextMiniGameAfterDelay(delay));
    }

    private IEnumerator LoadNextMiniGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
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
            randomIndex = UnityEngine.Random.Range(0, miniGameScenes.Length);
        } while (randomIndex == lastSceneIndex);
        return randomIndex;
    }

    // Heart Display
    public void UpdateHearts()
    {
        Debug.Log("Updating hearts...");
        // Clear existing hearts
        foreach (GameObject heart in GameObject.FindGameObjectsWithTag("Heart"))
        {
            Destroy(heart);
        }

        // Instantiate hearts based on lives
        for (int i = 0; i < lives; i++)
        {
            Vector3 position = spawnPoint + new Vector3(i * xOffset, 0, 0);
            GameObject heart = Instantiate(heartPrefab, position, Quaternion.identity);
            heart.tag = "Heart";
        }
    }

    // Game Logic
    public void EndGame(bool didWin)
    {
        if (didWin)
        {
            score++;
            ShowPopup("You Win!", () =>
            {
                LoadNextMiniGame();
            });
        }
        else
        {
            lives--;
            UpdateHearts();
            ShowPopup("You Lose!", () =>
            {
                if (lives > 0)
                {
                    LoadNextMiniGame();
                }
                else
                {
                    ShowPopup("Game Over!", () =>
                    {
                        //LoadMainMenu();
                    });
                }
            });
        }
    }

    // Popup Management
    public void ShowPopup(string message, Action onComplete)
    {
        popupPanel.SetActive(true);
        popupText.text = message;
        StartCoroutine(DisplayPopup(onComplete));
    }

    private IEnumerator DisplayPopup(Action onComplete)
    {
        yield return new WaitForSeconds(popupDisplayTime);
        popupPanel.SetActive(false);
        onComplete?.Invoke();
    }
}
