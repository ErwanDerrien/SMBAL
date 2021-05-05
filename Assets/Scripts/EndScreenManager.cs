using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    private string path = "Assets/Resources/Highscore.txt";

    private int _scoreFinal;
    
    [SerializeField] private TextMeshProUGUI ScoreFinal;
    [SerializeField] private TextMeshProUGUI HighScore1;
    [SerializeField] private TextMeshProUGUI HighScore2;
    [SerializeField] private TextMeshProUGUI HighScore3;
    // Start is called before the first frame update
    void Start()
    {
        //Affichage Score Final
        _scoreFinal = GameManager.GetInstance().GetDeathCount();
        ScoreFinal.text += _scoreFinal;
        
        string[] highscores = new string[4];
        int counter = 0;
        
        //Ajout highscores enregistres a un tableau
        StreamReader reader = new StreamReader(path);
        string line;

        while ((line = reader.ReadLine()) != null)
        {
            highscores[counter] = line;
            counter++;
        }
        reader.Close();
        
        // Trouver bonne position pour le score
        int position = 0;
        for (int i = 0; i < 3; i++)
        {
            if (_scoreFinal < Int32.Parse(highscores[i]))
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
            temp = highscores[i];
            highscores[i] = newValue;
            newValue = temp;
        }

        // Ajouter le score a la liste
        StreamWriter writer = new StreamWriter(path);
        foreach (var newLine in highscores)
        {
            writer.WriteLine(newLine);
        }
        
        writer.Close();

        HighScore1.text += highscores[0];
        HighScore2.text += highscores[1];
        HighScore3.text += highscores[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Restart()
    {
        SceneManager.LoadScene("SousSol");
    }
}
