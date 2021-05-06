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
    private static AudioClip _music;
    private static AudioClip _nextLevel;
    private static AudioSource _audioSource;
    private bool _key = true;
    
    void Start()
    {
        _deathSound = Resources.Load<AudioClip>("death1");
        _jumpSound = Resources.Load<AudioClip>("jump2");
        _music = Resources.Load<AudioClip>("music");
        _nextLevel = Resources.Load<AudioClip>("nextLevel");

        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public static void PlaySound(string sound)
    {
        switch (sound)
        {
            case "jump" :
                _audioSource.PlayOneShot(_jumpSound);
                break;
            case "death" :
                _audioSource.PlayOneShot(_deathSound);
                break;
            case "music" :
                _audioSource.PlayOneShot(_music);
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
