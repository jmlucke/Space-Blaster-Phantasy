using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //camera used to test off screen location
    Camera cam;
    Renderer m_Renderer;
    //screewrap bools
    bool isWrappingX = false;
    bool isWrappingY = false;

    //movement speed
    float _speed=7f;
    float _speed_boost = 10f;
    //PowerUps
   // [SerializeField]
    private bool _tripleShotIsActive = false;
    private bool _shieldIsActive = false;
    private bool _speedBoostIsActive = false;

    //Score related
    private int _score = 0;
    //somthing to count player extra lives
    [SerializeField]
     int player_lives = 2;
    //Shield
    public GameObject shieldVisual;
    //Lazer settings
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    private float _powerUpRate = 5f;
    //projectiles prefabs
    public GameObject lazerPrefab;
    public GameObject tripleShotPrefab;
    private AudioSource _audioSource;
    //Audio CLips
    [SerializeField]
    private AudioClip _lazerSound;
    [SerializeField]
    private AudioClip _explosionSound;
    [SerializeField]
    private AudioClip _powerUpSound;
    //AudioSource source = gameObject.GetComponent<AudioSource>();
     

    void Start()
    {
        //take a posistion and zero in out x,y,z
        cam = Camera.main;
        m_Renderer = GetComponent<Renderer>();
        if (this.gameObject != null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
            
        //add null checking

    }

    // Update is called once per frame
    void Update()
    {
      
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
        switch (soundId)
        {
            case 0:
                _audioSource.clip = _lazerSound;
                _audioSource.Play();
                break;
            case 1:
                _audioSource.clip = _explosionSound;
                _audioSource.Play();
                break;
            case 2:
                _audioSource.clip = _powerUpSound;
                _audioSource.Play();
                break;
            default:
                Debug.Log("No sound selected");
                break;

        }
    }

    //Controls players firing capabilities
    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            PlaySound(0);
            if (_tripleShotIsActive)
            {
                Instantiate(tripleShotPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                 
                //Need to destory empty PreFab 
            }
            else
            {
                Instantiate(lazerPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;

        }
    }
    //Controls screen wrap
    //Potential future issue with enemy location id
    void ScreenWrap()
    {
        //established up above to test if player is visible with a renderer
        var isVisible = m_Renderer.isVisible;

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

    //Controls Player Movement
    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // direction is a composite of vertical and horizontal
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        if(_speedBoostIsActive)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
            PowerUpTimer(2);
        }
        else
        {
            transform.Translate(direction * _speed_boost * Time.deltaTime);
        }
          
    }

    void Shield(bool state)
    {

        shieldVisual.SetActive(state);
    }

    //Getter and Setter Functions
    public void UpdatePlayerLife(int number)
    {
        player_lives += number;
        Debug.Log(player_lives);
        if(!_shieldIsActive)
        {
            GameObject.Find("Canvas").GetComponent<UI_Manger>().UpdateLives(player_lives);
        }
    }
    public int GetPlayerLife()
    {
        return player_lives;
    }

    //Could be extraplated to work for anypowerup
    public void SetPowerUp(bool active, int powerUpType)
    {
        //change to switch statement later
        PlaySound(2);
        switch (powerUpType)
        {
            case 0:
                _tripleShotIsActive = active;
                StartCoroutine(PowerUpTimer(powerUpType));
                break;
            case 1:
                _shieldIsActive = active;
                Shield(active);
               // if (_shieldIsActive==true) UpdatePlayerLife(1);

                //StartCoroutine(PowerUpTimer(powerUpType));
                break;
            case 2:
                _speedBoostIsActive = active;
                StartCoroutine(PowerUpTimer(powerUpType));
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
        //could be switch
 

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
            SetPowerUp(false,1);
            return;

        }

        UpdatePlayerLife(-1);
    }

}
