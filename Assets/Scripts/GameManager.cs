using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance { get; } = new GameManager();
    private int _deathCount;
    private int _stage = 0;

    [SerializeField] public Text deathUI;
    [SerializeField] public Text stageUI;

    void Start()
    {
        IncrementStage();
    }

    void Update()
    {
        deathUI.text = "Deaths: " + _deathCount;
    }

    public static GameManager GetInstance()
    {
        return Instance;
    }
    public void IncrementDeaths()
    {
        Debug.Log("Death count = " + _deathCount);
        _deathCount++;
    }

    public int GetDeathCount()
    {
        return _deathCount;
    }

    public void IncrementStage()
    {
        stageUI.text = "Stage: " + (++_stage);
        Debug.Log(_stage);
        
    }
}
