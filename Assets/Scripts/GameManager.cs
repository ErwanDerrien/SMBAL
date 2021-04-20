using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance { get; } = new GameManager();
    private int _deathCount;
    

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
        Debug.Log(_deathCount);
    }

    public int GETDeathCount()
    {
        return _deathCount;
    }
}
