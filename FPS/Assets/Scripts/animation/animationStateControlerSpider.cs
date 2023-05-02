using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateControlerSpider : MonoBehaviour
{
    //link to enemy scrpt
    public Enemy enemy;
    //link to unity animator
    Animator animator;
    //get animation
    //public Animator death;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks state of the spider and animates accordingly
        //walking
        if (enemy.walking == true)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        //attaccking
        if (enemy.attacking == true)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

        //dead
        if(enemy.dead == true)
        {
            animator.Play("Death");
        }
        
    }
}
