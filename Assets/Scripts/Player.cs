using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //
    Camera cam;
    Renderer m_Renderer;
    //left right movement speed
    
    float _speed=7f;

    //Up down movement speed

    //Lazer settings
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    Vector3 frameOrigin = new Vector3(0, 1, 0);
    //somthing to count player extra lives
    [SerializeField]
     int player_lives = 2;
    public GameObject lazerPrefab;
    bool isWrappingX = false;
    bool isWrappingY = false;
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
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            Instantiate(lazerPrefab,transform.position + new Vector3(0,0.8f,0), Quaternion.identity);
        }
    }


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

    public void UpdatePlayerLife(int number)
    {
        player_lives += number;
    }
    public int GetPlayerLife()
    {
        return player_lives;
    }
    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // direction is a composite of vertical and horizontal
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
         transform.Translate( direction * _speed * Time.deltaTime);
    }
}
