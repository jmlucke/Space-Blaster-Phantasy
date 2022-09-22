using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    // Start is called before the first frame update\
    //Projectile speed
    private float _speed = 3.5f;
    Renderer lazerRenderer;
     
    void Start()
    {
        lazerRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0, _speed, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        if(!lazerRenderer.isVisible)
       {
            if(transform.parent!=null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
             
        }
    }
}
