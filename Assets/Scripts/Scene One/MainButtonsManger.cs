using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainButtonsManger : MonoBehaviour
{
    public Button StartButton;
    public Button SettingsButton;
    public Button InfoButton;

    public GameObject MainPanel;
    public GameObject SettingsPanel;
    public GameObject InfoPanel;

    // Start VR scene
    public void StartVRScene()
    {
        SceneManager.LoadScene("Virtual Tour");
    }

    // Open Settings Panel
    public void OpenSettingsPanel()
    {
        MainPanel.SetActive(false);
        InfoPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    // Open Info Panel
    public void OpenInfoPanel()
    {
        MainPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        InfoPanel.SetActive(true);
    }

}
