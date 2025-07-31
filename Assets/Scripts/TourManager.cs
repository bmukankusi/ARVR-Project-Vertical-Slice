using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video; 

public class TourManager : MonoBehaviour
{
    public List<GameObject> roomContainers; 
    public Image fadePanel;
    public float fadeDuration = 1.0f; 
    public int startingRoomIndex = 0;

    public TTSManager ttsManager;

    private int currentRoomIndex = -1;
    private Coroutine fadeCoroutine;

    void Start()
    {
        //Initially all room containers are inactive
        foreach (GameObject room in roomContainers)
        {
            room.SetActive(false);
        }

        // First room should be active at start
        if (roomContainers.Count > 0)
        {
            SwitchToRoom(startingRoomIndex);
        }
    }

    public void SwitchToRoom(int newRoomIndex)
    {
        if (newRoomIndex < 0 || newRoomIndex >= roomContainers.Count || newRoomIndex == currentRoomIndex)
        {
            Debug.LogWarning("Invalid room index or already in this room: " + newRoomIndex);
            return;
        }

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeTransition(newRoomIndex));
    }

    IEnumerator FadeTransition(int newRoomIndex)
    {
        // 1. Fade to black
        float timer = 0f;
        Color startColor = fadePanel.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque black

        while (timer < fadeDuration)
        {
            fadePanel.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        fadePanel.color = endColor; 

        
        if (currentRoomIndex != -1)
        {
            roomContainers[currentRoomIndex].SetActive(false); 
            VideoPlayer oldVideoPlayer = roomContainers[currentRoomIndex].GetComponentInChildren<VideoPlayer>();
            if (oldVideoPlayer != null) oldVideoPlayer.Stop();
            AudioSource oldAudioSource = roomContainers[currentRoomIndex].GetComponentInChildren<AudioSource>();
            if (oldAudioSource != null) oldAudioSource.Stop();
            if (ttsManager != null)
            {
                ttsManager.StopSpeaking();
            }
        }

        currentRoomIndex = newRoomIndex;
        roomContainers[currentRoomIndex].SetActive(true); // Activate new room

        VideoPlayer newVideoPlayer = roomContainers[currentRoomIndex].GetComponentInChildren<VideoPlayer>();
        if (newVideoPlayer != null) newVideoPlayer.Play();
        AudioSource newAudioSource = roomContainers[currentRoomIndex].GetComponentInChildren<AudioSource>();
        if (newAudioSource != null && newAudioSource.clip != null)
        {
            newAudioSource.Play();
        }

       
        string currentNarrationText = "";
        RoomNarrative narrative = roomContainers[currentRoomIndex].GetComponent<RoomNarrative>();
        if (narrative != null)
        {
            currentNarrationText = narrative.roomText;
        }
        else
        {
            Debug.LogWarning("No RoomNarrative component found on " + roomContainers[currentRoomIndex].name + ". TTS will not play for this room.");
        }

        if (ttsManager != null && !string.IsNullOrEmpty(currentNarrationText))
        {
            ttsManager.PlayRoomNarration(currentNarrationText);
        }

        timer = 0f;
        startColor = fadePanel.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Fully transparent

        while (timer < fadeDuration)
        {
            fadePanel.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        fadePanel.color = endColor; 

        fadeCoroutine = null; // Reset coroutine reference
    }
}