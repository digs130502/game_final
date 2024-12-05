using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider masterVolumeSlider;
    public Slider soundEffectsVolumeSlider;
    public Slider musicVolumeSlider;
    public Dropdown resolutionDropdown; // Dropdown for selecting resolutions

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    private Resolution[] resolutions; // Array to store available screen resolutions

    private void Start()
    {
        // Load available resolutions and populate the dropdown
        PopulateResolutionDropdown();

        // Load saved volume levels
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        soundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume", 1f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

        // Apply loaded volume levels
        SetMasterVolume(masterVolumeSlider.value);
        SetSoundEffectsVolume(soundEffectsVolumeSlider.value);
        SetMusicVolume(musicVolumeSlider.value);

        // Add listeners for sliders
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        soundEffectsVolumeSlider.onValueChanged.AddListener(SetSoundEffectsVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);

        // Add listener for resolution dropdown
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    private void PopulateResolutionDropdown()
    {
        // Get all supported resolutions
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        var options = new System.Collections.Generic.List<string>();

        int currentResolutionIndex = 0;

        // Populate the dropdown with resolution options
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(option);

            // Check if the resolution matches the current screen resolution
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        // Set the screen resolution based on the selected dropdown option
        Resolution selectedResolution = resolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        Debug.Log($"Resolution set to: {selectedResolution.width} x {selectedResolution.height}");
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetSoundEffectsVolume(float volume)
    {
        float clampedVolume = Mathf.Clamp(volume, 0.0001f, 1.0f); // Ensure volume is never zero
        float dbVolume = Mathf.Log10(clampedVolume) * 20;
        dbVolume = Mathf.Clamp(dbVolume, -80f, 0f); // Clamp decibel range for the mixer
        audioMixer.SetFloat("SoundEffectsVolume", dbVolume);
        Debug.Log($"Set Sound Effects Volume to {dbVolume} dB (Clamped)");
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); // Convert linear scale to decibel
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void OnMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
