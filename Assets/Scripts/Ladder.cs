using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Rigidbody2D playerRigidbody; // Reference to the player's Rigidbody2D
    private bool isPlayerInArea = false;

    private void Start()
    {
        if (LevelManager.PlayerInstance != null)
        {
            playerRigidbody = LevelManager.PlayerInstance.GetComponent<Rigidbody2D>();
            if (playerRigidbody == null)
            {
                Debug.LogError("Rigidbody2D not found on Player in LevelManager.");
            }
        }
        else
        {
            Debug.LogError("PlayerInstance is null in LevelManager. Ensure the player is instantiated correctly.");
        }
    }

    void Update()
    {
        if (isPlayerInArea && playerRigidbody != null)
        {
            playerRigidbody.gravityScale = 0; // Set gravity scale to 0 while on the ladder

            if (Input.GetKey(KeyCode.W))
            {
                ClimbLadder(1); // Climb up
            }
            else if (Input.GetKey(KeyCode.S))
            {
                ClimbLadder(-1); // Climb down
            }
            else
            {
                // Stop vertical movement
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            }
        }
    }

    void ClimbLadder(int direction)
    {
        // Move the player vertically based on the direction (1 for up, -1 for down)
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 5f * direction);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the ladder.");
            isPlayerInArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited ladder.");
            isPlayerInArea = false;

            if (playerRigidbody != null)
            {
                playerRigidbody.gravityScale = 5; // Reset gravity scale when exiting the ladder
            }
        }
    }
}
