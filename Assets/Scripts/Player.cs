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
 
    //PowerUps
    private bool _tripleShotIsActive = false;
    private bool _shieldIsActive = false;
    private bool _speedBoostIsActive = false;
 
    //somthing to count player extra lives
    [SerializeField]
     int player_lives = 2;

    //Lazer settings
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    private float _powerUpRate = 5f;
    //projectiles prefabs
    public GameObject lazerPrefab;
    public GameObject tripleShotPrefab;
 
    void Start()
    {
        //take a posistion and zero in out x,y,z
        cam = Camera.main;
        m_Renderer = GetComponent<Renderer>();
        //transform.position = new Vector3(0, 0, 0);
  
    }

    // Update is called once per frame
    void Update()
    {
      
        Movement();
        ScreenWrap();
        PlayerShoot();
    }
    //Controls players firing capabilities
    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {

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
         transform.Translate( direction * _speed * Time.deltaTime);
    }

    //Getter and Setter Functions
    public void UpdatePlayerLife(int number)
    {
        player_lives += number;
    }
    public int GetPlayerLife()
    {
        return player_lives;
    }

    //Could be extraplated to work for anypowerup
    public void SetPowerUp(bool active, int powerUpType)
    {
        //change to switch statement later
 
        switch(powerUpType)
        {
            case 0:
                _tripleShotIsActive = active;
                break;
            case 1:
                _shieldIsActive = active;
                break;
            case 2:
                _speedBoostIsActive = active;
                break;
            default:
                break; 

        }
         
    }


    //Wait Functions
    IEnumerator PowerUpTimer(string powerUpType)
    {
        //Waits a certain amount of 
         
        yield return new WaitForSeconds(_powerUpRate);
        //could be switch
        if (powerUpType == "TripleShot")
        {
            _tripleShotIsActive = false;
        }

    }

}
