using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int SelectedCharacterId = 0; // Default to the first character

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UnlockCharacter(int characterId)
    {
        PlayerPrefs.SetInt("Character_" + characterId, 1);
        PlayerPrefs.Save();
    }

    public bool IsCharacterUnlocked(int characterId)
    {
        return PlayerPrefs.GetInt("Character_" + characterId, characterId == 0 ? 1 : 0) == 1;
    }

    public void SelectCharacter(int characterId)
    {
        if (IsCharacterUnlocked(characterId))
        {
            SelectedCharacterId = characterId;
            Debug.Log("Selected Character: " + characterId);
        }
        else
        {
            Debug.Log("Character is locked!");
        }
    }
}
