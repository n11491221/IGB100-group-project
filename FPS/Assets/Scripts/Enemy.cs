using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using System;

public class Enemy : MonoBehaviour
{

    NavMeshAgent agent;

    public float health = 100;
    //public GameObject enemyImage;
    public Image uiImage;


    AudioSource audioS;
    public AudioClip runing;
    public AudioClip die;
    public AudioClip see;
    public GameObject target;
    public FirstPersonController fpc;

    private float damage = 25;
    private float damageTime;
    private float damageRate = 0.5f;

    private Player player;
    private Vector3 lastKnownPlayerPosition = new Vector3(0, 0, 0);
    private Vector3 patrolLocation;
    private Vector3 nextPatrolDestination;
    private int patrolDistance = 20;


    private Rigidbody rigidbody;
    private bool isOnPatrol = true;


    //Patrol (idle movement)
    private int destPoint = 0;

    public bool walking = false;
    public bool attacking = false;
    public bool dead = false;

    //Effects
    public GameObject deathEffect;

    //Enemy Heath bar
    private float maxHealth;
    public Slider Ehealthbar;

    // Use this for initialization
    void Start()
    {
        //Enemies renew their health when they start.
        maxHealth = health;
        audioS = GetComponent<AudioSource>();// 
        //gameObject.transform.GetChild(0).GetChild(0).GetComponent<Slider>().maxValue = health;
        //gameObject.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = health;

        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        agent.stoppingDistance = 1.0f;


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
        StartPatrol();

        Patrol();
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
        bool canHearPlayer = Vector3.Distance(target.transform.position, transform.position) <= player.noiceLevel;
        audioS.clip = runing;
        audioS.Play();
        if (canSeePlayer || canHearPlayer)
        uiImage.gameObject.SetActive(true);//The enemy finds the player.
        //audioS.clip = runing;
        //audioS.Play();
        if (canSeePlayer || Vector3.Distance(target.transform.position, transform.position) <= player.noiceLevel)
        {
            isOnPatrol = false;
            walking = true;
            audioS.clip = see;
            audioS.Play();
            //found player
            //agent.destination = target.transform.position;
            agent.SetDestination(target.transform.position);
            lastKnownPlayerPosition = target.transform.position;
            patrolLocation = target.transform.position;

        }
        else if (!isOnPatrol /*agent.destination.Equals(lastKnownPlayerPosition)*/)
        {
            if (!agent.pathPending) //check if enemy has reached lastKnownPlayerPosition
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        // If yes, start patrolling
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
            Patrol();
        }

    }

    private void StartPatrol() // somehting is not working when the enemy has never noticed the player???
    {
        isOnPatrol = true;
        walking = true;

        if (! lastKnownPlayerPosition.Equals(new Vector3(0,0,0))) // if they have noticed the player previously
        {
            // patrol around lastKnownPlayerLocation
            patrolLocation = lastKnownPlayerPosition;
            nextPatrolDestination = patrolLocation;
        }
        else
        {
            //Patrol around where they are
            patrolLocation = transform.position;
            Debug.Log("transform.position = " + patrolLocation);
            nextPatrolDestination = patrolLocation + new Vector3(1, 0, 1);
            Debug.Log("nextPatrolDestination = " + nextPatrolDestination);

        }

        agent.destination = nextPatrolDestination;

        Patrol();
    }

    // defines the enemies movement when idle (doesn't know where player is)
    private void Patrol()
    {
        // walk in a random direction within a radius of the patrolLocation
        NavMeshPath navMeshPath = new NavMeshPath();
        bool canReachDestination = agent.CalculatePath(nextPatrolDestination, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete;
        bool isAtPosition = agent.remainingDistance <= agent.stoppingDistance;

        if (Vector3.Distance(transform.position, patrolLocation) > patrolDistance || isAtPosition || !canReachDestination)
        {
            //walk to a random spot around partolLocation
            System.Random random = new System.Random();
            nextPatrolDestination = patrolLocation + new Vector3(random.Next(-patrolDistance, patrolDistance), 0, random.Next(-patrolDistance, patrolDistance));
            agent.destination = nextPatrolDestination;
        }
    }


    //Public method for taking damage and dying
    public void takeDamage(float dmg)
    {
        health -= dmg;

        //If the enemy is not dead, but only receives damage, update the health bar (value).
        Ehealthbar.value = (health / maxHealth);

        if (health <= 0)
        {
            uiImage.gameObject.SetActive(false);//Enemy death Disappear UI.
            audioS.clip = die;
            audioS.Play();
            //Instantiate(deathEffect, transform.position, transform.rotation);
            dead = true;
            rigidbody.velocity = Vector3.zero;
            rigidbody.isKinematic = true;
            //enemyImage.SetActive(false);//found player
            Destroy(this.gameObject, 5);
            GameManager.instance.score += 1;
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
