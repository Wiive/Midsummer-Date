using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void QuitGame()
    {
    //If we are running in the editor
#if UNITY_EDITOR
     //Stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
#else
        //Quit the application
        Application.Quit();
#endif

    }
}
