using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manger : MonoBehaviour
{
     
   
    private bool isEnemySpawnActive = false;
    //Enememy
    public GameObject enemyPrefab;
    public GameObject lazerPowerUp;
    public GameObject shieldPowerUp;
    public GameObject speedPowerUp;
    public GameObject burstShotPowerUp;
    //collectibles
    public GameObject ammo_collectible;
    public GameObject health_collectile;
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
        while (isEnemySpawnActive)
        {
            //To determine spawn location needs to be set Y but variable X.
            //Currently random but could be a pattern.
            Instantiate(enemyPrefab, transform.position + new Vector3(Random.Range(-9f, 9f),6, 0), Quaternion.identity);
             
            if(Random.Range(0,3)>1)
            {
                int powerUpType = Random.Range(0, 4);
                int rarePowerUpType= Random.Range(0, 2);
                switch (powerUpType)
                {
                    //triple shot, speed, shield 8%
                    //northing, burst 4%
                    //28% approx chance overall for a powerup
                    case 0:
                        Instantiate(lazerPowerUp, transform.position + new Vector3(Random.Range(-9f, 9f), 6, 0), Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(shieldPowerUp, transform.position + new Vector3(Random.Range(-9f, 9f), 6, 0), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(speedPowerUp, transform.position + new Vector3(Random.Range(-9f, 9f), 6, 0), Quaternion.identity);
                        break;
                    case 3:
                        if (rarePowerUpType> 0)
                            {
                            Instantiate(burstShotPowerUp, transform.position + new Vector3(Random.Range(-9f, 9f), 6, 0), Quaternion.identity);
                            }  
                        break;
                    default:
                        // code block
                        break;
                }
            }
            if (Random.Range(0, 3) == 1)
            {
                int collectibleType = Random.Range(0, 2);

                switch (collectibleType)
                {

                    case 0:

                        Instantiate(ammo_collectible, transform.position + new Vector3(Random.Range(-9f, 9f), 6, 0), Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(health_collectile, transform.position + new Vector3(Random.Range(-9f, 9f), 6, 0), Quaternion.identity);
                        break;
                    default:
                        // code block
                        break;
                }
            }
            yield return new WaitForSeconds(spawnRate);
        }
  
         
         
    }

    public void SetIsEnemySpawnActive(bool spawn)
    {
        isEnemySpawnActive = spawn;
        if(isEnemySpawnActive)
        {
            StartCoroutine(SpawnEnemy());
        }
    }
}
