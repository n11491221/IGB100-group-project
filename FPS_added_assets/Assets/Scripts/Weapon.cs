using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    Animation animation;

    //Damage variables
    public float damage = 10.0f;
    public float fireRate = 1.0f;
    public float fireTime;

    //Spawn Effects and target objects
    public GameObject muzzleFlash;
    public GameObject bulletHit;
    public GameObject muzzle;
    public GameObject target;

    //Audio effect
    public GameObject fireSound;

    //Visual projectile
    public GameObject arrow;


    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        WeaponFiring();

    }

    private void WeaponFiring()
    {
        if (Input.GetMouseButton(0) && Time.time > fireTime)
        {
            Instantiate(arrow, transform.position, transform.rotation);

            //Setup next time to fire
            fireTime = Time.time + fireRate;



        }
    }
}
