using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer mainMixer;

    [Header("UI Elements")]
    public Slider buttonSoundSlider;
    public Toggle ttsToggle;
    public TMP_Dropdown qualityDropdown;

    [Header("SFX Preview")]
    public AudioSource buttonClickSource;
    public AudioClip buttonClickSound;

    // PlayerPrefs keys
    private const string PREFS_KEY = "prayerpreferences_";
    private const string BG_MUSIC_KEY = PREFS_KEY + "bg_music";
    private const string BUTTON_SOUND_KEY = PREFS_KEY + "button_sound";
    private const string TTS_KEY = PREFS_KEY + "tts_enabled";
    private const string QUALITY_KEY = PREFS_KEY + "quality_level";

    // AudioMixer exposed parameters
    private const string BUTTON_SOUND_PARAM = "ButtonSound";

    // Volume range (dB)
    private const float VOLUME_MIN = -80f;
    private const float VOLUME_MAX = 0f;
    private const float VOLUME_DEFAULT = 0f;

    private readonly List<string> qualityOptions = new List<string> { "Low", "Medium", "High" };

    void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        float bgMusicVol = PlayerPrefs.GetFloat(BG_MUSIC_KEY, VOLUME_DEFAULT);
        float buttonSoundVol = PlayerPrefs.GetFloat(BUTTON_SOUND_KEY, VOLUME_DEFAULT);
        int ttsEnabled = PlayerPrefs.GetInt(TTS_KEY, 1);
        int quality = PlayerPrefs.GetInt(QUALITY_KEY, 2); // Default to High

        SetMixerVolume(BUTTON_SOUND_PARAM, buttonSoundVol);

        if (buttonSoundSlider != null) buttonSoundSlider.value = buttonSoundVol;
        if (ttsToggle != null) ttsToggle.isOn = ttsEnabled == 1;

        if (qualityDropdown != null)
        {
            qualityDropdown.ClearOptions();
            qualityDropdown.AddOptions(qualityOptions);
            qualityDropdown.value = Mathf.Clamp(quality, 0, qualityOptions.Count - 1);
        }

        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }

    public void SetBGMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(BG_MUSIC_KEY, value);
    }

    public void SetButtonSoundVolume(float value)
    {
        SetMixerVolume(BUTTON_SOUND_PARAM, value);
        PlayerPrefs.SetFloat(BUTTON_SOUND_KEY, value);
    }

    private void SetMixerVolume(string parameter, float value)
    {
        mainMixer.SetFloat(parameter, value);
    }

    public void SetTTS(bool enabled)
    {
        PlayerPrefs.SetInt(TTS_KEY, enabled ? 1 : 0);
        Debug.Log("TTS is now " + (enabled ? "ENABLED" : "DISABLED"));
    }

    public bool IsTTSActive() => PlayerPrefs.GetInt(TTS_KEY, 1) == 1;

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt(QUALITY_KEY, index);
    }

    public void PlayClickSound()
    {
        if (buttonClickSource != null && buttonClickSound != null)
            buttonClickSource.PlayOneShot(buttonClickSound);
    }
}
