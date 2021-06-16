using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float lives = 3;
    [SerializeField] int score = 0;
    [SerializeField] int objAlive = 0;
    [SerializeField] int scoreToAdd = 0;
    [SerializeField] private TextMeshProUGUI UIScore;
    [SerializeField] private TextMeshProUGUI UIEnemies;
    [SerializeField] private TextMeshProUGUI UIExtras;
    [SerializeField] private Image UIHealth;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject quitMenuUI;
    [SerializeField] private GameObject gameOverMenuUI;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button mainMenuButton;

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
        pause = false;
        SetTimeScale(1);
        UIHealth.fillAmount = 1;
        UIEnemies.text = ("Left: " + objAlive);//Solo sirve para hacer debug
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && lives > 0)
        {
            SetPause();
        }
    }
    //Solo sirve para hacer debug
    public void StartObj()
    {
        objAlive++;
        if(!UIEnemies.IsActive())
        {
            UIEnemies.text = ("Left: " + objAlive);
        }
    }
    //Solo sirve para hacer debug
    public void UpdateObj()
    {
        objAlive--;
        UIEnemies.text = ("Left: " + objAlive);
        if(objAlive<=0)
        {
            GameOver();
        }
    }
    public void UpdateScore()
    {
        score += scoreToAdd;
        UIScore.text = ("" + score);
    }
    public void UpdateHealth(int actualHealth)
    {
        lives = (actualHealth);
        UIHealth.fillAmount = lives / 100;
        if(lives< minLives)
        {
            GameOver();
        }
    }
    private void SetTimeScale(int scale)
    {
        Time.timeScale = scale;
    }
    private void GameOver()
    {
        SetTimeScale(0);
        gameOverMenuUI.SetActive(true);
        continueButton.Select();
        mainMenuButton.Select();

    }
    public void SetPause()
    {
        pause = !pause;
        if (pause)
        {
            SetTimeScale(0);
            pauseMenuUI.SetActive(pause);
            continueButton.Select();
        }
        else
        {
            SetTimeScale(1);
            pauseMenuUI.SetActive(pause);
            quitMenuUI.SetActive(pause);
            mainMenuButton.Select();
        }
    }
    public void SelectButton()
    {
        if (continueButton.IsActive())
        {
            continueButton.Select();
        }
        else if (mainMenuButton.IsActive())
        {
            mainMenuButton.Select();
        }
    }
}
