using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string _previousSceneName;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _previousSceneName = SceneManager.GetActiveScene().name;

        if (GameObject.FindGameObjectsWithTag("SceneController").Length > 1)
        {
            Destroy(gameObject);
        }
    }

    public void GetNextSceneName (string sceneName)
    {
        _previousSceneName = sceneName;
    }
}
