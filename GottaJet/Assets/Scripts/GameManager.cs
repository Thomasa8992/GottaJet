using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    public TextMeshProUGUI highScoreText;
    private int highScore;

    public TextMeshProUGUI livesText;
    public int lives;

    public int enemiesLeft;

    public TextMeshProUGUI gameOverText;
    public bool gameIsOver;

    public Button restartButton;

    private int bonus;
    private int bonusInterval = 25000;

    public 
    // Start is called before the first frame update
    void Start() {
        score = 0;
        lives = 3;
        bonus = bonusInterval;

        GetLives();
        GetHighScore();
    }

    private void GetLives() {
        livesText.text = $"Lives: {lives}";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHighScore();
        GameOver();
    }

    private void GetHighScore() {
        highScore = PlayerPrefs.GetInt("highScore", highScore);
        highScoreText.text = $"High Score: {highScore}";
    }

    public void UpdateScore(int addToScore) {
        score += addToScore;

        IncreaseLives();

        scoreText.text = $"Score: {score}";
    }

    public void UpdateHighScore() {
        if (score > highScore) {
            SetHighScore();
            GetHighScore();
        }
    }

    private void SetHighScore() {
        highScore = score;

        PlayerPrefs.SetInt("highScore", highScore);
        PlayerPrefs.Save();
    }

    public void DecreaseLives() {
        lives -= 1;

        GetLives();
    }

    public void GameOver() {
        if(lives == 0) {
            gameIsOver = true;

            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void IncreaseLives() {
        if(score >= bonus) {
            lives++;
            bonus += bonusInterval;
        }

        GetLives();
    }
}
