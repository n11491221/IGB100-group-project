using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolldCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Let the blood bar face the main camera.
        gameObject.transform.LookAt(Camera .main .transform );
    }
}
