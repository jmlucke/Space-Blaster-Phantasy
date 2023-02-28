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
    private GameObject _targetPlayer;
    private bool _enemyHit=false;

    //Enemy fire
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;

    private int _moveType = 0;
    [SerializeField]
    private int _damageHP = 1;
    //projectiles prefabs
    public GameObject e_laserPrefab;

    [SerializeField]
    private int _enemyID=0;
    private bool _beamActive = false;
    private float _direction = 0.8f;
    public GameObject shieldVisual;
    public GameObject eBeam;
    private int _shieldHitCount = 0;
    private bool _spawnWithSheild = false;
    //shiled _colors
    private Color _noDmgShieldColor = new Color(255, 182, 0, 183);
    private Color _minorDmgShieldColor = new Color(255, 183, 23);
    private Color _majorDmgShieldColor = new Color(255, 29, 23);
    private bool _dodgeActive=false;
    private float _dodgeDirection = -1;
    private float _dodgeMoveTime = .5f;
    private float _canDodge = -1f;
    private bool _canShoot = true;
    private bool _zoneShoot = false;
    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _eAnimator = GetComponent<Animator>();
        _targetPlayer = GameObject.Find("Player");
        // Shield(_spawnWithSheild);
        if (_eAnimator==null)
        {
            Debug.Log("Animator is null on Enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            EnemyMovement(_moveType);
            if(_canShoot)EnemyShoot();
            if (!enemyRenderer.isVisible)
            {
                Destroy(this.gameObject);
            }
        }
    
 
    }
    public void SetEnemyMovementType(int number)
    {
        _moveType = number;
        if (_enemyID == 2) _moveType = 3;
    }

    public void EnemyDodge()
    {
        _dodgeActive = true;
        _canDodge = Time.time + _dodgeMoveTime;

    }

    private void EnemyMovement(int moveType)
    {

        switch (_moveType)
        {
            case 0:
                Vector3 direction = new Vector3(0, -_speed, 0);
                if (_dodgeActive)
                {
                     direction = new Vector3(_dodgeDirection* _speed, 0, 0);
                    if(Time.time > _canDodge) _dodgeActive = false;

                }
                 
                 
                transform.Translate(direction * _speed * Time.deltaTime);
                break;
            case 1:
                Vector3 direction1 = new Vector3(_speed, -_speed, 0);
                transform.Translate(direction1 * _speed * Time.deltaTime);
                break;
            case 2:
                Vector3 direction2 = new Vector3(-_speed, -_speed, 0);
                transform.Translate(direction2 * _speed * Time.deltaTime);
                break;
            case 3:
               
                Vector2 direction3 = _targetPlayer.transform.position - transform.position;
                transform.rotation = Quaternion.FromToRotation(Vector3.up, -direction3);
                // transform.LookAt(_targetPlayer.transform);
                transform.position = Vector2.MoveTowards(transform.position, _targetPlayer.transform.position, (_speed * 1) * Time.deltaTime);
                break;
            default:
                Debug.Log("Move Type not defined");
                break;
        }
         

    }

    public void ZoneShoot()
    {
       
 
          _zoneShoot = true;
       

    }

    private void EnemyShoot()
    {


        Vector3 e_position = this.transform.position;
         Vector3 p_position =  _player.GetPlayerPosistion();

          float eY = e_position.y;
          float eX_High = e_position.x + 2;
          float eX_Low = e_position.x - 2;
          float pY = p_position.y;
          float pX = p_position.x;
           int shoot_adjust = -2;
         


           if(_enemyID == 0 && _zoneShoot && Time.time > _canFire )
        {
            
            if (_canFire > 0) Instantiate(e_laserPrefab, transform.position + new Vector3(0, _direction + shoot_adjust, 0), Quaternion.identity);


            _canFire = Time.time + _fireRate;
            _zoneShoot = false;

        }
            else if (_enemyID == 0 && Time.time > _canFire && (Mathf.Abs(pX- e_position.x)< 2  ))
            {
            // PlaySound(0);
                    if (eY < pY && (eX_Low < pX && eX_High > pX))
                    {

                        _direction = _direction * -1;
                        shoot_adjust = 0;
                    }
            if (_canFire > 0) Instantiate(e_laserPrefab, transform.position + new Vector3(0, _direction + shoot_adjust, 0), Quaternion.identity);
            _canFire = Time.time + _fireRate;
        }
            else if (_enemyID == 1 && !_beamActive)
            {
          
              //  Instantiate(e_laserPrefab, transform.position + new Vector3(0, _direction, 0), Quaternion.identity);
                _beamActive = true;
                 eBeam.SetActive(true);
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
                    _canShoot = false;
                    _eAnimator.SetTrigger("Enemy_Dead");
                }
            }
            if (other.tag == "Laser")
            {
                //If Laser hits a enemy destory both
                //Will need Score increment on this.
                //prevents double hit
                _damageHP--;
                    if (_damageHP<1)
                {
                    _player.PlaySound(1);
                    _enemyHit = true;
                    Destroy(other.gameObject);
                    _speed = 0;
                    _canShoot = false;
                    _eAnimator.SetTrigger("Enemy_Dead");
                  if(!GameValues.boss)  _player.UpdateScore(10);


                }
 
            }
        }
 
    }

    public void Shield(bool state)
    {

        shieldVisual.SetActive(state);
    }
    public void IncrementShieldHitCount(int numHits)
    {

        _shieldHitCount += numHits;

    }
    public void SetShieldColor()
    {
        // Debug.Log("Shield Hit Change color " + _shieldHitCount);

        switch (_shieldHitCount)
        {
            case 0:

                //GameObject.Find("Shield").GetComponent<SpriteRenderer>().material.color = _noDmgShieldColor;
                // play a sound effect for shield burst
                //PlaySound(4);

                break;
            case 1:
                //PlaySound(3);
                GameObject.Find("Enemy_Shield").GetComponent<SpriteRenderer>().material.color = _majorDmgShieldColor;
                break;
            case 2:
               // PlaySound(3);
                GameObject.Find("Enemy_Shield").GetComponent<SpriteRenderer>().material.color = _minorDmgShieldColor;
                break;
            case 3:
                GameObject.Find("Enemy_Shield").GetComponent<SpriteRenderer>().material.color = _noDmgShieldColor;
                break;
            default:

                break;
        }
    }
}
