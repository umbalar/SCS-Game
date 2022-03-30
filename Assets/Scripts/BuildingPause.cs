using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPause : MonoBehaviour
{
    public static bool BuildPauseActive = false;
    public GameObject BuildingPauseMenu;
    public GameObject BuildingPauseOnButton;
    public GameObject BuildingPauseOffButton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (BuildPauseActive)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        BuildingPauseMenu.SetActive(false);
        BuildingPauseOnButton.SetActive(true);
        BuildingPause.BuildPauseActive = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        BuildingPauseMenu.SetActive(true);
        BuildingPauseOnButton.SetActive(false);
        BuildingPause.BuildPauseActive = true;
        Time.timeScale = 0f;
    }

}
