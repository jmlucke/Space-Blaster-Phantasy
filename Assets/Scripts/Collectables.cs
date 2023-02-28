using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    //This script is similar to PowerUp script but
    //has been broken out incase there is a need to seperate the two.
    float _speed = 1.5f;
    float _speedToPlayer=5.0f;
    //Could do a renderer or zone or despawn timer.


    //PowerUp id 0-2
    [SerializeField]
    int collectibleType;
    private Player _player;
    void Start()
    {
        //  p_Renderer = GetComponent<Renderer>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    // Update is called once per frame

    void Update()
    {
        collectibleMovement();



    }


    private void collectibleMovement()
    {
        Vector3 p_position = _player.GetPlayerPosistion();
        Vector3 direction;


        if (Input.GetKey(KeyCode.C))
        {
            transform.position = Vector2.MoveTowards(transform.position, p_position, _speedToPlayer * Time.deltaTime);



        }
        else
        {
            direction = new Vector3(0, -_speed, 0);
            transform.Translate(direction * _speed * Time.deltaTime);

        }

    }

    public void printCollectibleType(int typeCode)
    {
        switch (collectibleType)
        {

            case 0:
                Debug.Log("Ammo");
                break;
            case 1:
                Debug.Log("Ship Repair");
                break;
            case 2:
                //Might not make sense
                Debug.Log("Extra Life");
                break;
            default:
                Debug.Log("Unknown Collected");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                GameObject.Find("Player").GetComponent<Player>().SetCollectible(collectibleType);

            }
        }

        Destroy(this.gameObject);
    }
}
