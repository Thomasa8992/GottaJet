using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRangeY = 7;
    private float spawnPositionZ = 18;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", 3f, 3f);
        SpawnRandomEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnRandomEnemy() {
        var spawnPosition = new Vector3(0, Random.Range(-spawnRangeY, spawnRangeY), spawnPositionZ);

        Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
    }
}
