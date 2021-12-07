using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MANAGER : Singleton<MANAGER>
{
    [Header("Score")]
    [SerializeField] TextMeshProUGUI ScoreTxt;
    int Score;
    int highScore;

    [Header("Menu")]
    [SerializeField] GameObject MenuPause;
    [SerializeField] GameObject MenuGameOver;
    [SerializeField] Text HighScoreGOtext;

    [Space(10)]
    [SerializeField] Clicker clickerScript;

    


    private void Awake()
    {
        
        

        
    }


    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        Score = 0;
        UpdateDisplayScore();
    }

    // permet d'ajouter du score 
    public void AddScore(int ScoreToAdd)
    {
        Score += ScoreToAdd;
        UpdateDisplayScore();
    }

    // Mets a jour l'affichage du score 
    void UpdateDisplayScore()
    {
        ScoreTxt.text = Score.ToString();
    }

    #region Ui Button
    public void button_pause()
    {
        Time.timeScale = 0;
        MenuPause.SetActive(true);
        clickerScript.canClick = false;
    }

    public void Button_Resume()
    {
        Time.timeScale = 1;
        MenuPause.SetActive(false);
        clickerScript.canClick = true;
    }

    public void Button_MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        clickerScript.canClick = true;
    }
    #endregion

    public void GameOver()
    {
        Time.timeScale = 0;
        MenuGameOver.SetActive(true);

        if (Score > highScore) highScore = Score;
        
        PlayerPrefs.SetInt("HighScore", highScore);
        HighScoreGOtext.text = "HighScore: " + highScore.ToString();


    }
}
