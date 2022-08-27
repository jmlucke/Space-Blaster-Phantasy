using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manger : MonoBehaviour
{
     
    [SerializeField]
    private bool isEnemySpawnActive = true;
    //Enememy
    public GameObject enemyPrefab;
    public GameObject lazer_powerUp;
    public GameObject shield_powerUp;
    public GameObject speed_powerUp;
    //two and half second delay. Could be longer and vary in the future.
    public float spawnRate = 2.5f;
     

    void Start()
    {
        StartCoroutine(SpawnEnemy());
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
             
            if(Random.Range(0,4)==1)
            {
                int powerUpType = Random.Range(0, 3);
                switch (powerUpType)
                {

                    case 0:
                        Instantiate(lazer_powerUp, transform.position + new Vector3(Random.Range(-9f, 9f), 6, 0), Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(shield_powerUp, transform.position + new Vector3(Random.Range(-9f, 9f), 6, 0), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(speed_powerUp, transform.position + new Vector3(Random.Range(-9f, 9f), 6, 0), Quaternion.identity);
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
    }
}
