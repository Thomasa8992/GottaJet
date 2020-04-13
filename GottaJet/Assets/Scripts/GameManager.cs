using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

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
    public bool gameIsActive = false;

    public int easyMode = 1;
    public int mediumMode = 2;
    public int hardMode = 3;

    public Button restartButton;

    private int bonus;
    private int bonusInterval = 25000;

    public GameObject titleScreen;

    public void StartGame(int difficultyLevel) {
        gameIsActive = true;

        if (difficultyLevel == easyMode) {

        }

        if (difficultyLevel == mediumMode) {

        }

        if (difficultyLevel == hardMode) {

        }


        titleScreen.SetActive(false);
    }

    // Start is called before the first frame update
    void Start() {
        lives = 3;

        score = 0;

        bonus = bonusInterval;

        GetHighScore();

        GetLives();
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
            gameIsActive = false;

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
