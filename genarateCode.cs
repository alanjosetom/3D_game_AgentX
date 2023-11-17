using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genarateCode : MonoBehaviour
{
    public float genRange = 4f;
    public GameObject player;

    public GameObject gameMan;
    // bool handCode = false;
    public bool timerG = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // handCode = player.GetComponent<Player>().handCode;
        if (Vector3.Distance(player.transform.position, transform.position) <= genRange)
        {
            if (player.GetComponent<Player>().handCode)
            {
                gameMan.GetComponent<GameManager>().timer = true;
            }
        }
    }
}
