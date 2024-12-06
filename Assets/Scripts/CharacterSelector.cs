using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Array of character prefabs
    public Button[] selectButtons; // Buttons for selecting characters
    private int selectedCharacterId = 0; // Default to the first character

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is not available!");
            return;
        }

        // Ensure the first character is unlocked
        GameManager.Instance.UnlockCharacter(0);

        InitializeCharacterUI();
    }

    // Initializes UI for character selection
    private void InitializeCharacterUI()
    {
        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            int characterId = i; // Capture index for closure

            // Check if the character is unlocked
            bool isUnlocked = GameManager.Instance.IsCharacterUnlocked(characterId);

            // Enable or disable the button based on unlock status
            selectButtons[characterId].interactable = isUnlocked;

            // Assign a listener to the button
            selectButtons[characterId].onClick.AddListener(() => OnCharacterSelected(characterId));
        }
    }

    // Called when a character is selected
    private void OnCharacterSelected(int characterId)
    {
        if (characterId >= 0 && characterId < characterPrefabs.Length)
        {
            selectedCharacterId = characterId;

            // Notify the GameManager of the selected character
            GameManager.Instance.SelectCharacter(characterId);

            Debug.Log("Character " + characterId + " selected.");
        }
        else
        {
            Debug.LogError("Invalid character ID selected: " + characterId);
        }
    }

    // Provides the selected character prefab for instantiation
    public GameObject GetSelectedCharacterPrefab()
    {
        return characterPrefabs[selectedCharacterId];
    }
}
