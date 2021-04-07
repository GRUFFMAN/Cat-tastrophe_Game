using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    [Header("GameObjects")]
        [SerializeField] public GameObject playerPrefab; //I assume is the player
        [SerializeField] public GameObject player2; //is the enemy prefab
    
    [Header("Game Elements")]
        [SerializeField] public int gameScore = 0;   // initialize the game score
        [SerializeField] public int gameAmmo = 0;    //ammo
        [SerializeField] public int gameHealth = 10; //health
        [SerializeField] public float gameplaySpeed = 1.0f; // sets the player speed forward

    [Header("UI Elements")]
        [SerializeField] public Text scoreText;     // accessing the UI    
        [SerializeField] public Text ammoText;      // accessing the UI
        [SerializeField] public Text healthText;    // accessing the UI

    [Header("Ememy Spawn")] 
        [SerializeField] public Rigidbody basicEnemy; // acessing the enemy prefab
        [SerializeField] public Rigidbody fastEnemy; // acessing the enemy prefab
        [SerializeField] public Rigidbody heavyEnemy; // acessing the enemy prefab
        [SerializeField] public GameObject bossEnemy; // acessing the enemy prefab
    
    [Header("Ememy Spawn Parameters")] 
        [SerializeField] public float minX = -10.0f; // Min/max variables define early spawn placement system, these are relative to the GameplayRealm
        [SerializeField] public float maxX =  10.0f;
        [SerializeField] public float minY = -5.0f;
        [SerializeField] public float maxY =  7.0f;
        [SerializeField] public float minZ =  80.0f;
        [SerializeField] public float maxZ = 100.0f;
        [SerializeField] public bool bossSpawn = false;
    
    [Header("Ememy Spawn Timers")] 
        [SerializeField] public float basicTimer = 10.0f; // basicTimer until new enenmy ship is spawned
        [SerializeField] public float fastTimer = 20.0f;
        [SerializeField] public float heavyTimer = 35.0f;
        [SerializeField] public float bossTimer = 300.0f;
    
    [Header("PowerUp Spawn")] //basicTimer and rigid body for spawning a powerup every minute in case of terrible enemy drop rates. might innovate a weighted chance if the player has had either really bad luck or good luck
        [SerializeField] public float powerUpTimer = 60.0f;
        [SerializeField] public Rigidbody PowerUP;
    
    ///////////////////////////////////////////////////////////////// START OF TESTING
    
    [Header("testing")]

        [SerializeField] [Tooltip("How far the boresight and mouse flight are from the aircraft")]
        private float aimDistance = 500f;
        [SerializeField] [Tooltip("Transform of the aircraft the rig follows and references")]
        private Transform aircraft = null;
    public Vector3 BoresightPos
    {
        get
        {
            return aircraft == null
            ? transform.forward * aimDistance
            :(aircraft.transform.forward * aimDistance) + aircraft.transform.position;
        }
    }

    void Update()
    {

        UpdateUI();
        SpawnPowerUp();
        MoveForward();
        SpawnBossEnemy();  
        if(bossSpawn == false)
        {
            SpawnBasicEnemy();
            SpawnFastEnemy();
            SpawnHeavyEnemy();
        }   
    }

    void MoveForward()
    {
        if(player2.activeSelf == false) //if dead stop moving.
        {
            gameplaySpeed = 0;
        }
        transform.position += new Vector3(0,0,3)*gameplaySpeed*Time.deltaTime; // move the GameplayRealm and it's contents forard by gameplaySpeed
    }

    void UpdateUI()
    {
        string s;  //the following is for logging the Score, ammo and health to the Gameplay UI
        s = gameScore.ToString();
        scoreText.text = "Score:" +  s; // adding the score to the UI
        ///gameAmmo = playerPrefab.GetComponent<ShootForward>().ammoCount; //grabing the ammo amount from the ShootForward script
        string a;
        a = gameAmmo.ToString();
        ammoText.text = "Ammo:" +  a; // ammo to the UI
        //gameHealth = player2.GetComponent<PlayerMovement>().currentHealth;
        string h;
        h = gameHealth.ToString();
        healthText.text = "Health:" +  h; // ammo to the UI
    }

    void SpawnPowerUp()
    {
        powerUpTimer -= (1* Time.deltaTime); // this is for the basic spawning of a powerup every minute regardless of enenmy drops
        if(powerUpTimer <= 0) //when the powerup basicTimer hits 0, a power is spawned and basicTimer reset
        {
            Rigidbody newPowerUP = Instantiate(PowerUP, transform.position + new Vector3(Random.Range(minX, maxX),Random.Range(minY, maxY),Random.Range(minZ, maxZ)), PowerUP.rotation);
            powerUpTimer = 120.0f;
        }
    }

    void SpawnBasicEnemy()
    {
        basicTimer -= (1* Time.deltaTime); // this is for the basic enenmy spawner, when at 0 an enemy spawns 
        if(basicTimer <= 0) // if the basicTimer is below or equal to 0 spawn a new enemy ship at a random range distance.
        {    
            basicTimer = 2.5f; //reset the basicTimer
            Rigidbody newBasicEnemy = Instantiate(basicEnemy, transform.position + new Vector3(Random.Range(minX, maxX),Random.Range(minY, maxY),Random.Range(minZ, maxZ)), Quaternion.Euler(0, 180, 0));
            //newBasicEnemy.GetComponent<EnemyBehavior>().player2 = gameObject;
            //newBasicEnemy.GetComponent<EnemyBehavior>().enemyMovementSpeed = 20.0f; //spawn the bad guy and give him basic movement 
        }       
    }

    void SpawnFastEnemy()
    {
        fastTimer -= (1* Time.deltaTime); 
        if(fastTimer <= 0)
        {
            fastTimer = 7f;
            Rigidbody newFastEnemy = Instantiate(fastEnemy, transform.position + new Vector3(Random.Range(minX, maxX),Random.Range(minY, maxY),Random.Range(minZ, maxZ)), Quaternion.Euler(0, 180, 0));
        }
    }

    void SpawnHeavyEnemy()
    {
        heavyTimer -= (1* Time.deltaTime); 
        if(heavyTimer <= 0)
        {
            heavyTimer = 12.5f;
            Rigidbody newHeavyEnemy = Instantiate(heavyEnemy, transform.position + new Vector3(Random.Range(minX, maxX),Random.Range(minY, maxY),Random.Range(minZ, maxZ)), Quaternion.Euler(0, 180, 0));
        }
    }

    void SpawnBossEnemy()
    {
        bossTimer -= (1* Time.deltaTime); 
        if(bossTimer <= 0)
        {
            bossSpawn = true;
            bossTimer = 99999999999999f;
            bossEnemy.SetActive(true);
        }
    }
    
    
}