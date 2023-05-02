using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    NavMeshAgent agent;

    public float health = 100;

    public GameObject target;

    private float damage = 25;
    private float damageTime;
    private float damageRate = 0.5f;

    private float detectionRange = 25;

    public bool walking = false;
    public bool attacking = false;
    public bool dead = false;

    //Effects
    public GameObject deathEffect;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();

        //Player Reference exception catching
        try
        {
            target = GameObject.FindGameObjectWithTag("Player");//Label names are case sensitive
        }
        catch
        {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update () {

        //make sure spider isnt dead
        if (dead == false)
        {
            Movement();
        }
        else
        {
            agent.destination = transform.position;
        }
	}

    private void Movement()
    {
        if (target && Vector3.Distance(target.transform.position, transform.position) <= detectionRange)
        {
            agent.destination = target.transform.position;
            
            walking = true;

        }
        else
        {
            walking = false;
            agent.destination = transform.position;
        }
        

        

    }


    //Public method for taking damage and dying
    public void takeDamage(float dmg) {
        health -= dmg;

        if (health <= 0) {
            
            //Instantiate(deathEffect, transform.position, transform.rotation);
            dead = true;
            Destroy(this.gameObject, 5);
        }
    }

    private void OnTriggerStay(Collider otherObject) {
        //make sure dont do damage will it is dead
        if (otherObject.transform.tag == "Player" && Time.time > damageTime && dead == false) {
            otherObject.GetComponent<Player>().takeDamage(damage);
            damageTime = Time.time + damageRate;
            attacking = true;
        }
        else
        {
            attacking = false;
        }
    }
}
