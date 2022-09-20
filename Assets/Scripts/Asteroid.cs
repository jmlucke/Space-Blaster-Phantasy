using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Animator a_Animator;
    private float _rotateSpeed = 10f;
    void Start()
    {
        a_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * _rotateSpeed);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Lazer")
        {
            Destroy(other.gameObject);
        }
        GameObject.Find("Canvas").GetComponent<UI_Manger>().startWave(1);
        a_Animator.SetTrigger("Explode_Astro");
        GameObject.Find("Post Process Volume").GetComponent<PostProcessing>().SetChangeBloom(true);
        GameObject.Find("Audio Manger").GetComponent<Game_Audio_Manger>().ChangeMusic(2);
    }
}
