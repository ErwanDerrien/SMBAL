using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance { get; } = new GameManager();
    private int _deathCount = 0;
    private int _stageCount = 1;
    private static AudioClip _deathSound;
    private static AudioClip _jumpSound;
    private static AudioClip _music1;
    private static AudioClip _music2;
    private static AudioClip _music3;
    private static AudioClip _nextLevel;
    private static AudioSource _audioSource;
    private bool _key = true;
    
    void Start()
    {
        _deathSound = Resources.Load<AudioClip>("death");
        _jumpSound = Resources.Load<AudioClip>("jump");
        _music1 = Resources.Load<AudioClip>("music1");
        _music2 = Resources.Load<AudioClip>("music2");
        _music3 = Resources.Load<AudioClip>("music3");
        _nextLevel = Resources.Load<AudioClip>("nextLevel");

        _audioSource = GetComponent<AudioSource>();
        PlaySound("music2");
    }

    void Update()
    {
        
    }

    public static void PlaySound(string sound)
    {
        switch (sound)
        {
            case "jump" :
                _audioSource.PlayOneShot(_jumpSound,0.1f);
                break;
            case "death" :
                _audioSource.PlayOneShot(_deathSound,0.2f);
                break;
            case "music1" :
                _audioSource.PlayOneShot(_music1, 0.1f);
                break;
            case "music2" :
                _audioSource.PlayOneShot(_music2, 0.1f);
                break;
            case "music3" :
                _audioSource.PlayOneShot(_music3, 0.1f);
                break;
            case "nextLevel" :
                _audioSource.PlayOneShot(_nextLevel);
                break;
        }
    }

    public static GameManager GetInstance()
    {
        return Instance;
    }
    public void IncrementDeaths()
    {
        _deathCount++;
        Debug.Log("Death count = " + _deathCount);
    }
    
    public void IncrementStage()
    {
        ++_stageCount;
        PlaySound("nextLevel");
        Debug.Log("Stage: " + _stageCount);
        
    }
    public int GetDeathCount()
    {
        return _deathCount;
    } public int GetStageCount()
    {
        return _stageCount;
    }

    public void SetKey(bool possesion)
    {
        _key = possesion;
    }

    public bool GETKey()
    {
        return _key;
    }
}
