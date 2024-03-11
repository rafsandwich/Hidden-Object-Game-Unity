using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuHandler : MonoBehaviour
{
    private bool isOptionsActive = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        // if esc pressed, toggle
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc key pressed ");
            ToggleOptionsScreen();
        }
    }

    public void ToggleOptionsScreen()
    {
        //isOptionsActive = !isOptionsActive;

        // enable / disable
        //gameObject.SetActive(isOptionsActive);

        if (isOptionsActive)
        {
            // disable player interactions with background
            FindObjectOfType<inputHandler>().DisablePlayerInteractions();
            gameObject.SetActive(true);
        }
        else
        {
            // enable player interactions with background
            FindObjectOfType<inputHandler>().EnablePlayerInteractions();
            gameObject.SetActive(false);
        }
    }
}
