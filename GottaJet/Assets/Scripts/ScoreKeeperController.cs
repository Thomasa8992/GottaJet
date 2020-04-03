using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeperController : MonoBehaviour
{
    public int score = 0;
    private TextMesh scoreComponent;

    // Start is called before the first frame update
    void Start()
    {
        scoreComponent = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreComponent.text = "Score: " + score;
    }
}
