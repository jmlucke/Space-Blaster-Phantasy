using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damage : MonoBehaviour
{
    [SerializeField]
    private int dmg_threshold=1;
    private Animator d_Animator;
    private Player _player;
    public GameObject dmgVisual;
    private Renderer d_sprite;
    // Start is called before the first frame update
    void Start()
    {
        DmgVisualization(false);
        d_Animator = GetComponent<Animator>();
         d_sprite = GetComponent<Renderer>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player!=null && _player.GetPlayerLife() == dmg_threshold)
        {
            DmgVisualization(true);
            
        }
    }

    void DmgVisualization(bool state)
    {  
        if (state)
        {
            d_sprite.enabled = state;
            d_Animator.SetTrigger("P_Dmg");
        }
    }
}
