using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private EnemyController enemyController;
    public GameObject enemyProjectile;
    private EnemyProjectile enemyProjectileScript;

    public GameObject waveText;
    private TextMesh waveTextMesh;

    private float spawnRangeY = 7;
    private float spawnPositionZ = 14;

    private int enemyCount;
    public bool newWaveHasStarted = false;

    private float invokeTime = 3;

    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        enemyProjectileScript = enemyProjectile.GetComponent<EnemyProjectile>();
        enemyProjectileScript.movementSpeed = 15;


        enemyController = enemyPrefab.GetComponent<EnemyController>();
        enemyController.movementSpeed = 10;

        waveTextMesh = waveText.GetComponent<TextMesh>();
        waveText.SetActive(true);

        SpawnRandomEnemy();
    }

    //Todo: offset the time between the last enemy spawn and the next wave text 

    IEnumerator BeginEnemySpawn() {

        yield return new WaitForSeconds(5);

        waveText.SetActive(false);
    }

    private void SpawnRandomEnemy() {
        var spawnPosition = new Vector3(0, UnityEngine.Random.Range(-spawnRangeY, spawnRangeY), spawnPositionZ);

        Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
        enemyCount++;

        Invoke("SpawnRandomEnemy", invokeTime);
        enemyCount++;

        HandleNewWave();
    }

    private void HandleNewWave() {
        if (enemyCount == 5) {
            handleNewWave(2.5f, 18);
        }

        if (enemyCount == 10) {
            handleNewWave(2, 20);
        }

        if (enemyCount == 20) {
            handleNewWave(1.5f, 16);
        }
    }

    private void handleNewWave(float newInvoketime, float enemyProjectileMovementSpeed) {
        newWaveHasStarted = true;
        waveNumber++;

        waveTextMesh.text = "Wave " + waveNumber;
        waveText.SetActive(true);

        enemyProjectileScript.movementSpeed = enemyProjectileMovementSpeed;

        StartCoroutine(StartNextPhase(newInvoketime));
    }

    private IEnumerator StartNextPhase(float newInvoketime) {
        invokeTime = 10;

        yield return new WaitForSeconds(8);

        waveText.SetActive(false);
        invokeTime = newInvoketime;
        newWaveHasStarted = false;
    }
}
