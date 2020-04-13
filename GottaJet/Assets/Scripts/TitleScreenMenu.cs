using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenMenu : MonoBehaviour
{
    public bool playerHasChoseDifficultyMode;
    public GameObject difficultyUI;
    public Button difficultyModeButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetModeOfGame() {
        if (gameObject.name == "DifficultyModeButton") {
            playerHasChoseDifficultyMode = true;
            difficultyModeButton.gameObject.SetActive(false);
            difficultyUI.SetActive(true);
        }
    }

    public void ReturnToTitleScreen() {
        playerHasChoseDifficultyMode = false;

        difficultyUI.SetActive(false);
        difficultyModeButton.gameObject.SetActive(true);
    }


}
