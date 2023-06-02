using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;

    public Player player;

    private bool hasSpawnedGuardian = false;

    void Start()
    {
        //Player Reference exception catching
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();//Label names are case sensitive
        }
        catch
        {
            player = null;
        }
    }


    // Update is called once per frame
    void Update()
    {



        if (player && !hasSpawnedGuardian)
        {
            int keysCollected = 0;

            foreach (int key in player.keysCollected)
            {
                keysCollected += key;
            }

            if (keysCollected == 3)
            {
                Instantiate(enemy, transform.position, transform.rotation);
                hasSpawnedGuardian = true;
            }
        }
    }
}
