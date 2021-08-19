using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MANAGER : Singleton<MANAGER>
{
    float Score;
    [SerializeField] TextMeshProUGUI ScoreTxt;

    private void Start()
    {
        Score = 0f;
        UpdateDisplayScore();
    }

    // permet d'ajouter du score 
    public void AddScore(float ScoreToAdd)
    {
        Score += ScoreToAdd;
        UpdateDisplayScore();
    }

    // Mets a jour l'affichage du score 
    void UpdateDisplayScore()
    {
        ScoreTxt.text = Score.ToString();
    }
}
