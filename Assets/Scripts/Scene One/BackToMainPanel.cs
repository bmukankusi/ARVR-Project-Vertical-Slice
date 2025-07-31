using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainPanel : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject infoPanel;

    // Back to Main Panel
    public void ToMainPanel()
    {
        settingsPanel.SetActive(false);
        infoPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

}
