using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;




public class GameManager : MonoBehaviour
{
    public GameObject findEnemy;
    public bool gameOn = false;
    public bool isPause = false;
    public GameObject canvas_Obj;
    public GameObject gunObject;
    public float timeLeft = 20.0f;
    public TextMeshProUGUI TimeText;
    public GameObject spawnText;
    public GameObject enemyObj;
    public bool firstSpawn = true;
    public bool timer;
    public GameObject timeText;
    public GameObject newEnm1;
    public GameObject newEnm2;

    public GameObject newEnm3;

    // public int Xpos;
    // public int Ypos;
    public int enemyCount;
    public AudioClip thisAudioClip;
    private AudioSource AudioSource;
    public int bulletcount = 40;
    int[] Xspawnloaction = { 6, 80, 109, 109 };
    float[] Yspawnloaction = { 1.63f, 1.88f, 2.01f, 2.06f };
    int[] Zspawnloaction = { -29, 33, -19, -82 };
    public GameObject gen;
    public GameObject playerObj;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (OVRInput.Get(OVRInput.Button.One))
        // {
        //     Debug.Log("Button Pressed");
        // }
        if (gameOn)
        {
            if (bulletcount <= 0)
            {
                gameOn = false;

            }
            if (timer)
            {

                timeLeft -= Time.deltaTime;
                var minutes = Mathf.FloorToInt(timeLeft / 60);
                var seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
                // Convert integer to string
                string TimeDisplay = string.Format("{0:0}:{1:00}", minutes, seconds);
                // Debug.Log("t" + minutes);
                // Debug.Log("t" + seconds);
                TimeText.text = TimeDisplay;
                if (minutes == 0 && seconds == 0 && firstSpawn)
                {
                    spawnText.SetActive(true);
                    firstSpawn = false;
                    EnemySpawn();
                    timeText.SetActive(false);

                }
                else
                {
                    spawnText.SetActive(false);
                }
            }
            if (findEnemy == null)
            {
                findEnemy = GameObject.FindWithTag("enemyWithcode");
                if (findEnemy == false)
                {
                    playerObj.GetComponent<Player>().handCode = true;
                }
            }
            if (timer == false)
            {
                timeText.SetActive(false);
            }

        }

    }
    public void StartGame()
    {
        AudioSource.PlayOneShot(thisAudioClip);
        canvas_Obj.SetActive(false);
        gameOn = true;
        timer = true;
        gunObject.SetActive(true);

    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }
    public void EnemySpawn()
    {
        // for (int i = 0; i < Xspawnloaction.Length; i++)
        // {
        //     Instantiate(enemyObj, new Vector3(Xspawnloaction[i], Yspawnloaction[i], Zspawnloaction[i]), Quaternion.identity);
        // }
        newEnm1.SetActive(true);
        newEnm2.SetActive(true);
        newEnm3.SetActive(true);


    }

}
