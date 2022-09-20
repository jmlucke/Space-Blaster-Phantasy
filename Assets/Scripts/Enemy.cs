using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    float _speed = 2f;
    Renderer e_Renderer;
    private Animator e_Animator;
    private Player _player;
    private bool _enemy_hit=false;
    void Start()
    {
        e_Renderer = GetComponent<Renderer>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        e_Animator = GetComponent<Animator>();
      
        if (e_Animator==null)
        {
            Debug.Log("Animator is null on Enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0, -_speed, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        if (!e_Renderer.isVisible)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!_enemy_hit )
        {
            //Explosion sound
             
            if (other.tag == "Player")
            {
                //prevents double hit
                _player.PlaySound(1);
                _enemy_hit = true;
                _player.Damage();
                if (_player.GetPlayerLife() == 0)
                {

                    e_Animator.SetTrigger("Enemy_Dead");
                    Destroy(other.gameObject);
                    GameObject.Find("Spawn_Manger").GetComponent<Spawn_Manger>().SetIsEnemySpawnActive(false);
                    GameObject.Find("Canvas").GetComponent<UI_Manger>().startGameOver();
                }
                else
                {
                    _speed = 0;
                    e_Animator.SetTrigger("Enemy_Dead");
                }
            }
            if (other.tag == "Lazer")
            {
                //If Laser hits a enemy destory both
                //Will need Score increment on this.
                //prevents double hit
                _player.PlaySound(1);
                _enemy_hit = true;
                Destroy(other.gameObject);
                _speed = 0;
                e_Animator.SetTrigger("Enemy_Dead");
                _player.UpdateScore(10);
            }
        }
    }
}
