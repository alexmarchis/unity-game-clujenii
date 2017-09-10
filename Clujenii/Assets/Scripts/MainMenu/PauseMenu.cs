﻿using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{

    private ShowPanels showPanels;                      //Reference to the ShowPanels script used to hide and show UI panels
    private bool isPaused;                              //Boolean to check if the game is paused or not
    private MainMenuOptions mainMenuOptions;                   //Reference to the StartButton script

    //Awake is called before Start()
    void Awake()
    {
        //Get a component reference to ShowPanels attached to this object, store in showPanels variable
        showPanels = GetComponent<ShowPanels>();
        //Get a component reference to StartButton attached to this object, store in startScript variable
        mainMenuOptions = GetComponent<MainMenuOptions>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !mainMenuOptions.inMainMenu)
        {
            if (!isPaused)
            {
                DoPause();
            }
            else if (isPaused)
            {
                UnPause();
            }
        }
    }

    public void DoPause()
    {
        //Set isPaused to true
        isPaused = true;
        //Set time.timescale to 0, this will cause animations and physics to stop updating
        Time.timeScale = 0;
        //call the ShowPausePanel function of the ShowPanels script
        showPanels.ShowPausePanel();
    }

    public void UnPause()
    {
        //Set isPaused to false
        isPaused = false;
        //Set time.timescale to 1, this will cause animations and physics to continue updating at regular speed
        Time.timeScale = 1;
        //call the HidePausePanel function of the ShowPanels script
        showPanels.HidePausePanel();
    }


}
