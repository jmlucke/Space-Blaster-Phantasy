using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manger : MonoBehaviour
{
    // Script spawns objects
    //In this game it is the various enimies.
    public GameObject enemyPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(enemyPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
    }
}
