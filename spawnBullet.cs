using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBullet : MonoBehaviour
{
    public GameObject positionBullet;
    public GameObject Bullet;
    public float fireSpeed = 20;
    public GameManager gManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnBullet()
    {
        // Instantiate(Bullet, positionBullet.transform.position, transform.rotation);
        GameObject spawnedBullet = Instantiate(Bullet, positionBullet.transform.position, transform.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = positionBullet.transform.forward * fireSpeed * -1;
        gManager.GetComponent<GameManager>().bulletcount -= 1;
    }

}
