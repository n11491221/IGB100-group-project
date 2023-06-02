using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Finish : MonoBehaviour
{
    
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision .gameObject.name == "Player")
        {
            
            Invoke("finsihLeve",0.5f);
            

        }
    }
    private void finsihLeve()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
