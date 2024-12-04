using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Slider masterVolumeSlider;
    public Slider soundEffectsVolumeSlider;
    public Slider musicVolumeSlider;

    private Resolution[] resolutions;

    private void Start()
    {
        // Populate resolution dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        // Convert resolutions to string list
        var options = new System.Collections.Generic.List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Add listeners for sliders
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        soundEffectsVolumeSlider.onValueChanged.AddListener(SetSoundEffectsVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);

        // Set default values for sliders
        masterVolumeSlider.value = AudioListener.volume;
        soundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume", 1f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution selectedResolution = resolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetSoundEffectsVolume(float volume)
    {
        // Implement sound effects volume (e.g., via Audio Mixer)
        PlayerPrefs.SetFloat("SoundEffectsVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        // Implement music volume (e.g., via Audio Mixer)
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void onMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
