using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public GameObject[] items;
    public float spawnRangeZ = 25;
    public float spawnPositionY = 8;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomItem", 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnRandomItem() {
        var spawnPosition = new Vector3(0, spawnPositionY, Random.Range(-spawnRangeZ, spawnRangeZ));
        var itemIndex = Random.Range(0, items.Length);

        Instantiate(items[itemIndex], spawnPosition, items[itemIndex].transform.rotation);
    }
}
