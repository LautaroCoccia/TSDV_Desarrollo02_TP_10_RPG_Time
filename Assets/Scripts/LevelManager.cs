using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int lives = 3;
    [SerializeField] int score = 0;
    [SerializeField] int enemiesLeft = 0;

    [SerializeField] private TextMeshProUGUI UIScore;
    [SerializeField] private TextMeshProUGUI UIHealth;
    [SerializeField] private TextMeshProUGUI UIEnemies;
    [SerializeField] private TextMeshProUGUI UIExtras;
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject QuitMenuUI;
    [SerializeField] private GameObject GameOverMenuUI;

    private static bool pause = false;
    private static LevelManager _instanceLevelManager;
    private const int minLives = 1;
    public static LevelManager Get()
    {
        return _instanceLevelManager;
    }
    private void Awake()
    {
        if (_instanceLevelManager == null)
        {
            _instanceLevelManager = this;
        }
        else if (_instanceLevelManager != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UIHealth.text = ("Lives: " + lives);
        UIEnemies.text = ("Left: " + enemiesLeft);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause();
        }
    }
    public void StartEnemies()
    {
        enemiesLeft++;
    }
    public void UpdateEnemies()
    {
        enemiesLeft--;
        UIEnemies.text = ("Left: " + enemiesLeft);
    }
    public void UpdateScore(int SCORE)
    {
        score += SCORE;
        UIScore.text = ("Score: " + score);
    }
    public void UpdateHealth()
    {
        lives--;
        if(lives< minLives)
        {
            GameOver();
        }
        UIHealth.text = ("Lives: " + lives);
    }
    private void SetTimeScale(int scale)
    {
        Time.timeScale = scale;
    }
    private void GameOver()
    {
        SetTimeScale(0);
        GameOverMenuUI.SetActive(true);
    }
    public void SetPause()
    {
        pause = !pause;
        if (pause)
        {
            SetTimeScale(0);
            PauseMenuUI.SetActive(pause);
        }
        else
        {
            SetTimeScale(1);
            PauseMenuUI.SetActive(pause);
            QuitMenuUI.SetActive(pause);
        }
    }
}
