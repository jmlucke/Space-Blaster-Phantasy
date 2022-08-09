using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manger : MonoBehaviour
{
    // Script spawns objects
    [SerializeField]
    private bool isEnemySpawnActive = true;
    //In this game it is the various enimies.
    public GameObject enemyPrefab;
    public float spawnRate = 3f;

     
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {

     }

    IEnumerator SpawnEnemy()
    {
        //Waits a certain amount of time and the will spawn a new enemy.
        while (isEnemySpawnActive)
        {
            //To determine spawn location needs to be set Y but variable X.

            Instantiate(enemyPrefab, transform.position + new Vector3(Random.Range(-9f, 9f),6, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
        //do stuff
         
         
    }
}
