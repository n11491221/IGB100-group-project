using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //Singleton Setup
    public static GameManager instance = null;

    public float time;
    public float maxTime = 15;

    public GameObject player;

    //UI Elements
    public Text enemiseRemainingText;
    //public Text timeRemainingText;
    public Text scoreText;
    public Text gameOverText;

    //Game Elements
    public GameObject[] enemies;
    public int score = 0;

    public bool gameOver = false;
    public bool dead = false;



    // Awake Checks - Singleton setup
    void Awake() {

        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        time += Time.deltaTime;
        //time = Time.time;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameConditions();

        UpdateUI();
    }

    private void GameConditions()
    {
        if (!player)
        {
            gameOver = true;
            dead = true;
        }
        // LOOK!
        //This can be changed to open the portal and then change the end.
        //else if (time > maxTime && enemies.Length == 0)
        //    gameOver = true;
        if (gameOver)
        {
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

        }
    }

    private void UpdateUI()
    {
        //Enemy
        enemiseRemainingText.text = "Eneies Remaining:" + enemies.Length;

        //Time
        //timeRemainingText.text = "Time Remaining:" + (int)(maxTime - time);

        //Score
        scoreText.text = "Score:" + score;

        //Endgame Text
        if (gameOver && dead)
        {
            gameOverText.text = "You are Dead";
        }
        else if (gameOver)
            gameOverText.text = "You Win!";
    }
}
