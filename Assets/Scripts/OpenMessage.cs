using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMessage : MonoBehaviour
{
    public GameObject textPanel; 

    private bool isPanelOpen = false;

    public void TogglePanel()
    {
        isPanelOpen = !isPanelOpen;
        if (textPanel != null)
        {
            textPanel.SetActive(isPanelOpen);
        }
    }
}
