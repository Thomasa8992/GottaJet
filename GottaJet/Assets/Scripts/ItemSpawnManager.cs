using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public GameObject[] items;
    private float spawnRangeZ = 14;
    private float spawnPositionY = 10;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        StartCoroutine(SpawnRandomItem());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRandomItem() {
        while(!gameManager.gameIsOver) {
            yield return new WaitForSeconds(10);

            var spawnPosition = new Vector3(0, spawnPositionY, Random.Range(-spawnRangeZ, spawnRangeZ));
            var itemIndex = Random.Range(0, items.Length);

            Instantiate(items[itemIndex], spawnPosition, items[itemIndex].transform.rotation);
        }
    }
}
