using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{

    public float health = 100;


    public GameObject mainCamera;

    private FirstPersonController fpc;
    public int noiceLevel = 0;
    private int sneakNoiceLevel = 0;
    private int walkNoiceLevel = 25;
    private int runNoiceLevel = 50;

    private Vector3 currentPosition;
    private Vector3 previousPosition;

    public bool gameOver = false;
    public int[] keysCollected;




    //UI Elements
    private float maxHealth;
    public Slider healthbar;

    // Use this for initialization
    void Start()
    {
        maxHealth = health;
        fpc = GetComponent<FirstPersonController>(); // not sure if this works
        keysCollected = new int[] { 0, 0, 0 };
    }

    // Update is called once per frame
    void Update()
    {
        previousPosition = currentPosition;
        currentPosition = fpc.transform.position;

        UpdateNoiceLevel();



    }


    private void UpdateNoiceLevel()
    {
        if (currentPosition == previousPosition)
        {
            noiceLevel = 0;
        }
        else
        {
            noiceLevel = fpc.m_IsWalking ? walkNoiceLevel : runNoiceLevel;
            noiceLevel = fpc.m_IsSneaking ? sneakNoiceLevel : noiceLevel;
        }

    }

    public void takeDamage(float dmg)
    {
        health -= dmg;

        healthbar.value = (health / maxHealth);

        if (health <= 0)
        {
            mainCamera.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Pedestal"))
        {
            if (other.transform.position.z > 30)
           {
                keysCollected[0] = 1;
            }
            else if (other.transform.position.z < -30)
            {
                keysCollected[1] = 1;
            }
            else
           {
                keysCollected[2] = 1;
            }
    
        }
        else if (other.tag.Equals("Portal"))
        {
            int sumOfKeys = keysCollected[0] + keysCollected[1] + keysCollected[2];
            if (sumOfKeys == 3)
            {
                gameOver = true;
                // TODO: change to the win scene
                SceneManager.LoadScene("win");

            }
        }
    }

}
