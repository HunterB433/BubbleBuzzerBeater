using UnityEngine;
using UnityEngine.UI;
using System;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance; // Singleton instance

    public GameObject popupPanel; // Reference to your popup UI panel
    public Text popupText;        // Reference to the popup message text
    public float displayTime = 2f; // Time to display the popup

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
    }

    public void ShowPopup(string message, Action onComplete)
    {
        popupPanel.SetActive(true);
        popupText.text = message;
        StartCoroutine(DisplayPopup(onComplete));
    }

    private System.Collections.IEnumerator DisplayPopup(Action onComplete)
    {
        yield return new WaitForSeconds(displayTime);
        popupPanel.SetActive(false);
        onComplete?.Invoke();
    }
}
