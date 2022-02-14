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
    private static int highScore;


    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI hsUI;
    [SerializeField] private GameObject GameOverUI;

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
        GameOverUI.SetActive(false);
    }
    private void Update()
    {

        scoreUI.text = ($"Score: {score}");
    
    }

    public void GameOver()
    {
        print("game over");
        ScoreControl();
        GameOverUI.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void StartGame()
    {
        score = 0;
        Time.timeScale = 1;
        BlocoSpawner.instance.RandomSpawn();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void ScoreControl()
    {
        if (score > highScore)
            highScore = score;

        hsUI.text = ($"Highscore: {highScore}");
    }
}
