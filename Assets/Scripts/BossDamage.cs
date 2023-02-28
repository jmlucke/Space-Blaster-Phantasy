using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossDamage : MonoBehaviour
{
    [SerializeField]
    private int _hitPoints;
    private bool _explode= false;
    private Animator _bAnimator;
    private Player _player;
    private Boss _boss;
    // Start is called before the first frame update
    void Start()
    {
         _bAnimator = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _boss = this.transform.parent.gameObject.GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log(_boss.GetShieldStatus());

        if (this.tag !="boss_h" || !_boss.GetShieldStatus())
        {
            _hitPoints--;
            if (other.tag == "Laser" && !_explode)
            {
                if (_hitPoints <= 0)
                {
                    if(this.tag == "boss_l")
                    {
                        _boss.Shield(false);
                    }
                    _bAnimator.SetTrigger("explode");
                    _explode = true;


                }
                _player.PlaySound(1);
                Destroy(other.gameObject);
            }
        }
        else if(this.tag == "boss_h")
        {
            _player.PlaySound(3);
        }

    }
}
