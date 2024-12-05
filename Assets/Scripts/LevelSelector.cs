using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons; // Array of level buttons (assign in the Unity Inspector)
    public Color lockedColor = Color.gray; // Color for locked levels
    public Color unlockedColor = Color.white; // Color for unlocked levels

    void Start()
    {
        UpdateLevelButtons();
    }

    public void UpdateLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 2; // Level index starts at 2 (Scene 2 is Level 1)
            bool isUnlocked = GameManager.Instance.IsLevelUnlocked(levelIndex);

            Button button = levelButtons[i];
            Image buttonImage = button.GetComponent<Image>();
            Text buttonText = button.GetComponentInChildren<Text>();

            if (isUnlocked)
            {
                button.interactable = true;
                if (buttonImage != null) buttonImage.color = unlockedColor;
                if (buttonText != null) buttonText.color = Color.white; // Optional: set text color
            }
            else
            {
                button.interactable = false; // Disable interaction
                if (buttonImage != null) buttonImage.color = lockedColor;
                if (buttonText != null) buttonText.color = Color.black; // Optional: set text color
            }
        }
    }

    public void OnLevel(int level)
    {
        if (GameManager.Instance.IsLevelUnlocked(level))
        {
            SceneManager.LoadScene(level);
        }
        else
        {
            Debug.Log("Level is locked!");
        }
    }

    public void OnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
