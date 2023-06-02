using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerE : MonoBehaviour
{
    public GameObject BoxUI;
    public Text BoxText;
    public string npcText;

    private bool playerBox;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            BoxUI.SetActive(false);
            playerBox = false;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BoxText.text = npcText;
            BoxUI.SetActive(true);
            playerBox = true;
            Debug.Log("1");

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            BoxUI.SetActive(false);
            playerBox = false;
            Debug.Log("1");

        }
    }


}