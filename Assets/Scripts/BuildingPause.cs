using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPause : MonoBehaviour
{
    public static bool BuildPauseActive = false;
    public GameObject BuildingPauseMenu;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangePauseStatus();   
        }
    }

    public void ChangePauseStatus()
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

    private void Resume()
    {
        BuildingPauseMenu.SetActive(false);
        BuildingPause.BuildPauseActive = false;
        Time.timeScale = 1f;
    }

    private void Pause()
    {
        BuildingPauseMenu.SetActive(true);
        BuildingPause.BuildPauseActive = true;
        Time.timeScale = 0f;
    }

}
