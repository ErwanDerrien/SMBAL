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
    private bool _key = true;
    
    void Start()
    {
       
    }

    void Update()
    {
        
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

    public void setKey(bool possesion)
    {
        _key = possesion;
    }

    public bool getKey()
    {
        return _key;
    }
}
