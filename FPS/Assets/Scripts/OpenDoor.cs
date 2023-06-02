using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animation hingeAnimation;
    private int keyCount = 0; 

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && keyCount >= 3)
        {
            hingeAnimation.Play();
        }
    }

    public void AddKey()
    {
        keyCount++;
    }
}
