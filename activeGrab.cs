using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class activeGrab : MonoBehaviour
{
    public GameObject leftRay;
    public GameObject rightRay;
    public XRDirectInteractor leftDirect;
    public XRDirectInteractor rightDirect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        leftRay.SetActive(leftDirect.interactablesSelected.Count == 0);
        rightRay.SetActive(rightDirect.interactablesSelected.Count == 0);

    }
}
