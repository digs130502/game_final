using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win2 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CompleteLevel();
            SceneManager.LoadScene(9);
            GameManager.Instance.UnlockCharacter(1); // unlocks bruce character
        }
    }

    public void CompleteLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameManager.Instance.CompleteLevel(currentSceneIndex);
    }
}
