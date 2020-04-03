using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreController : MonoBehaviour
{
    public int highScore;

    public TextMesh highScoreComponent;
    // Start is called before the first frame update
    void Start()
    {
        highScoreComponent = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        highScoreComponent.text = "High Score" + highScore;
    }
}
