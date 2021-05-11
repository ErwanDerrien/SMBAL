using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EndScreenManager : MonoBehaviour
{
    private string path = "Assets/Resources/Highscore.txt";

    private int _scoreFinal;
    
    [SerializeField] private TextMeshProUGUI scoreFinal;
    [SerializeField] private TextMeshProUGUI highScore1;
    [SerializeField] private TextMeshProUGUI highScore2;
    [SerializeField] private TextMeshProUGUI highScore3;
 
    void Start()
    {
        //Affichage Score Final
        _scoreFinal = GameManager.GetInstance().GetDeathCount();
        scoreFinal.text += _scoreFinal + " deaths";
        
        string[] highScores = new string[4];
        int counter = 0;
        
        //Ajout highscores enregistres a un tableau
        StreamReader reader = new StreamReader(path);
        string line;

        while ((line = reader.ReadLine()) != null)
        {
            highScores[counter] = line;
            counter++;
        }
        reader.Close();
        
        // Trouver bonne position pour le score
        int position = 0;
        for (int i = 0; i < 3; i++)
        {
            if (_scoreFinal < Int32.Parse(highScores[i]))
            {
                Debug.Log("position : " + position);
                break;
            }
            position++;
        }
        
        // Mettre le score a la bonne position
        string temp;
        string newValue = _scoreFinal.ToString();
        for (int i = position; i < 4; i++)
        {
            temp = highScores[i];
            highScores[i] = newValue;
            newValue = temp;
        }

        // Ajouter le score a la liste
        StreamWriter writer = new StreamWriter(path);
        foreach (var newLine in highScores)
        {
            writer.WriteLine(newLine);
        }
        
        writer.Close();

        highScore1.text += highScores[0] + " deaths";
        highScore2.text += highScores[1] + " deaths";
        highScore3.text += highScores[2] + " deaths";
    }
    
    
    public void Restart()
    {
        GameManager.GetInstance().Restart();
        SceneManager.LoadScene("SousSol");
    }
}
