using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Animator _astroAnimator;
    private float _rotateSpeed = 10f;
    private bool _astroTriggered = false;
  
    void Start()
    {
        _astroAnimator = GetComponent<Animator>();
        GameObject.Find("Post Process Volume").GetComponent<PostProcessing>().ChangeBloomColor();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * _rotateSpeed);
    }

 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!_astroTriggered)
        {
            Debug.Log(other.tag);
            if (other.tag == "Laser")
            {
                Destroy(other.gameObject);
            }
            if (GameValues.wave == 0) GameValues.wave = 1;
            GameObject.Find("Canvas").GetComponent<UI_Manger>().startWave(GameValues.wave);
            _astroAnimator.SetTrigger("Explode_Astro");
            GameObject.Find("Post Process Volume").GetComponent<PostProcessing>().SetChangeBloom(true);
            int waveAdjForArray = GameValues.wave + 1;
   
            GameObject.Find("Audio Manger").GetComponent<Game_Audio_Manger>().ChangeMusic(waveAdjForArray);
            _astroTriggered = true;
        }
    }
}
