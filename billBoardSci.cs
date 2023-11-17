using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billBoardSci : MonoBehaviour
{
    public Transform cam;
    // Start is called before the first frame update
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
