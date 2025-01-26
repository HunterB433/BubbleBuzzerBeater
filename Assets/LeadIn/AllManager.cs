using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

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
    public List<GameObject> popupPrefabs; // List of popup prefabs
    public Vector3 popupLocation;        // Location where the popup will appear
    public float popupDisplayTime = 2f;

    // Game Controller
    public int score = 0;
    public int lives = 3;

    public bool winned = true;

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
            if (!winned)
            {
                lives--;
                if (lives == 0)
                {
                    Debug.Log("Outta Lives");
                }
            }
            LoadNextMiniGameWithDelay(1f);
            UpdateHearts();
        }

        else if (scene.name == "BubbleWrap")
        {
            List<int> popupSequence = new List<int> { 0, 1 };
            ShowPopupsSequentially(popupSequence);
        }

        else if (scene.name == "TypingGame")
        {
            List<int> popupSequence = new List<int> { 2, 3 };
            ShowPopupsSequentially(popupSequence);
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
            
            
            LoadNextMiniGame();
           
        }
        else
        {
            lives--;
            UpdateHearts();
            
            if (lives > 0)
            {
                LoadNextMiniGame();
            }
            else
            {
                    
            }
            
        }
    }

    // Popup Management
    public void ShowPopupsSequentially(List<int> popupIndices)
    {
        StartCoroutine(DisplayPopupsSequentially(popupIndices));
    }

    private IEnumerator DisplayPopupsSequentially(List<int> popupIndices)
    {
        foreach (int popupIndex in popupIndices)
        {
            // Show the current popup
            ShowPopup(popupIndex);

            // Wait for the duration of the popup display before showing the next one
            yield return new WaitForSeconds(popupDisplayTime);
        }
    }

    public void ShowPopup(int popupIndex)
    {
        if (popupIndex < 0 || popupIndex >= popupPrefabs.Count)
        {
            Debug.LogWarning("Invalid popup index");
            return;
        }

        // Instantiate the selected popup prefab
        GameObject popup = Instantiate(popupPrefabs[popupIndex], popupLocation, Quaternion.identity);

        // Set the popup's initial size
        popup.transform.localScale = Vector3.one;

        // Start the popup animation coroutine
        StartCoroutine(AnimatePopup(popup));
    }

    private IEnumerator AnimatePopup(GameObject popup)
    {
        float elapsedTime = 0f;
        Vector3 startScale = Vector3.one;               // Initial size
        Vector3 endScale = new Vector3(50f, 50f, 50f);  // Final size

        // Gradually scale the popup
        while (elapsedTime < popupDisplayTime)
        {
            float progress = elapsedTime / popupDisplayTime;
            popup.transform.localScale = Vector3.Lerp(startScale, endScale, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final size is set
        popup.transform.localScale = endScale;

        // Destroy the popup after the animation
        Destroy(popup);
    }

}
