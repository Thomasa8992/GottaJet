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

        SpawnRandomEnemy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnRandomEnemy() {
        waveText.SetActive(false);

        var spawnPosition = new Vector3(0, Random.Range(-spawnRangeY, spawnRangeY), spawnPositionZ);

        Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
        enemyCount++;

        handleNewWave();

        Invoke("SpawnRandomEnemy", invokeTime);
        newWaveHasStarted = false;
        
        if(invokeTime == 10) {
            invokeTime = 3;
        }
    }

    private void handleNewWave( ) {
        if (enemyCount == 25) {
            waveNumber++;

            waveTextMesh.text = "Wave " + waveNumber;
            waveText.SetActive(true);

            newWaveHasStarted = true;
            invokeTime = 10;

            //enemyController.movementSpeed += 2;
            enemyProjectileScript.movementSpeed += 2;
        }
    }
}
