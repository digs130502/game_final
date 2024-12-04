using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void OnLevelOne()
    {
        SceneManager.LoadScene(2);
    }
    public void OnLevelTwo()
    {
        SceneManager.LoadScene(3);
    }
    public void OnLevelThree()
    {
        SceneManager.LoadScene(4);
    }
    public void OnLevelFour()
    {
        SceneManager.LoadScene(5);
    }
    public void OnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void onTest()
    {
        SceneManager.LoadScene(12);
    }
}
