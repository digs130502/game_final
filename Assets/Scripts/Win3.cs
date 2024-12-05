using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win3 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CompleteLevel();
            SceneManager.LoadScene(10);
            GameManager.Instance.UnlockCharacter(2); // unlocks bruce character
        }
    }

    public void CompleteLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameManager.Instance.CompleteLevel(currentSceneIndex);
    }
}
