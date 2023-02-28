using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ZoneDectection : MonoBehaviour
{
    private int _dodgeAmount = 1;
    void Start()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {


        if (_dodgeAmount > 0 && other.tag == "Laser")
        {
            //Dodge EnemyDodge()
            //  parentGameObject .GetComponent<EnemyDodge>()
            this.transform.parent.GetComponent<Enemy>().EnemyDodge();
            _dodgeAmount--;
        }

 

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PowerUp")
        {

            this.transform.parent.GetComponent<Enemy>().ZoneShoot();
        }
    }
}
