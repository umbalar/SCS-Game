using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private string _nextSceneName;

    public void LoadNextScene()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>()._levelComplete = true;
        SceneManager.LoadScene(_nextSceneName);
    }
}
