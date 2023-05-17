using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuList : MonoBehaviour
{
    public GameObject menuList;

    [SerializeField] private bool menuKeys = true;
    //[SerializeField] private AudioSource bgmSound;//BGM Open and stop
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menuKeys)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                menuList.SetActive(true);
                menuKeys = false;
                Time.timeScale = (0);
                //bgmSound.Pause();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            menuList.SetActive(false);
            menuKeys = true;
            Time.timeScale = (1);
            //bgmSound.Pause();

        }

    }

    public void Return()//back game
    {
        menuList.SetActive(false);
        menuKeys = true;
        Time.timeScale = (1);
        //bgmSound.Pause();
    }
    public void MineMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void Exit()
    {
#if UNITY_EDITOR //Exit in the editor

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
    

#endif
    }
}
