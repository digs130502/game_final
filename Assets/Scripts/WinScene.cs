using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    [SerializeField] AudioClip win;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(win, Vector3.zero, 1.0f);
        StartCoroutine(Win());
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(7);

        SceneManager.LoadScene(1);
    }


}
