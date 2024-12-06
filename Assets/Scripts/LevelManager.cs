using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static GameObject PlayerInstance { get; private set; } // Static reference to the player
    public Transform spawnPoint; // Where the character should spawn
    public CameraFollow cameraFollow; // Reference to the CameraFollow script
    public Image healthBar; // Reference to the health bar UI element

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is not available!");
            return;
        }

        // Get the selected character prefab from the GameManager
        GameObject selectedCharacterPrefab = GameManager.Instance.GetSelectedCharacterPrefab();

        if (selectedCharacterPrefab == null)
        {
            Debug.LogError("Selected character prefab is null. Check GameManager characterPrefabs array.");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not assigned. Assign it in the LevelManager.");
            return;
        }

        // Instantiate the selected character at the spawn point
        PlayerInstance = Instantiate(selectedCharacterPrefab, spawnPoint.position, spawnPoint.rotation);

        // Ensure the instantiated player has the "Player" tag
        PlayerInstance.tag = "Player";

        // Set the camera's target dynamically
        if (cameraFollow != null)
        {
            cameraFollow.SetTarget(PlayerInstance.transform);
        }
        else
        {
            Debug.LogError("CameraFollow reference is missing! Ensure the LevelManager has a reference to it.");
        }

        // Assign the health bar to the player's Health script
        Health playerHealth = PlayerInstance.GetComponent<Health>();
        if (playerHealth != null && healthBar != null)
        {
            playerHealth.AssignHealthBar(healthBar);
        }
        else
        {
            Debug.LogError("Health script or health bar reference is missing!");
        }
    }
}
