using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public int difficultyLevel;

    private GameManager gameManager;

    public void SetDifficulty() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        gameManager.StartGame(difficultyLevel);
    }
}
