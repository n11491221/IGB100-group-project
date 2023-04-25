using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float health = 100;
    private float maxHealth;

    public GameObject mainCamera;

    //UI Elements
    public Slider healthbar;

	// Use this for initialization
	void Start () {
        maxHealth = health;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Sneak();
        } else
        {
            EndSneak();
        }

	}

    public void takeDamage(float dmg) {
        health -= dmg;

        //healthbar.value = (health / maxHealth);

        if (health <= 0) {
            mainCamera.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    public void Sneak()
    {
        transform.localScale = new Vector3(1, 0.75f, 1);
    }

    public void EndSneak()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
