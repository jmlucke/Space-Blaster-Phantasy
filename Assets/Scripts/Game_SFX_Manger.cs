using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_SFX_Manger : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip[] _sfxClips;
    void Start()
    {

        _audioSource = GetComponent<AudioSource>();
    }

    public void ChangeSFX(int sfxId)
    {
        AudioSource source = gameObject.GetComponent<AudioSource>();
        gameObject.GetComponent<AudioSource>().clip = _sfxClips[sfxId];
        source.Play();
    }
}
