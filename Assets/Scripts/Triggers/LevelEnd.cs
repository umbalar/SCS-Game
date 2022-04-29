using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private string _nextSceneName;
    private SceneController _sceneController;

    private void Start()
    {
        _sceneController = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
    }
    public void LoadNextScene()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController1>()._levelComplete = true;
        _sceneController.GetNextSceneName(_nextSceneName);
        SceneManager.LoadScene(_nextSceneName);
    }
}
