using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{

    NavMeshAgent agent;

    public float health = 100;

    public GameObject target;
    public FirstPersonController fpc;

    private float damage = 25;
    private float damageTime;
    private float damageRate = 0.5f;

    private Player player;
    private Vector3 lastKnownPlayerPosition;
    private Vector3 currentPosition;
    private Vector3 previousPosition;

    private Rigidbody rigidbody;
    private bool isIdle = true;



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

            player = target.GetComponent<Player>();
            rigidbody = this.GetComponent<Rigidbody>();

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
        if (!dead && !attacking)
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
        previousPosition = currentPosition;


        RaycastHit hit = new RaycastHit();
        bool canSeePlayer = target && !(Physics.Linecast(transform.position, target.transform.position, out hit) && hit.transform.position != target.transform.position);

        if (canSeePlayer || Vector3.Distance(target.transform.position, transform.position) <= player.noiceLevel)
        {
            isIdle = false;
            walking = true;
            //agent.destination = target.transform.position;
            agent.SetDestination(target.transform.position);
            lastKnownPlayerPosition = target.transform.position;

        }
        else
        {

            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        isIdle = true;
                        walking = false;
                        agent.SetDestination(transform.position);
                    } else
                    {
                        isIdle = false;
                        walking = true;
                        agent.SetDestination(lastKnownPlayerPosition);
                    }
                } else
                {
                    isIdle = false;
                    walking = true;
                    agent.SetDestination(lastKnownPlayerPosition);
                }
            }
            else
            {
                isIdle = false;
                walking = true;
                agent.SetDestination(lastKnownPlayerPosition);
            }


            /*
            if (!isIdle && agent.CalculatePath(lastKnownPlayerPosition, new NavMeshPath()))  // if the enemy can get closer to lastKnownPlayerPosition
            {
                isIdle = false;
                walking = true;
                agent.SetDestination(lastKnownPlayerPosition);

            }
            else
            {
                // go idle
                isIdle = true;
                walking = false;
                agent.SetDestination(transform.position);

                rigidbody.velocity = Vector3.zero;

            }
            */

        }

    }


    private void IdleMovement()
    {
        agent.SetDestination(transform.position);
    }

    private void UpdateDetectionRange()
    {
        RaycastHit hit = new RaycastHit();
        bool canSeePlayer = target && !(Physics.Linecast(transform.position, target.transform.position, out hit) && hit.transform.position != target.transform.position);

        if (canSeePlayer)
        {
            currentDetectionRange = fpc.m_IsWalking ? walkDetectionRange : sprintDetectionRange;
            currentDetectionRange = fpc.m_IsSneaking ? sneakDetectionRange : currentDetectionRange;
        }
        else
        {
            currentDetectionRange = 0;
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
            rigidbody.velocity = Vector3.zero;
            rigidbody.isKinematic = true;

            Destroy(this.gameObject, 5);
        }
    }

    private void OnTriggerEnter(Collider otherObject)
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

    private void OnTriggerExit(Collider other)
    {
        attacking = false;
    }

}
