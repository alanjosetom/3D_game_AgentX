using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SdoorOpen : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator doorOpen;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            doorOpen.SetBool("isOpen", true);
        }
        else
        {
            doorOpen.SetBool("isOpen", false);
        }
    }
}
