using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameMenuManager : MonoBehaviour
{
    public GameObject MenuObj;
    public Transform Playerpos;
    public float spawnpos = 2;
    public InputActionProperty showBtn;
    public GameObject ManagerScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var targetScript = ManagerScript.GetComponent<GameManager>();
        var gameOn = targetScript.gameOn;
        if (showBtn.action.WasPressedThisFrame())
        {
            Debug.Log("get");
            if (gameOn)
            {

                // MenuObj.SetActive(!MenuObj.activeSelf);
                MenuObj.SetActive(false);
                MenuObj.transform.position = Playerpos.position + new Vector3(Playerpos.forward.x, 0, Playerpos.forward.z).normalized * spawnpos;
                gameOn = false;

            }
            else
            {
                MenuObj.SetActive(true);
                gameOn = true;

            }

        }
        MenuObj.transform.LookAt(new Vector3(Playerpos.position.x, MenuObj.transform.position.y, Playerpos.position.z));
        MenuObj.transform.forward *= -1;
    }
}
