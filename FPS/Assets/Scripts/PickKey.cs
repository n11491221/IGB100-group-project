using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickKey : MonoBehaviour
{
    public OpenDoor door;
    public GameObject keyObject;
    public Image keyUIImage;
    public AudioSource keyAudioSource;
    public AudioClip keyPickupSound;

    void Start()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {

            keyUIImage.gameObject.SetActive(true);

            door.AddKey();
            keyObject.SetActive(false);
            PlayKeyPickupSound();
        }
    }
    void PlayKeyPickupSound()
    {
        if (keyAudioSource && keyPickupSound)
        {
            keyAudioSource.PlayOneShot(keyPickupSound);
        }
    }


}
