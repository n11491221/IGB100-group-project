using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    public GameObject hintImage; // Hint image object
    public string hintText; // Hint text

    private bool hasCollided = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TransparentCollider") && !hasCollided)
        {
            // Show the hint image and text
            hintImage.SetActive(true);
            // Set the hint text content
            // (Assuming your hint text is a Text component, you can get it using GetComponent<Text>())
            // hintTextComponent.text = hintText;

            hasCollided = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TransparentCollider"))
        {
            // Hide the hint image and text
            hintImage.SetActive(false);
            // Clear the hint text content
            // hintTextComponent.text = "";

            hasCollided = false;
        }
    }
}