using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manger : MonoBehaviour
{
     
    [SerializeField]
    private bool isEnemySpawnActive = true;
    //Enememy
    public GameObject enemyPrefab;
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
            yield return new WaitForSeconds(spawnRate);
        }
  
         
         
    }

    public void SetIsEnemySpawnActive(bool spawn)
    {
        isEnemySpawnActive = spawn;
    }
}
