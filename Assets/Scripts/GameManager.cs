using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int selectedCharacterId = 0; // Default selected character
    private bool[] unlockedCharacters = new bool[3]; // Example unlock data
    public GameObject[] characterPrefabs = new GameObject[3]; // Array of character prefabs

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Ensure the first character is unlocked by default
            unlockedCharacters[0] = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Check if a character is unlocked
    public bool IsCharacterUnlocked(int characterId)
    {
        if (characterId >= 0 && characterId < unlockedCharacters.Length)
        {
            return unlockedCharacters[characterId];
        }
        Debug.LogError("Invalid character ID: " + characterId);
        return false;
    }

    // Unlock a character
    public void UnlockCharacter(int characterId)
    {
        if (characterId >= 0 && characterId < unlockedCharacters.Length)
        {
            unlockedCharacters[characterId] = true;
        }
        else
        {
            Debug.LogError("Invalid character ID: " + characterId);
        }
    }

    // Select a character for the game session
    public void SelectCharacter(int characterId)
    {
        if (characterId >= 0 && characterId < characterPrefabs.Length)
        {
            selectedCharacterId = characterId;
            Debug.Log("Character " + characterId + " selected.");
        }
        else
        {
            Debug.LogError("Invalid character ID: " + characterId);
        }
    }

    // Get the currently selected character prefab
    public GameObject GetSelectedCharacterPrefab()
    {
        if (selectedCharacterId >= 0 && selectedCharacterId < characterPrefabs.Length)
        {
            return characterPrefabs[selectedCharacterId];
        }
        Debug.LogError("Selected character ID is invalid: " + selectedCharacterId);
        return null;
    }

    // **New Method**: Get the selected character ID
    public int GetSelectedCharacterId()
    {
        return selectedCharacterId;
    }

    // Level progression
    public void UnlockLevel(int level)
    {
        PlayerPrefs.SetInt("Level_" + level, 1);
        PlayerPrefs.Save();
    }

    public bool IsLevelUnlocked(int level)
    {
        // Default: Level 1 (index 2) is unlocked at start
        return PlayerPrefs.GetInt("Level_" + level, level == 2 ? 1 : 0) == 1;
    }

    public void CompleteLevel(int level)
    {
        UnlockLevel(level + 1); // Unlock the next level
        Debug.Log("Level " + level + " completed. Level " + (level + 1) + " unlocked!");
    }
}
