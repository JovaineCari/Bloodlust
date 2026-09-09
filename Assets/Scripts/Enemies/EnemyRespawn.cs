using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public float startTime;
    public float elapsedGameTime;
    public float respawnPositions;
    public float respawnTime = 0;
    private bool isRespawning = false;
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public GameObject specialEnemy;
    public float specialEnemyTimer;
    private bool isSpecialEnemyActive = false;
    private static GameObject specialEnemyInstance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        respawnPositions = transform.position.x;
        startTime = Time.unscaledTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the elapsed game time from the start time
        elapsedGameTime = Time.unscaledTime - startTime;
        
        // Increase the difficulty of the game based on elapsed time
        if (elapsedGameTime >= 45 && elapsedGameTime < 90 && respawnTime == 0)
        {
            Debug.LogWarning("Dificulty increased, enemies will spawn faster.");
            respawnTime = 1;
        }
        if (elapsedGameTime >= 90 && respawnTime == 0)
        {
            Debug.LogWarning("Maximum dificulty reached, enemies will spawn even faster.");
            respawnTime = 1.70f;
        }
        
        // Check if the special enemy is active and update the respawn timer
        specialEnemyTimer += Time.deltaTime;
        if (specialEnemyInstance == null) 
        {
            isSpecialEnemyActive = false;
        }
        else
        {
            isSpecialEnemyActive = true;
            specialEnemyTimer = 0;
        }
        // Check if the special enemy should respawn and calls the respawn function
        if (specialEnemyTimer >= 15 && isSpecialEnemyActive == false && specialEnemyInstance == null)
        {
            isRespawning = true;
            isSpecialEnemyActive = true;
            SpecialEnemyRespawn();
            
            
        }

        // Update the respawn timer and check if it's time to respawn a regular enemy
        respawnTime += Time.deltaTime;
        if (respawnTime >= 3)
        {
            isRespawning = true;
            respawnTime = 0;
            Respawn();
        }
        
    }


    void Respawn()
    {


        // Respawn the enemy at the original position
        if (isRespawning == true)
        {
            Object.Instantiate(enemyPrefab).transform.position = new Vector2(respawnPositions, transform.position.y);
            isRespawning = false;

        }

    }    
    void SpecialEnemyRespawn()
    {
        // Respawn the special enemy at the original position creating a new instance of the special enemy prefab
        specialEnemyInstance = Instantiate(specialEnemy, new Vector2(respawnPositions, transform.position.y), Quaternion.identity);
        Debug.Log("Special Enemy Respawned at" + specialEnemyTimer);
        isRespawning = false;
      
    }


}