using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {
    public GameObject enemyPrefab;
    private EnemyController enemyController;
    public GameObject enemyProjectile;
    private EnemyProjectile enemyProjectileScript;

    public GameObject waveText;
    private TextMesh waveTextMesh;

    private float spawnRangeY = 7;
    private float spawnPositionZ = 14;

    private int enemyCount;
    public bool newWaveHasStarted;

    public int waveNumber = 1;

    public bool gameIsActive = false;
    private float spawnRate;

    // Start is called before the first frame update
    void Start() {
        spawnRate = 3;
        newWaveHasStarted = false;

        enemyProjectileScript = enemyProjectile.GetComponent<EnemyProjectile>();
        enemyProjectileScript.movementSpeed = 15;


        enemyController = enemyPrefab.GetComponent<EnemyController>();
        enemyController.movementSpeed = 10;

        enemyController.enemiesLeft = 5;

        waveTextMesh = waveText.GetComponent<TextMesh>();
        waveText.SetActive(true);

        StartCoroutine(SpawnRandomEnemy());
    }

    IEnumerator SpawnRandomEnemy() {
        Debug.Log(newWaveHasStarted);
        while (!newWaveHasStarted) {
            yield return new WaitForSeconds(spawnRate);
            waveText.SetActive(false);

            var spawnPosition = new Vector3(0, UnityEngine.Random.Range(-spawnRangeY, spawnRangeY), spawnPositionZ);
            Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
            enemyCount++;

            HandleNewWave();
        }
    }

    private void HandleNewWave() {
        if(enemyCount == 5 && enemyController.enemiesLeft == 0) {
            spawnRate = 10;
            StartCoroutine(SetNewWave(2, 17));
            newWaveHasStarted = false;
        }
    }

    IEnumerator SetNewWave(float newSpawnRate, float enemyProjectileMovementSpeed) {
        newWaveHasStarted = true;
        waveNumber++;
        waveTextMesh.text = "Wave " + waveNumber;
        waveText.SetActive(true);

        enemyProjectileScript.movementSpeed = enemyProjectileMovementSpeed;

        yield return new WaitForSeconds(8);

        spawnRate = newSpawnRate;

    }
}
