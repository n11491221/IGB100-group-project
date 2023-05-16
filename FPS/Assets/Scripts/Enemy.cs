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


    private Rigidbody rigidbody;
    private bool isOnPatrol = true;


    //Patrol (idle movement)
    public Transform[] points;
    private int destPoint = 0;

    public bool walking = false;
    public bool attacking = false;
    public bool dead = false;

    //Effects
    public GameObject deathEffect;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        agent.stoppingDistance = 1.0f;

        GotoNextPoint();
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
        RaycastHit hit = new RaycastHit();
        bool canSeePlayer = target && !(Physics.Linecast(transform.position, target.transform.position, out hit) && hit.transform.position != target.transform.position);

        if (canSeePlayer || Vector3.Distance(target.transform.position, transform.position) <= player.noiceLevel)
        {
            isOnPatrol = false;
            walking = true;
            //agent.destination = target.transform.position;
            agent.SetDestination(target.transform.position);
            lastKnownPlayerPosition = target.transform.position;

        }
        else if (!isOnPatrol /*agent.destination.Equals(lastKnownPlayerPosition)*/) 
        {
            Debug.Log("IS NOT ON PATROL");
            if (!agent.pathPending) //check if enemy has reach lastKnownPlayerPosition
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        // If yes, start patrolling
                        isOnPatrol = true;
                        StartPatrol();
                        return;
                    }
                }
            }
            //if no, walk to lastKnownPlayerPosition
            isOnPatrol = false;
            walking = true;
            agent.SetDestination(lastKnownPlayerPosition);
        }
        else
        {
            Debug.Log("Is on patrol");
            Patrol();
        }




    }

    private void StartPatrol()
    {
        GotoNextPoint();
    }

    // defines the enemies movement when idle (doesn't know where player is)
    private void Patrol()
    {

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        } else
        {
            Debug.Log("agent.remainingDistance: " + agent.remainingDistance + "\n agent.pathPending: " + agent.pathPending);
        }

        //walking = false;
        //agent.SetDestination(transform.position);
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
        {
            return;
        }

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;

        Debug.Log("agent.destination = " + agent.destination + "\ndestPoint: " + destPoint);
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
