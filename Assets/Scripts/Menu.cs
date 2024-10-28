using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnControlButton()
    {
        SceneManager.LoadScene(8);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
