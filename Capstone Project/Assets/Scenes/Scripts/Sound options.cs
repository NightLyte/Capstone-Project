using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider musicSlider; // Assign the music volume slider in the inspector
    public Slider soundSlider; // Assign the sound effects volume slider in the inspector
    public Button saveButton; // Assign the save button in the inspector
    public Button backButton; // Assign the back button in the inspector

    private AudioSource musicSource; // Reference to the music AudioSource
    private AudioSource soundSource; // Reference to the sound effects AudioSource

    private float previousMusicVolume;
    private float previousSoundVolume;

    void Start()
    {
        // Initialize AudioSources (you can assign these in the inspector or find them dynamically)
        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        soundSource = GameObject.Find("SoundSource").GetComponent<AudioSource>();

        // Load saved values (if any)
        LoadSettings();

        // Save the initial values as the previous values
        previousMusicVolume = musicSlider.value;
        previousSoundVolume = soundSlider.value;

        // Add listeners to buttons
        saveButton.onClick.AddListener(ApplySettings);
        backButton.onClick.AddListener(RevertSettings);
    }

    void ApplySettings()
    {
        // Set the AudioSource volumes based on slider positions
        if (musicSource != null)
        {
            musicSource.volume = musicSlider.value;
        }

        if (soundSource != null)
        {
            soundSource.volume = soundSlider.value;
        }

        // Save the settings
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
        PlayerPrefs.Save();

        // Update the previous values
        previousMusicVolume = musicSlider.value;
        previousSoundVolume = soundSlider.value;

        Debug.Log("Settings saved: Music = " + musicSlider.value + ", Sound = " + soundSlider.value);
    }

    void RevertSettings()
    {
        // Revert the sliders to their previous positions
        musicSlider.value = previousMusicVolume;
        soundSlider.value = previousSoundVolume;

        // Optionally revert the AudioSource volumes immediately
        if (musicSource != null)
        {
            musicSource.volume = previousMusicVolume;
        }

        if (soundSource != null)
        {
            soundSource.volume = previousSoundVolume;
        }

        Debug.Log("Settings reverted: Music = " + previousMusicVolume + ", Sound = " + previousSoundVolume);
    }

    void LoadSettings()
    {
        // Load saved values or use defaults
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f); // Default to 0.5
        float savedSoundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.5f); // Default to 0.5

        musicSlider.value = savedMusicVolume;
        soundSlider.value = savedSoundVolume;

        if (musicSource != null)
        {
            musicSource.volume = savedMusicVolume;
        }

        if (soundSource != null)
        {
            soundSource.volume = savedSoundVolume;
        }
    }
}