using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damage : MonoBehaviour
{
    [SerializeField]
    private int _dmgThreshold=1;
    private Animator _dAnimator;
    private Player _player;
     
    public GameObject dmgVisual;
    private Renderer _dSprite;
    private bool _animState=false;
    // Start is called before the first frame update
    void Start()
    {
        DmgVisualization(false);
        _dAnimator = GetComponent<Animator>();
         _dSprite = GetComponent<Renderer>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_player!=null && !_animState && _player.GetPlayerLife() == _dmgThreshold)
        {
            _animState = true;
 
            
            DmgVisualization(_animState);
            
        }
        else if(_player != null && !_animState && _player.GetPlayerLife()> _dmgThreshold)
        {
            _dAnimator.ResetTrigger("P_Dmg");
            _dSprite.enabled = false;
        }

    }

    void DmgVisualization(bool state)
    {  
        if (state)
        {
            _dSprite.enabled = state;
            _dAnimator.SetTrigger("P_Dmg");
            _animState = false;
        }
         
    }
}
