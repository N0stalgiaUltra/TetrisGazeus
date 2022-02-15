using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Score")]
    public int score;
    [SerializeField] private int highScore;


    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI hsUI;
    [SerializeField] private Canvas GameOverUI;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        StartGame();
        GameOverUI.enabled = false;
    }
    private void Update()
    {

        scoreUI.text = $"Score: {score}";
    
    }

    /// <summary>
    /// Metodo que gerencia o Game Over
    /// </summary>
    public void GameOver()
    {
        print("game over");
        ScoreControl();
        GameOverUI.enabled = true;
        Time.timeScale = 0;
    }
    
    /// <summary>
    /// Metodo para iniciar o game
    /// </summary>
    public void StartGame()
    {
        hsUI.text = $"Highscore:{PlayerPrefs.GetInt("HighScore")}";
        score = 0;
        Time.timeScale = 1;
        BlocoSpawner.instance.RandomSpawn();
    }

    /// <summary>
    /// Metodo para resetar
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Metodo para sair do game (não funciona no editor, só em aplicações)
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Metodo para controlar o Score e Highscore 
    /// </summary>
    private void ScoreControl()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
        hsUI.text = $"Highscore: {PlayerPrefs.GetInt("HighScore")}";
    }
}
