using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update\
    //Projectile speed
    [SerializeField]
    private float _speed = 3.5f;
    private bool _playerHit = false;
    private Player _player;
    Renderer lazerRenderer;
    [SerializeField]
    private Vector3 _direction;
 
    private GameObject _target;
    
    private bool _homingActive = false;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        lazerRenderer = GetComponent<Renderer>();
        if (_direction == null)
        {
            _direction = new Vector3(0, _speed, 0);
        }
        Vector3 e_position = this.transform.position;
        Vector3 p_position = _player.GetPlayerPosistion();

        float eY = e_position.y;
        float eX_High = e_position.x + 2;
        float eX_Low = e_position.x - 2;
        float pY = p_position.y;
        float pX = p_position.x;

 
        if(this.tag == "Laser")
            {
                if(Input.GetKey(KeyCode.H))
                {
                    _homingActive = true;
                _target = FindClosestEnemy();
                }
            }
        else if(eY < pY && (eX_Low < pX && eX_High > pX))
            {
                _direction = _direction * -1;
            }

    }

    // Update is called once per frame
    void Update()
    {

        if(_homingActive && _target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, (_speed*3) * Time.deltaTime);
        }
        else
        {
            transform.Translate(_direction * _speed * Time.deltaTime);
        }
         
        if (!lazerRenderer.isVisible && this.tag !="B_Beam")
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
        
        if (other.tag == "Player" && (this.tag=="E_Laser" || this.tag == "E_Beam" || this.tag == "B_Beam") && !_playerHit)
        {
            //prevents double hit
         
            _playerHit = true;
            _player.Damage();
            if (_player.GetPlayerLife() == 0)
            {

              
                Destroy(other.gameObject);
                GameObject.Find("Spawn_Manger").GetComponent<Spawn_Manger>().SetIsEnemySpawnActive(false);
                GameObject.Find("Canvas").GetComponent<UI_Manger>().startGameOver();
                if (this.tag != "E_Beam" && this.tag != "B_Beam") Destroy(this.gameObject);
            }
            else
            {
               
                _speed = 0;
                if (this.tag != "E_Beam" && this.tag != "B_Beam")
                {
                    Debug.Log(this.tag +"destory?");
                    Destroy(this.gameObject);
                }
                 
            }
        }
 

    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

}
