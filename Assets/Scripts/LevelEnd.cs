using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
