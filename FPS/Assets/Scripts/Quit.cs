using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public void OnExitGame()
    {
#if UNITY_EDITOR //Exit in the editor

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
    

#endif



    }
}
