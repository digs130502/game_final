using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public Image characterImage;
    public Button selectButton;
    public int characterId; // Assign a unique ID for each character

    void Start()
    {
        UpdateCharacterUI();
    }

    void UpdateCharacterUI()
    {
        if (GameManager.Instance.IsCharacterUnlocked(characterId))
        {
            characterImage.color = Color.white; // Show normal sprite
            selectButton.interactable = true;
        }
        else
        {
            characterImage.color = Color.black; // Show locked appearance
            selectButton.interactable = false;
        }
    }

    public void OnCharacterClicked()
    {
        GameManager.Instance.SelectCharacter(characterId);
        Debug.Log("Character " + characterId + " clicked.");
    }
}
