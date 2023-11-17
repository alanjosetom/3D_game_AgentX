using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnedEnemyController : MonoBehaviour
{

    public NavMeshAgent meshAgent;
    public float startwaitTime = 4;
    public float timeTorotate = 2;

    public float walkSpeed = 3;
    public float runSpeed = 6;
    public float f_followRange = 15;
    public float followAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstcMask;
    public bool b_followRange;
    public float f_attackRange;
    public bool b_attackRange;
    Vector3 playerLastPos = Vector3.zero;
    Vector3 PlayerPos;
    float m_waitTime;
    float m_timetoRotate;
    bool m_Playerrange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;
    public bool isPatrol = true;

    int c_wayPointIndex;
    Vector3 target;
    public GameObject ManagerScript;
    public Transform[] waypoints;
    public Animator anim;
    public bool firePlayer = false;
    public GameObject positionBullet;
    public GameObject Bullet;

    private float timeBtwShoots;
    public float StartTimeB;
    public int MaxHealth = 100;
    public int currentHealth;
    public enemyHealthbar healthBar;
    public bool deid = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        healthBar.SetHealth(MaxHealth);
        timeBtwShoots = StartTimeB;
        // timeBtwShoots = StartTimeB;
        PlayerPos = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_Playerrange = false;
        m_PlayerNear = false;
        m_waitTime = startwaitTime;                 //  Set the wait time variable that will change
        m_timetoRotate = timeTorotate;

        c_wayPointIndex = 0;
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.isStopped = false;
        meshAgent.speed = walkSpeed;
        meshAgent.SetDestination(waypoints[c_wayPointIndex].position);
        anim = gameObject.GetComponent<Animator>();
        // Patrol();

    }

    // Update is called once per frame
    void Update()
    {

        var targetScript = ManagerScript.GetComponent<GameManager>();
        var gameOn = targetScript.gameOn;
        gameObject.transform.Rotate(new Vector3(0, 0, 0) * Time.deltaTime, Space.World);
        if (gameOn)
        {
            if (deid == false)
            {
                Env();

                // if (Vector3.Distance(transform.position, target) < 1)
                // {
                //     Patrol();
                // }
                if (!m_IsPatrol)
                {

                    Chasing();


                }
                else
                {

                    Patrol();


                }
            }

            if (firePlayer)
            {
                if (timeBtwShoots <= 0)
                {
                    EnemyFiring();
                    timeBtwShoots = StartTimeB;
                }
                else
                {
                    timeBtwShoots -= Time.deltaTime;
                }

            }
            if (currentHealth == 0)
            {
                firePlayer = false;
                deid = true;
                anim.SetBool("enemyDied", true);
                meshAgent.isStopped = true;
                meshAgent.speed = 0;
            }

        }
    }
    // void Patrol()
    // {
    //     target = waypoints[c_wayPointIndex].position;
    //     meshAgent.SetDestination(target);
    //     c_wayPointIndex++;
    //     if (c_wayPointIndex == waypoints.Length)
    //     {
    //         c_wayPointIndex = 0;
    //     }
    // }
    private void Patrol()
    {

        if (m_PlayerNear)
        {
            //  Check if the enemy detect near the player, so the enemy will move to that position
            // if (m_timetoRotate <= 0)
            // {
            Move(walkSpeed);
            LookingPlayer(PlayerPos);
            // }
            // else
            // {
            //     //  The enemy wait for a moment and then go to the last player position
            //     Stop();

            //     m_timetoRotate -= Time.deltaTime;
            // }
        }
        else
        {
            m_PlayerNear = false;           //  The player is no near when the enemy is platroling
            PlayerPos = Vector3.zero;

            meshAgent.SetDestination(waypoints[c_wayPointIndex].position);    //  Set the enemy destination to the next waypoint
            if (meshAgent.remainingDistance <= meshAgent.stoppingDistance)
            {
                //  If the enemy arrives to the waypoint position then wait for a moment and go to the next
                if (m_waitTime <= 0)
                {
                    NextPoint();
                    Move(walkSpeed);
                    m_waitTime = startwaitTime;
                }
                else
                {
                    Stop();
                    m_waitTime -= Time.deltaTime;
                }
            }
        }
    }
    void Chasing()
    {
        m_PlayerNear = false;
        playerLastPos = Vector3.zero;

        if (!m_CaughtPlayer)
        {
            Move(runSpeed);
            meshAgent.SetDestination(PlayerPos);          //  set the destination of the enemy to the player location
        }
        if (meshAgent.remainingDistance <= meshAgent.stoppingDistance)    //  Control if the enemy arrive to the player location
        {
            if (m_waitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 7f)
            {
                //  Check if the enemy is not near to the player, returns to patrol after the wait time delay
                m_IsPatrol = true;
                m_PlayerNear = false;
                anim.SetBool("firePlayer", false);
                firePlayer = false;
                Move(walkSpeed);
                m_timetoRotate = timeTorotate;
                m_waitTime = startwaitTime;
                meshAgent.SetDestination(waypoints[c_wayPointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 4f)
                    //  Wait if the current position is not the player position
                    Stop();
                m_waitTime -= Time.deltaTime;
            }
        }
    }

    void Stop()
    {
        meshAgent.isStopped = true;
        meshAgent.speed = 0;
        anim.SetBool("enemyWalk", false);
    }

    void Move(float speed)
    {
        anim.SetBool("enemyWalk", true);
        meshAgent.isStopped = false;
        // anim.SetBool("", true);
        meshAgent.speed = speed;

    }

    void CaughtPlayer()
    {
        m_CaughtPlayer = true;


    }
    public void NextPoint()
    {
        // c_wayPointIndex = (c_wayPointIndex + 1) % waypoints.Length;
        c_wayPointIndex++;
        if (c_wayPointIndex == waypoints.Length)
        {
            c_wayPointIndex = 0;
        }
        // c_wayPointIndex = Random.Range(0, waypoints.Length);
        meshAgent.SetDestination(waypoints[c_wayPointIndex].position);
    }
    void Env()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, f_followRange, playerMask);
        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < followAngle / 2)
            {
                float distToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, distToPlayer, obstcMask))
                {
                    m_Playerrange = true;
                    m_IsPatrol = false;
                }
                else
                {
                    m_Playerrange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > f_followRange)
            {
                m_Playerrange = false;
            }

            if (m_Playerrange)
            {
                /*
                 *  If the enemy no longer sees the player, then the enemy will go to the last position that has been registered
                 * */
                PlayerPos = player.transform.position;
                anim.SetBool("firePlayer", true);
                firePlayer = true;     //  Save the player's current position if the player is in range of vision
            }
        }
    }
    void LookingPlayer(Vector3 player)
    {
        meshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (m_waitTime <= 0)
            {
                m_PlayerNear = false;
                Move(walkSpeed);
                meshAgent.SetDestination(waypoints[c_wayPointIndex].position);
                m_waitTime = startwaitTime;
                m_timetoRotate = timeTorotate;
            }
            else
            {
                Stop();
                m_waitTime -= Time.deltaTime;
            }
        }
    }
    void EnemyFiring()
    {
        GameObject spawnedBullet = Instantiate(Bullet, positionBullet.transform.position, transform.rotation);

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("playerBullet"))
        {
            Damage(25);
        }
    }
    void Damage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

}
