using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    public enum EnemyState
    {
        PATROL,
        CHASE,
        INVESTIGATE 
    }

    public NavMeshAgent agent;
    public GameObject enemyBody;
    public GameObject target;
    public EnemyState enemyState;

    public GameObject[] waypoints;

    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 3f;

    // Float array containg angles for gerald to cylce through to see the player, if one collides with the player then he will chase
    float[] lookAngles = {-2f, -1.4f, -1f, -0.75f, -0.6f, -0.5f, -0.4f, -0.3f, -0.2f, -0.1f, 0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.75f, 1f, 1.4f, 2f};
    float[] upAngles = {-0.1f, 0f, -0.2f, -0.3f, -0.4f, -0.6f};

    public float lookDistance = 6.0f; // Float for determining for far forward Gerald can see
    public float rotationSpeed = 50f; 
    public float maxRotation = 300f;
    public float timer = 0;
    public float lastSeenTimer = 10f;

    public bool meowHeard = false;

    Vector3 targetLocation;
    Vector3 startingPos;
    
    public bool foundKitty = false;
    int waypointIndex;
    public float waypointProximity = 2.0f;

    Vector3 investigateLocatation;
    
    void Start()
    {
        startingPos = transform.position;
        enemyState = EnemyState.PATROL;

        waypoints = GameObject.FindGameObjectsWithTag("Waypoints");
        waypointIndex = Random.Range(0, waypoints.Length);
    }

    void Update()
    {
        if(enemyState == EnemyState.CHASE)
        {
            Chase();
        }
        if(enemyState == EnemyState.PATROL)
        {
            Patrol();
        }
        if(enemyState == EnemyState.INVESTIGATE)
        {
            Investigate();
        }
        //transform.rotation = Quaternion.Euler(maxRotation * Mathf.Sin(Time.time * rotationSpeed), 0f , 0f);

        if(foundKitty == false && enemyState == EnemyState.CHASE)
        {
            timer += Time.deltaTime;
        }
        if(foundKitty == true && enemyState == EnemyState.CHASE)
        {
            timer = 0;
        }

                        
    }
    void FixedUpdate()
    {
        foundKitty = false;

        // below casts 21 * 6 raycasts in front of the chaser to detect the player. I have read online that raycasts are not terribly heavy for the computer, so I figured it is fine even though there is a lot of rays being cast.
        RaycastHit hit;
        for(int z = 0; z < upAngles.Length; z++)
        {
            for(int i = 0; i < lookAngles.Length; i++)
            {
                Debug.DrawRay(transform.position, (transform.forward + (transform.up * upAngles[z]) + (transform.right * lookAngles[i])).normalized * (lookDistance - (2*Mathf.Abs(lookAngles[i])) + (4*upAngles[z])), Color.red);
                // we do some fun maths I just thought of to constrict his vison to confine it with something more human. It was a fun exercise thinking about how I could use the array to restrict the rays made from the array, I think it works quite well.
                //Debug.Log("I shot ray " + i);
                if(Physics.Raycast(transform.position, (transform.forward + (transform.up * upAngles[z]) + (transform.right * lookAngles[i])).normalized, out hit, (lookDistance - (2*Mathf.Abs(lookAngles[i])) + (4*upAngles[z]))))
                {
                    GameObject hitObj = hit.transform.gameObject;
                    if(hitObj.tag == "Player")
                    {
                        enemyState = EnemyState.CHASE;
                        target = hitObj;
                        //targetLocation = hitObj.transform.position;
                        lookDistance = 9f;
                        Debug.Log("I found you kitty!!!");
                        foundKitty = true;
                        return;
                    }
                }
                
            }
        }
        
    }
    void Chase()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(target.transform.position);

        if(timer >= lastSeenTimer)
        {
            enemyState = EnemyState.PATROL;
            lookDistance = 6f;
            Debug.Log("RATS! I LOST HIM!!!!");
            timer = 0;
        }
    }
    void Patrol()
    {
        agent.speed = patrolSpeed;
        //agent.SetDestination(startingPos);
        //enemyBody.transform.rotation = Quaternion.Euler(0f, maxRotation * Mathf.Sin(Time.time * rotationSpeed) , 0f);

        if(Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position) >= waypointProximity)
        {
            agent.SetDestination(waypoints[waypointIndex].transform.position);
        }
        else if(Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position) <= waypointProximity)
        {
            waypointIndex = Random.Range(0, waypoints.Length);
        }

    }

    void Investigate()
    {
        agent.speed = patrolSpeed;
        agent.SetDestination(investigateLocatation);

        if(Vector3.Distance(transform.position, investigateLocatation) >= waypointProximity)
        {
            if(timer >= lastSeenTimer)
            {
                enemyState = EnemyState.PATROL;
                lookDistance = 6f;
                Debug.Log("MUST HAVE BEEN THE WIND");
                timer = 0;
            }
        }

    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            meowHeard = col.gameObject.GetComponent<PlayerController3D>().meowing;
            if(meowHeard == true)
            {
                investigateLocatation = col.gameObject.transform.position;
                enemyState = EnemyState.INVESTIGATE;

                Debug.Log("WHERE IS THAT MEOW COMING FROM");

                col.gameObject.GetComponent<PlayerController3D>().meowing = false;
            }
        }
    }
    
}
