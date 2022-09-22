using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    float _speed = 2f;
    Renderer enemyRenderer;
    private Animator _eAnimator;
    private Player _player;
    private bool _enemyHit=false;

    //Enemy fire
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
 
    //projectiles prefabs
    public GameObject e_lazerPrefab;
  

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _eAnimator = GetComponent<Animator>();
      
        if (_eAnimator==null)
        {
            Debug.Log("Animator is null on Enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0, -_speed, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        if (!enemyRenderer.isVisible)
        {
            Destroy(this.gameObject);
        }
    }


    private void PlayerShoot()
    {
        if (Time.time > _canFire)
        {
           // PlaySound(0);

             
                Instantiate(e_lazerPrefab, transform.position + new Vector3(0, -0.8f, 0), Quaternion.identity);
            
            _canFire = Time.time + _fireRate;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!_enemyHit )
        {
            //Explosion sound
             
            if (other.tag == "Player")
            {
                //prevents double hit
                GameObject.Find("Audio SFX").GetComponent<Game_SFX_Manger>().ChangeSFX(1);
                _enemyHit = true;
                _player.Damage();
                if (_player.GetPlayerLife() == 0)
                {

                    _eAnimator.SetTrigger("Enemy_Dead");
                    Destroy(other.gameObject);
                    GameObject.Find("Spawn_Manger").GetComponent<Spawn_Manger>().SetIsEnemySpawnActive(false);
                    GameObject.Find("Canvas").GetComponent<UI_Manger>().startGameOver();
                }
                else
                {
                    _speed = 0;
                    _eAnimator.SetTrigger("Enemy_Dead");
                }
            }
            if (other.tag == "Lazer")
            {
                //If Laser hits a enemy destory both
                //Will need Score increment on this.
                //prevents double hit
                _player.PlaySound(1);
                _enemyHit = true;
                Destroy(other.gameObject);
                _speed = 0;
                _eAnimator.SetTrigger("Enemy_Dead");
                _player.UpdateScore(10);
            }
        }
    }
}
