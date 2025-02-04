using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// This should allow you to change the audio volume once it's implemented.
// Not entirely sure how we plan to have audio work yet, but this can be adjusted if needed once it's made.
public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider slider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private GameObject audioSettings;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject buttons;

    private GameObject lastMenu;

    public void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
            LoadVolume();
        else
            SetVolume();
    }
    public void SetVolume()
    {
        if (mixer != null)
        {
            float volume = slider.value;
            mixer.SetFloat("volume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("volume", volume);
        }
    }

    public void SetMusicVolume()
    {
        if (mixer != null)
        {
            float volume = musicSlider.value;
            mixer.SetFloat("music", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("musicVolume", volume);
        }
    }

    public void SetSFXVolume()
    {
        if (mixer != null)
        {
            float volume = sfxSlider.value;
            mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("sfxVolume", volume);
        }
    }

    private void LoadVolume()
    {
        slider.value = PlayerPrefs.GetFloat("volume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    // What menu should we return too?
    // Settings will probably be present in the pause menu as well.
    // Current idea is that I create a gameobject that gets set to remember the menu used to get here so I can return easier.
    public void ReturnMenu(GameObject menu)
    {
        gameObject.SetActive(false);
        menu.SetActive(true);
    }

    // Return to main settings
    // Create a tracker to return easier.
    public void ReturnToSettings()
    {
        lastMenu.SetActive(false);
        buttons.SetActive(true);
    }

    public void GoToAudio()
    {
        lastMenu = audioSettings;
        buttons.SetActive(false);
        audioSettings.SetActive(true);
    }

    public void GoToCredits()
    {
        lastMenu = credits;
        buttons.SetActive(false);
        credits.SetActive(true);
    }
}
