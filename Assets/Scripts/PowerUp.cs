using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    //Defined on Object Spawn
    [SerializeField]
    string powerUpType;
    void Start()
    {
        //testing triple shot
        powerUpType = "TripleShot";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (powerUpType)
                {

                    case "TripleShot":
                        GameObject.Find("Player").GetComponent<Player>().SetPowerUp(true, powerUpType);
                        break;
                    case "Shield":
                        // code block
                        break;
                    default:
                        // code block
                        break;
                }


            }
             


        }
         
        Destroy(this.gameObject);
    }
 }
