using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Animator astroAnimator;
    private float _rotateSpeed = 10f;
    private bool _astroTriggered = false;
    void Start()
    {
        astroAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * _rotateSpeed);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!_astroTriggered)
        {  
            if (other.tag == "Lazer")
            {
                Destroy(other.gameObject);
            }
            GameObject.Find("Canvas").GetComponent<UI_Manger>().startWave(1);
            astroAnimator.SetTrigger("Explode_Astro");
            GameObject.Find("Post Process Volume").GetComponent<PostProcessing>().SetChangeBloom(true);
            GameObject.Find("Audio Manger").GetComponent<Game_Audio_Manger>().ChangeMusic(2);
            _astroTriggered = true;
        }
    }
}
