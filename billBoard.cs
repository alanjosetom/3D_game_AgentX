using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billBoard : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    public Transform cam;
    public GameObject playerObj;
    public float spawnDis = 2;
    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(transform.position + cam.forward);
        // transform.position = new Vector3(playerObj.transform.position.x + 2, playerObj.transform.position.y + 2, playerObj.transform.position.z + 1);
        menu.transform.position = playerObj.transform.position + new Vector3(playerObj.transform.forward.x, 3, playerObj.transform.forward.z).normalized * spawnDis;
        menu.transform.LookAt(new Vector3(playerObj.transform.position.x, menu.transform.position.y, playerObj.transform.position.z));
        menu.transform.forward *= -1;
    }
}
