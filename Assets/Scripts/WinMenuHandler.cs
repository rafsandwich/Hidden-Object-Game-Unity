using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenuHandler : MonoBehaviour
{
    public Button quitButton;
    public Button playAgainButton;

    void Start()
    {
        // add button click listeners
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }

        if (playAgainButton != null)
        {
            playAgainButton.onClick.AddListener(PlayAgain);
        }
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void PlayAgain()
    {
        // reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
