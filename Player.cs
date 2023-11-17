using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public bool handCode;
    private GameObject enemyWithcode;
    public GameObject gameman;
    public GameObject Sci;
    void Start()
    {

        currentHealth = MaxHealth;
        healthBar.SetHealth(MaxHealth);


    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("GameOver");

        }
        // enemyWithcode = GameObject.FindGameObjectWithTag("enemyWithcode");
        // if (!enemyWithcode)
        // {
        //     handCode = true;
        // }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("enemyBullet"))
        {
            Damage(4);
        }
        if (other.gameObject.CompareTag("mag"))
        {
            gameman.GetComponent<GameManager>().bulletcount += 10;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("win"))
        {
            if (Sci == true)
            {
                if (Vector3.Distance(transform.position, Sci.transform.position) <= 4f)
                    Debug.Log("Won");
            }
        }
    }
    void Damage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

}
