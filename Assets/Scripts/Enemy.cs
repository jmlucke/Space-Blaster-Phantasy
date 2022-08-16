using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    float _speed = 2f;
    Renderer e_Renderer;
    void Start()
    {
        e_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0, -_speed, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        if (!e_Renderer.isVisible)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Test output coll " + other.tag);
        if (other.tag=="Player")
        {
             
            //calls get and set functions to update player life counts and destory if less than 1
            if (1 > GameObject.Find("Player").GetComponent<Player>().GetPlayerLife())
            {
                Destroy(this.gameObject);
                Destroy(other.gameObject);
                //stop spawing enemies
                GameObject.Find("Spawn_Manger").GetComponent<Spawn_Manger>().SetIsEnemySpawnActive(false);
                //Also should trigger GameOver Screen.
            }
            else
            {
                Destroy(this.gameObject);
                GameObject.Find("Player").GetComponent<Player>().UpdatePlayerLife(-1);
            }



        }
        if(other.tag=="Lazer")
        {
            //If Laser hits a enemy destory both
            //Will need Score increment on this.
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
