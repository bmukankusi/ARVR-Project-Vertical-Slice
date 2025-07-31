using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Meta.WitAi.TTS.Integrations; 
using Meta.WitAi.TTS.Utilities;

/// <summary>
/// This class manages Text-to-Speech (TTS) functionality using the Meta Voice SDK.
/// </summary>

public class TTSManager : MonoBehaviour
{
    public AudioSource ttsAudioSource;
    public TextMeshProUGUI ttsToggleButtonText;
    public TTSSpeaker ttsSpeaker; 

    private bool isTTSEnabled = true;
    private bool isSpeaking = false;
    private Coroutine currentSpeechMonitorCoroutine; 

    void Awake()
    {
       
        if (ttsAudioSource == null)
        {
            ttsAudioSource = GetComponent<AudioSource>();
            if (ttsAudioSource == null)
            {
                ttsAudioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        ttsAudioSource.spatialBlend = 0f; // 2D for narration
        ttsAudioSource.playOnAwake = false;
        ttsAudioSource.loop = false;

        // Make sure the TTSSpeaker is assigned
        if (ttsSpeaker == null)
        {
            ttsSpeaker = GetComponent<TTSSpeaker>();
            if (ttsSpeaker == null)
            {
                Debug.LogError("TTSSpeaker component not found on TTS_Manager. Please add one (Meta > Voice SDK > TTS > Add TTSSpeaker) or assign it.", this);
            }
        }

        if (ttsSpeaker != null)
        {

            
        }
    }

    void Start()
    {
        UpdateToggleButtonText();
    }

    public void ToggleTTS()
    {
        isTTSEnabled = !isTTSEnabled;
        if (!isTTSEnabled)
        {
            StopSpeaking();
        }
        UpdateToggleButtonText();
        Debug.Log("TTS Enabled: " + isTTSEnabled);
    }

    public void PlayRoomNarration(string text)
    {
        if (!isTTSEnabled)
        {
            Debug.Log("TTS is disabled. Not playing narration.");
            return;
        }

        if (string.IsNullOrEmpty(text))
        {
            Debug.LogWarning("Narration text is empty. Not playing TTS.");
            return;
        }

        // Always stop current speech before starting new one
        if (isSpeaking)
        {
            StopSpeaking();
        }

        if (ttsSpeaker == null)
        {
            Debug.LogError("TTSSpeaker is not assigned. Cannot play narration.", this);
            return;
        }

        // Use the TTSSpeaker's Speak method
        ttsSpeaker.Speak(text);

        if (currentSpeechMonitorCoroutine != null)
        {
            StopCoroutine(currentSpeechMonitorCoroutine);
        }
        currentSpeechMonitorCoroutine = StartCoroutine(MonitorAudioSourcePlayback());

        isSpeaking = true;
        Debug.Log("Meta Voice SDK TTS: Requesting speech for: " + text);
    }

    public void StopSpeaking()
    {
      
        if (ttsSpeaker != null)
        {
            ttsSpeaker.Stop();
        }

        if (ttsAudioSource != null && ttsAudioSource.isPlaying)
        {
            ttsAudioSource.Stop();
        }

        // Stop the monitoring coroutine
        if (currentSpeechMonitorCoroutine != null)
        {
            StopCoroutine(currentSpeechMonitorCoroutine);
            currentSpeechMonitorCoroutine = null;
        }

        isSpeaking = false;
        Debug.Log("TTS stopped.");
    }

    private void UpdateToggleButtonText()
    {
        if (ttsToggleButtonText != null)
        {
            ttsToggleButtonText.text = isTTSEnabled ? "TTS: ON" : "TTS: OFF";
        }
    }

    private IEnumerator MonitorAudioSourcePlayback()
    {
        while (!ttsAudioSource.isPlaying && ttsSpeaker.IsSpeaking) // IsSpeaking on TTSSpeaker is a good indicator
        {
            yield return null;
        }

        while (ttsAudioSource.isPlaying)
        {
            yield return null;
        }

        while (ttsSpeaker.IsSpeaking) 
        {
            yield return null;
        }


        isSpeaking = false;
        Debug.Log("Meta Voice SDK TTS: Audio playback finished.");
        currentSpeechMonitorCoroutine = null; // Clearing the coroutine reference
    }
}