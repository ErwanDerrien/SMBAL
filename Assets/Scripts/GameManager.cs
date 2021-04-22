using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance { get; } = new GameManager();
    private int _deathCount;
    private int _stageCount;
    
    void Start()
    {
        _deathCount = 0;
        _stageCount = 0;
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
        _stageCount++;
        Debug.Log("Stage: " +_stageCount);
        
    }
    public int GetDeathCount()
    {
        return _deathCount;
    } public int GetStageCount()
    {
        return _stageCount;
    }
}
