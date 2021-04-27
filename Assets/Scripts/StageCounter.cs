using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageCounter : MonoBehaviour
{
    public Text text;
    private int _stageCount;
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        _stageCount = GameManager.GetInstance().GetStageCount();
        text.text = "Stage: " + _stageCount;
    }
}
