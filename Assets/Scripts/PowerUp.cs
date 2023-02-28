using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //GameObject move speed
    float _speed = 2f;
    float _speedToPlayer = 5f;
    //Could do a renderer or zone or despawn timer.
    Renderer p_Renderer;

    //PowerUp id 0-2
    [SerializeField]
    int powerUpType;
    private Player _player;
    void Start()
    {
        //  p_Renderer = GetComponent<Renderer>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
        powerUpMovement();
 
 
    }

    private void powerUpMovement()
    {
        Vector3 p_position = _player.GetPlayerPosistion();
        Vector3 direction;


        if (Input.GetKey(KeyCode.C))
        {
            transform.position = Vector2.MoveTowards(transform.position, p_position, _speedToPlayer * Time.deltaTime);



        }
        else
        {
             direction =new Vector3(0, -_speed, 0);
            transform.Translate(direction * _speed * Time.deltaTime);

        }
         
    }
    public void printPowerUpType(int typeCode)
    {
        switch (powerUpType)
        {

            case 0:
                Debug.Log("Triple Shot");
                break;
            case 1:
                Debug.Log("Shield");
                break;
            case 2:
                Debug.Log("Speed");
                break;
            case 3:
                Debug.Log("Burst Shot");
                break;
            case 4:
                Debug.Log("Negative Bomb");
                break;
            case 5:
                Debug.Log("Slow Powerup");
                break;
            default:
                Debug.Log("Unknown PowerUp");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            Player player = other.transform.GetComponent<Player>();
             
            if (player != null)
            {            
                GameObject.Find("Player").GetComponent<Player>().SetPowerUp(true, powerUpType);

            }
            Destroy(this.gameObject);
        }
        else if(other.tag == "E_Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
         
         
    }
 }
