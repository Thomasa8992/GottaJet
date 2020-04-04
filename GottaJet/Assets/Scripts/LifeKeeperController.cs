using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeKeeperController : MonoBehaviour
{
    public int lives = 3;
    private TextMesh lifeKeeper;

    // Start is called before the first frame update
    void Start()
    {
        lifeKeeper = GetComponent<TextMesh>();
        lifeKeeper.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        lifeKeeper.text = "Lives: " + lives;
    }
}
