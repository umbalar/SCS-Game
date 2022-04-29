using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    SceneController _sceneController;
    [SerializeField] private string _baseStartScene;
    private void Start()
    {
        _sceneController = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void StartGame()
    {
        if (_sceneController._previousSceneName != "Menu")
        {
            SceneManager.LoadScene(_sceneController._previousSceneName);
        }
        else
        {
            SceneManager.LoadScene(_baseStartScene);
        }
    }
}
