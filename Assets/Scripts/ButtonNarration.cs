using UnityEngine;
using TMPro;

public class ButtonNarration : MonoBehaviour
{
    [TextArea(3, 10)]
    public string narrationText; 

    public TTSManager ttsManager; 


    void Start()
    {
        if (ttsManager == null)
        {
            // Try to find the TTSManager if not assigned
            ttsManager = FindObjectOfType<TTSManager>();
            if (ttsManager == null)
            {
                Debug.LogError("ButtonNarration: TTSManager not found in scene. Please assign it.", this);
            }
        }


    }

    public void PlayButtonNarration()
    {
        if (ttsManager != null && !string.IsNullOrEmpty(narrationText))
        {
            ttsManager.PlayRoomNarration(narrationText); 
            Debug.Log("Button clicked: Playing TTS for: " + narrationText);
        }
        else if (ttsManager == null)
        {
            Debug.LogError("TTSManager not assigned to ButtonNarration script.", this);
        }
        else if (string.IsNullOrEmpty(narrationText))
        {
            Debug.LogWarning("Narration text is empty for this button.", this);
        }
    }
}