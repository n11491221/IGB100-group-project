using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Text highScore;

    // Start is called before the first frame update
    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadScene(string levelname)
    {
        SceneManager.LoadScene(levelname);
    }

}
