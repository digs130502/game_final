using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int SelectedCharacterId = 0; // Default to the first character
    public SpriteRenderer playerSpriteRenderer; // Reference to the player's sprite renderer
    public Sprite[] characterSprites; // Array of available character sprites

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SelectedCharacterId = PlayerPrefs.GetInt("SelectedCharacter", 0);
            Debug.Log("Loaded SelectedCharacterId: " + SelectedCharacterId);
            UpdatePlayerSprite();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (playerSpriteRenderer == null)
        {
            Debug.LogError("Player sprite renderer is not assigned!");
        }
    }


    public void UnlockCharacter(int characterId)
    {
        PlayerPrefs.SetInt("Character_" + characterId, 1);
        PlayerPrefs.Save();
    }

    public bool IsCharacterUnlocked(int characterId)
    {
        // Default to unlocked for the first character (ID 0)
        return PlayerPrefs.GetInt("Character_" + characterId, characterId == 0 ? 1 : 0) == 1;
    }

    public void SelectCharacter(int characterId)
    {
        if (IsCharacterUnlocked(characterId))
        {
            SelectedCharacterId = characterId;
            PlayerPrefs.SetInt("SelectedCharacter", characterId);
            PlayerPrefs.Save(); // Ensure changes are written
            UpdatePlayerSprite();
            Debug.Log("Selected Character: " + characterId);
        }
        else
        {
            Debug.Log("Character is locked!");
        }
    }


    private void UpdatePlayerSprite()
    {
        if (playerSpriteRenderer == null)
        {
            GameObject player = GameObject.FindWithTag("Player"); // Ensure your player has a "Player" tag
            if (player != null)
            {
                playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
            }
        }

        if (playerSpriteRenderer != null && characterSprites != null && characterSprites.Length > SelectedCharacterId)
        {
            playerSpriteRenderer.sprite = characterSprites[SelectedCharacterId];
        }
        else
        {
            Debug.LogWarning("Player sprite renderer or character sprites array is not properly set!");
        }
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

