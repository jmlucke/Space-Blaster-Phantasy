using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    // Start is called before the first frame update\
    //Projectile speed
    private float _speed = 2f;
    private bool _playerHit = false;
    private Player _player;
    Renderer laserRenderer;
    [SerializeField]
    private Vector3 _direction;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        laserRenderer = GetComponent<Renderer>();
        if (_direction == null)
        {
            _direction = new Vector3(0, _speed, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(_direction * _speed * Time.deltaTime);
        if (!laserRenderer.isVisible)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player" && this.tag == "E_Laser" && !_playerHit)
        {
            //prevents double hit
 
            _playerHit = true;
            _player.Damage();
            if (_player.GetPlayerLife() == 0)
            {


                Destroy(other.gameObject);
                GameObject.Find("Spawn_Manger").GetComponent<Spawn_Manger>().SetIsEnemySpawnActive(false);
                GameObject.Find("Canvas").GetComponent<UI_Manger>().startGameOver();
                Destroy(this.gameObject);
            }
            else
            {
               // _speed = 0;
              //  Destroy(this.gameObject);
            }
        }

    }

}
