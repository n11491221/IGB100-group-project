using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float lifeTime = 3.0f;
    public float moveSpeed = 50.0f;

    public float damage = 10.0f;
    //public GameObject projectileHit;

    public float rotateSpeed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    private void Movement()
    {
        transform.position += Time.deltaTime * moveSpeed * transform.forward;
        //Makes the arrow rotate
        transform.Rotate(new Vector3(0, 0, rotateSpeed));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            //Damage enemies
            if (other.transform.tag == "Enemy")
            {
                other.transform.GetComponent<Enemy>().takeDamage(damage);
            }
            //Instantiate(projectileHit, other.transform.position, other.transform.rotation);
 
            Destroy(this.gameObject);
        }
    }
}
