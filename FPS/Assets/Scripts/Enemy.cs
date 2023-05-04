using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class Enemy : MonoBehaviour
{

    NavMeshAgent agent;

    public float health = 100;

    public GameObject target;
    public FirstPersonController fpc;

    private float damage = 25;
    private float damageTime;
    private float damageRate = 0.5f;

    private float currentDetectionRange = 25;
    private float sneakDetectionRange = 10;
    private float walkDetectionRange = 25;
    private float sprintDetectionRange = 50;

    public bool walking = false;
    public bool attacking = false;
    public bool dead = false;

    //Effects
    public GameObject deathEffect;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //Player Reference exception catching
        try
        {
            target = GameObject.FindGameObjectWithTag("Player");//Label names are case sensitive
            fpc = target.GetComponent<FirstPersonController>();

        }
        catch
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {

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
        UpdateDetectionRange();

        if (target && Vector3.Distance(target.transform.position, transform.position) <= currentDetectionRange)
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

    private void UpdateDetectionRange()
    {

        RaycastHit hit = new RaycastHit();
        if (target && Physics.Linecast(transform.position, target.transform.position, out hit) && hit.transform.position != target.transform.position)
        {
            currentDetectionRange = 0;
        }
        else
        {
            currentDetectionRange = fpc.m_IsWalking ? walkDetectionRange : sprintDetectionRange;
            currentDetectionRange = fpc.m_IsSneaking ? sneakDetectionRange : currentDetectionRange;
        }

        // May want to change something here if we want the player to be able to hide behind trees or make the sneak mechanic better

    }


    //Public method for taking damage and dying
    public void takeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {

            //Instantiate(deathEffect, transform.position, transform.rotation);
            dead = true;
            Destroy(this.gameObject, 5);
        }
    }

    private void OnTriggerStay(Collider otherObject)
    {
        //make sure dont do damage will it is dead
        if (otherObject.transform.tag == "Player" && Time.time > damageTime && dead == false)
        {
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
