using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    public TextMeshProUGUI highScoreText;
    private int highScore;

    // Start is called before the first frame update
    void Start() {
        score = 0;

        GetHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHighScore();
    }

    private void GetHighScore() {
        highScore = PlayerPrefs.GetInt("highScore", highScore);
        highScoreText.text = $"High Score: {highScore}";
    }

    public void UpdateScore(int addToScore) {
        score += addToScore;

        scoreText.text = $"Score: {score}";
    }

    public void UpdateHighScore() {

        if (score > highScore) {
            highScore = score;

            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();

            GetHighScore();
        }
    }
}
