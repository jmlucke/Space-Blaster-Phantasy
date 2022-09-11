using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //GameObject move speed
    float _speed = 2f;
    //Could do a renderer or zone or despawn timer.
    Renderer p_Renderer;

    //PowerUp id 0-2
    [SerializeField]
    int powerUpType;
    void Start()
    {
        p_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0, -_speed, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        if (!p_Renderer.isVisible)
        {
            Destroy(this.gameObject);
        }
        // printPowerUpType(powerUpType);
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

            }
        }
         
        Destroy(this.gameObject);
    }
 }
