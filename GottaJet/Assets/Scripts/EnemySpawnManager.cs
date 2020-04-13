using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {
    public GameObject enemyPrefab;
    private EnemyController enemyController;
    public GameObject enemyProjectile;
    private EnemyProjectile enemyProjectileScript;

    private GameManager gameManager;

    //public GameObject waveText;
    //private TextMesh waveTextMesh;

    private float spawnRangeY = 7;
    private float spawnPositionZ = 14;

    private int enemyWaveCount;
    public bool newWaveHasStarted;

    public int waveNumber = 1;

    public bool gameIsActive = false;
    private float spawnRate;

    private float newSpawnRate;

    // Start is called before the first frame update
    void Start() {
        spawnRate = 3;
        newSpawnRate = spawnRate;
        newWaveHasStarted = false;
        enemyWaveCount = 0;

        enemyProjectileScript = enemyProjectile.GetComponent<EnemyProjectile>();
        enemyProjectileScript.movementSpeed = 15;


        enemyController = enemyPrefab.GetComponent<EnemyController>();
        enemyController.movementSpeed = 10;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        gameManager.enemiesLeft = 5;

        //waveTextMesh = waveText.GetComponent<TextMesh>();
        //waveText.SetActive(true);

        StartCoroutine(SpawnRandomEnemy());
    }

    IEnumerator SpawnRandomEnemy() {

        Debug.Log(gameManager.gameIsActive);
        while (gameManager.gameIsActive) {
            yield return new WaitForSeconds(spawnRate);
            spawnRate = newSpawnRate;
            //waveText.SetActive(false);

            var spawnPosition = new Vector3(0, UnityEngine.Random.Range(-spawnRangeY, spawnRangeY), spawnPositionZ);
            Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
            enemyWaveCount++;
            
            //HandleNewWave();
        }
    }

    private void Update() {
    }

    private void HandleNewWave() {
        if (enemyWaveCount == 5) {
            newWaveHasStarted = true;

            if (gameManager.enemiesLeft == 1) {
                StartCoroutine(SetNewWave(17));
                newWaveHasStarted = false;
            }
        }
    }

     IEnumerator SetNewWave(float enemyProjectileMovementSpeed) {
        Debug.Log("In Set New Wave");
        yield return new WaitForSeconds(5);

        //waveTextMesh.text = "Wave " + waveNumber;
        //waveText.SetActive(true);
        enemyProjectileScript.movementSpeed = enemyProjectileMovementSpeed;
    }


    //Spawn a certain amount of enemies at the beginning of the game to mark wave 1
    //When the last enemy of each wave is destroyed start next wave
}
