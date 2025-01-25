using UnityEngine;
using TMPro;

public class AutoKeyboardInput : MonoBehaviour
{
    public TMP_Text targetText; // Assign the TextMeshPro UI element in the Inspector
    public int maxCharacters = 100; // Optional: Maximum characters allowed

    // Update is called once per frame
    void Update()
    {
        // Capture keyboard input
        foreach (char c in Input.inputString)
        {
            // Handle backspace to remove the last character
            if (c == '\b')
            {
                if (targetText.text.Length > 0)
                {
                    targetText.text = targetText.text.Substring(0, targetText.text.Length - 1);
                }
            }
            // Handle Enter or Return to add a new line
            else if (c == '\n' || c == '\r')
            {
                targetText.text += "\n";
            }
            else
            {
                // Add the typed character if it doesn't exceed maxCharacters
                if (targetText.text.Length < maxCharacters)
                {
                    targetText.text += c;
                }
            }
        }
    }
}
