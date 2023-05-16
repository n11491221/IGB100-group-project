using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{

    public float health = 100;
    private float maxHealth;

    public GameObject mainCamera;

    private FirstPersonController fpc;
    public int noiceLevel = 0;
    private int sneakNoiceLevel = 0;
    private int walkNoiceLevel = 25;
    private int runNoiceLevel = 50;

    private Vector3 currentPosition;
    private Vector3 previousPosition;






    //UI Elements
    public Slider healthbar;

    // Use this for initialization
    void Start()
    {
        maxHealth = health;
        fpc = GetComponent<FirstPersonController>(); // not sure if this works
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
        } else
        {
            noiceLevel = fpc.m_IsWalking ? walkNoiceLevel : runNoiceLevel;
            noiceLevel = fpc.m_IsSneaking ? sneakNoiceLevel: noiceLevel;
        }

    }

    public void takeDamage(float dmg)
    {
        health -= dmg;

        //healthbar.value = (health / maxHealth);

        if (health <= 0)
        {
            mainCamera.SetActive(true);
            Destroy(this.gameObject);
        }
    }

}
