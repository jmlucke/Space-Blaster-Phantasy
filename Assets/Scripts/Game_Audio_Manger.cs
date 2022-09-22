using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Audio_Manger : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip[] _musicClips;
    void Start()
    {

        _audioSource = GetComponent<AudioSource>();
    }

    public void ChangeMusic(int musicId)
    {
        AudioSource source = gameObject.GetComponent<AudioSource>();
        gameObject.GetComponent<AudioSource>().clip = _musicClips[musicId];
        source.Play();
    }
}
