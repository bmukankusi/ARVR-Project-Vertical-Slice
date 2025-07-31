using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuitVT : MonoBehaviour
{
   public void QuitVTscene()
    {
        //Load start scene
        SceneManager.LoadScene("StartScene");
    }
}
