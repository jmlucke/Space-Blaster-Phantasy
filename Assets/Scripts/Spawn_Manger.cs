using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manger : MonoBehaviour
{
     
   
    private bool _isEnemySpawnActive = false;
    //Enememy
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public GameObject laserPowerUp;
    public GameObject shieldPowerUp;
    public GameObject speedPowerUp;
    public GameObject burstShotPowerUp;
    public GameObject slownegaPowerup;
    //collectibles
    public GameObject ammoCollectible;
    public GameObject healthCollectile;
    private int _commonSpawnCount = 0;
    //two and half second delay. Could be longer and vary in the future.

    public float spawnRate = 2.5f;
     

    void Start()
    {
        //StartCoroutine(SpawnEnemy());
    }

     
    void Update()
    {

     }

    IEnumerator SpawnEnemy()
    {
        //Waits a certain amount of time and the will spawn a new enemy.
        float randomLocation = Random.Range(-9f, 9f);
        //_isEnemySpawnActive
        while (1==2)
        {
             
            int mainRandomSpawner = Random.Range(0, 10);
            
            if(mainRandomSpawner<5)
            {
                
                //To determine spawn location needs to be set Y but variable X.
                //Currently random but could be a pattern.
                //Could use a counter to make at least c common enemies spawn before a unique does.
                GameObject spawnedEnemy;
                int enemyRandomSpawner = 10;

                if (enemyRandomSpawner < 2 &&  _commonSpawnCount>5)
                {
                    //calling different enemy prefab
                     spawnedEnemy = Instantiate(enemyPrefab2, transform.position + new Vector3(randomLocation, 6, 0), Quaternion.identity) as GameObject;
                    _commonSpawnCount = 0;
                }
                else if(enemyRandomSpawner < 4 && _commonSpawnCount > 5)
                {
                    spawnedEnemy = Instantiate(enemyPrefab, transform.position + new Vector3(randomLocation, 6, 0), Quaternion.identity) as GameObject;
                    _commonSpawnCount++;
                    spawnedEnemy.GetComponent<Enemy>().Shield(true);
                    _commonSpawnCount = 0;
                }
                else if (enemyRandomSpawner < 6 && _commonSpawnCount > 5)
                {
                    spawnedEnemy = Instantiate(enemyPrefab3, transform.position + new Vector3(randomLocation, 6, 0), Quaternion.identity) as GameObject;
                    _commonSpawnCount++;
 
                    _commonSpawnCount = 0;
                }
                else
                {
                    Debug.Log("New e");
                    spawnedEnemy = Instantiate(enemyPrefab, transform.position + new Vector3(randomLocation, 6, 0), Quaternion.identity) as GameObject;
                    _commonSpawnCount++;

                }
                if (randomLocation < -8)
                {
                    spawnedEnemy.GetComponent<Enemy>().SetEnemyMovementType(1);
                }
                else if (randomLocation > 8)
                {
                    spawnedEnemy.GetComponent<Enemy>().SetEnemyMovementType(2);
                }
                else
                {
                    spawnedEnemy.GetComponent<Enemy>().SetEnemyMovementType(0);
                }

            }
            else
            {
                 
                randomLocation= Random.Range(-9f, 9f);
               
                //power up spawning
                if (Random.Range(0, 2) < 1)
                {
                    int powerUpType = Random.Range(0, 3);
                    int rarePowerUpType = Random.Range(0, 2);
                    switch (powerUpType)
                    {
                        //triple shot, speed, shield 8%
                        //northing, burst 4%
                        //28% approx chance overall for a powerup
                        case 0:
                            Instantiate(laserPowerUp, transform.position + new Vector3(randomLocation, 6, 0), Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(shieldPowerUp, transform.position + new Vector3(randomLocation, 6, 0), Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(speedPowerUp, transform.position + new Vector3(randomLocation, 6, 0), Quaternion.identity);
                            break;
                        case 3:
                            if (rarePowerUpType > 0)
                            {
                                Instantiate(burstShotPowerUp, transform.position + new Vector3(randomLocation, 6, 0), Quaternion.identity);
                            }
                            break;
                        case 4:
                            Instantiate(slownegaPowerup, transform.position + new Vector3(randomLocation, 6, 0), Quaternion.identity);
                            break;
                        case 5:
                            Instantiate(slownegaPowerup, transform.position + new Vector3(randomLocation, 6, 0), Quaternion.identity);
                            break;
                        default:
                            // code block
                            break;
                    }
                }
                if (Random.Range(0, 2) == 1)
                {
                    int collectibleType = Random.Range(0, 2);

                    switch (collectibleType)
                    {

                        case 0:

                            Instantiate(ammoCollectible, transform.position + new Vector3(Random.Range(-9f, 9f), 6, 0), Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(healthCollectile, transform.position + new Vector3(Random.Range(-9f, 9f), 6, 0), Quaternion.identity);
                            break;
                        default:
                            // code block
                            break;
                    }
                }
            }
 

 
                        yield return new WaitForSeconds(spawnRate);
        }
  
         
         
    }

    public void SetIsEnemySpawnActive(bool spawn)
    {
        _isEnemySpawnActive = spawn;
        if(_isEnemySpawnActive)
        {
            StartCoroutine(SpawnEnemy());
        }
    }
}
