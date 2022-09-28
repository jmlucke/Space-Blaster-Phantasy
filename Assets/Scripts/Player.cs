using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //camera used to test off screen location
    Camera cam;
    Renderer mainRenderer;
    //screewrap bools
    bool isWrappingX = false;
    bool isWrappingY = false;

    //movement speed
    [SerializeField]
    private float _speed=5f;
    [SerializeField]
    private float _thrust = 3f;
    [SerializeField]
    private float _speedBoost = 10f;
    //PowerUps
   // [SerializeField]
    private bool _tripleShotIsActive = false;
    private bool _burstShotIsActive = false;
    //shield
    private bool _shieldIsActive = false;
    private int _shieldHitCount=0;
    //speed boost
    private bool _speedBoostIsActive = false;

    //Score related
    private int _score = 0;
    //somthing to count player extra lives
    [SerializeField]
     int playerLives = 3;
    //Shield
    public GameObject shieldVisual;
    //Lazer settings
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    private float _powerUpRate = 5f;
    //ammo
    [SerializeField]
    private int _ammoAmt=15;
    private bool _hasAmmo = true;
    //projectiles prefabs
    public GameObject lazerPrefab;
    public GameObject tripleShotPrefab;
    public GameObject burstShotPrefab;
    //particle effect
    private ParticleSystem _thrustEffect;
    private GameObject _shieldObject;
    private SpriteRenderer _shieldRenderer;
    //Colors for shield
    private Color _noDmgShieldColor = new Color(255, 182,0,183);
    private Color _minorDmgShieldColor = new Color(255, 183, 23);
    private Color _majorDmgShieldColor= new Color(255, 29, 23);
    private UI_Manger _UIManger;

    void Start()
    {
        //take a posistion and zero in out x,y,z
        cam = Camera.main;
        mainRenderer = GetComponent<Renderer>();
        _thrustEffect = GameObject.Find("Thruster Particle System").GetComponent<ParticleSystem>();
        _UIManger = GameObject.Find("Canvas").GetComponent<UI_Manger>();
        //add null checking

        //

    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Movement();
        ScreenWrap();
        PlayerShoot();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }
    //Player Audio
    public void PlaySound(int soundId)
    {
        if (this.gameObject == null) return;

        GameObject.Find("Audio SFX").GetComponent<Game_SFX_Manger>().ChangeSFX(soundId);
 
    }

    //Controls players firing capabilities
    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {

            if (_hasAmmo)
            {
                PlaySound(0);
                _UIManger.UpdateAmmo(_ammoAmt);
                _ammoAmt--;
                if (_ammoAmt == 0) _hasAmmo = false;
                if (_tripleShotIsActive)
                {
                    Instantiate(tripleShotPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

                    //Need to destory empty PreFab 
                }
                else if(_burstShotIsActive)
                {
                    Instantiate(burstShotPrefab, transform.position + new Vector3(0, -0.2f, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(lazerPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                }
                _canFire = Time.time + _fireRate;

            }
            else
            {
                PlaySound(3);
            }
                 

        }
    
    }
    //Controls screen wrap
    //Potential future issue with enemy location id
    void ScreenWrap()
    {
        //established up above to test if player is visible with a renderer
        var isVisible = mainRenderer.isVisible;

        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }

        if (isWrappingX && isWrappingY)
        {
            return;
        }

        var screenPos = cam.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;

        if (!isWrappingX && (screenPos.x > 1 || screenPos.x < 0))
        {
            newPosition.x = -newPosition.x;
            //set wrap for x
            isWrappingX = true;
        }

        if (!isWrappingY && (screenPos.y > 1 || screenPos.y < 0))
        {
            newPosition.y = -newPosition.y;
            //set wrap for y
            isWrappingY = true;
        }
        
        transform.position = newPosition;
    }

    void Thrust()
    {
         
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //thrust
            _thrust = 3;
            //address lagged response.
            if(!_thrustEffect.isPlaying) _thrustEffect.Play();
        }
        else
        {
            //not thrust
            _thrust = 0;
            if (_thrustEffect.isPlaying) _thrustEffect.Stop();
        }

    }

    //Controls Player Movement
    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // direction is a composite of vertical and horizontal
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
 
        if(_speedBoostIsActive)
        {
            transform.Translate(direction * (_speedBoost + _thrust) * Time.deltaTime);
            PowerUpTimer(2);
        }
        else
        {
            transform.Translate(direction * (_speed + _thrust) * Time.deltaTime);
        }
          
    }

    void Shield(bool state)
    {

        shieldVisual.SetActive(state);
    }
    public void IncrementShieldHitCount(int numHits)
    {

        _shieldHitCount += numHits;
         
    }
    public void SetShieldColor()
    {
        Debug.Log("Shield Hit Change color " + _shieldHitCount);

        switch(_shieldHitCount)
        {
            case 0:
                 
                //GameObject.Find("Shield").GetComponent<SpriteRenderer>().material.color = _noDmgShieldColor;
                // play a sound effect for shield burst
                PlaySound(4);

                break;
            case 1:
                PlaySound(3);
                GameObject.Find("Shield").GetComponent<SpriteRenderer>().material.color = _majorDmgShieldColor;
                break;
            case 2:
                PlaySound(3);
                GameObject.Find("Shield").GetComponent<SpriteRenderer>().material.color = _minorDmgShieldColor;
                break;
            case 3:
                GameObject.Find("Shield").GetComponent<SpriteRenderer>().material.color = _noDmgShieldColor;
                break;
            default:
                 
                break;
        }
    }
    //Getter and Setter Functions
    public void UpdatePlayerLife(int number)
    {
        playerLives += number;
     //   Debug.Log(playerLives);
        if(!_shieldIsActive)
        {
            _UIManger.UpdateLives(playerLives);
        }
    }
    public int GetPlayerLife()
    {
        return playerLives;
    }

    //Could be extraplated to work for anypowerup
    public void SetPowerUp(bool active, int powerUpType)
    {
        //change to switch statement later
        if(active)PlaySound(2);
        switch (powerUpType)
        {
            case 0:
                _tripleShotIsActive = active;
                StartCoroutine(PowerUpTimer(powerUpType));
                break;
            case 1:
                _shieldIsActive = active;
                Shield(active);
                _shieldHitCount = 3;
                SetShieldColor();
                 
               // if (_shieldIsActive==true) UpdatePlayerLife(1);

                //StartCoroutine(PowerUpTimer(powerUpType));
                break;
            case 2:
                _speedBoostIsActive = active;
                StartCoroutine(PowerUpTimer(powerUpType));
                break;
            case 3:
                _burstShotIsActive = active;
                StartCoroutine(PowerUpTimer(powerUpType));
                break;

                break;
            default:
                break; 

        }
         
    }
    public void SetCollectible(int collectibleType)
    {
        PlaySound(2);
        switch (collectibleType)
        {
            case 0:
                //ammo
                _hasAmmo = true;
                _ammoAmt = 15;
                  _UIManger.ReloadAmmo();
                //call reload ammo function.
                break;
            case 1:
                //playerLives = 3;
                if(playerLives!=3)
                {

                    UpdatePlayerLife(1);
                    _UIManger.UpdateLives(playerLives);
                }
                //repair
                 
                //set ship damage up a level
                break;
            default:
                break;

        }

    }

    //Wait Functions
    IEnumerator PowerUpTimer(int powerUpType)
    {
        //Waits a certain amount of 
        
        yield return new WaitForSeconds(_powerUpRate);

        switch (powerUpType)
        {
            case 0:
                 
                _tripleShotIsActive = false;
                break;
            case 1:
               // _shieldIsActive = false;
                break;
            case 2:
                _speedBoostIsActive = false;
                break;
            case 3:
                _burstShotIsActive = false;
                break;
            default:
                break;

        }

    }

    //Score related and UI
    public void UpdateScore(int num)
    {
        _score += num;
       // Debug.Log("Test");
        GameObject.Find("Canvas").GetComponent<UI_Manger>().SetScore(_score.ToString());
    }
    //Damage
    public void Damage()
    {
        if(_shieldIsActive)
        {
            //Shield id 1
             
            IncrementShieldHitCount(-1);
            SetShieldColor();
            if (_shieldHitCount == 0)
            {
                //after 3 hits deactivate the shield
                SetPowerUp(false, 1);
            }
             
            return;

        }

        UpdatePlayerLife(-1);
    }

}
