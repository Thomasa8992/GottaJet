﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRangeY = 8;
    private float spawnPositionZ = 26;
    public int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", 2, 1.5f);
        SpawnRandomEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnRandomEnemy() {
        var spawnPosition = new Vector3(0, Random.Range(-spawnRangeY, spawnRangeY), spawnPositionZ);

        if(counter <= 1) {
            counter++;
            Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
        }
    }
}