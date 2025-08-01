using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    // Accessible from the Unity Editor

    public TMP_Dropdown qualityOptions;
    public Slider bgMusicSlider, buttonSoundSlider;
    public AudioMixer mainMixer;
    public TTSManager ttsManager;
    public Toggle ttsToggle;

    //Methods to manage settings
    //---QualitySettings---
    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(qualityOptions.value);
        PlayerPrefs.SetInt("QualityLevel", qualityOptions.value);
    }

    //---Background Music & Button sound Volume---
    public void ChangeMusicVolume()
    {
        mainMixer.SetFloat("VolumeBgMusic", bgMusicSlider.value);
        PlayerPrefs.SetFloat("VolumeBgMusic", bgMusicSlider.value);
    }

    public void ChangeButtonSoundVolume()
    {
        mainMixer.SetFloat("VolumeButtonSound", buttonSoundSlider.value);
        PlayerPrefs.SetFloat("VolumeButtonSound", buttonSoundSlider.value);
    }

    //---Text-to-Speech Disabling---
    public void ToggleTTS()
    {
        if (ttsToggle.isOn)
        {
            ttsManager.ToggleTTS(); 
            PlayerPrefs.SetInt("TTS", 1);
        }
        else
        {
            ttsManager.ToggleTTS();
            PlayerPrefs.SetInt("TTS", 0);
        }
    }


}
