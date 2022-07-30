using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    // Start is called before the first frame update\
    //Projectile speed
    float _speed = 5.0f;
    Renderer l_Renderer;
     
    void Start()
    {
        l_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0, _speed, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        if(!l_Renderer.isVisible)
        {
            Destroy(this.gameObject);
        }
    }
}
