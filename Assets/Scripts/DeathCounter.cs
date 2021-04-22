using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCounter : MonoBehaviour
{
    public Text text;
    private int _deathCount;
    void Start()
    {
        text = GetComponent<Text>();
    }

    
    void Update()
    {
        _deathCount = GameManager.GetInstance().GetDeathCount();
        text.text = "Deaths: " + _deathCount;
    }
}
