using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    //Defined on Object Spawn
    [SerializeField]
    int powerUpType;
    void Start()
    {
        //testing triple shot
        powerUpType = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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
              
                printPowerUpType(powerUpType);

            }
             


        }
         
        Destroy(this.gameObject);
    }
 }
