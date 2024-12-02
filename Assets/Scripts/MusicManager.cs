using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Determine which music to play based on scene index
        if (scene.buildIndex == 0 || scene.buildIndex == 1 || scene.buildIndex == 8)
        {
            PlayMusic(menuMusic);
        }
        else if (scene.buildIndex == 2 || scene.buildIndex == 3 || scene.buildIndex == 4 || scene.buildIndex == 5)
        {
            PlayMusic(gameplayMusic);
        }
        else
        {
            Debug.LogWarning("No specific music set for this scene!");
            audioSource.Stop(); // Optional: Stop music for unhandled scenes
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return; // Avoid restarting the same track

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
