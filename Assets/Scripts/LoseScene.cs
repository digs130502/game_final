using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Lose());
    }

    IEnumerator Lose()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(1);
    }
}
