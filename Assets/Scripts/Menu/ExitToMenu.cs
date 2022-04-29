using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenu : MonoBehaviour
{
    CharacterController1 characterController1;
    private void Start()
    {
        characterController1 = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController1>();
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            characterController1._levelComplete = true;
            SceneManager.LoadScene("Menu");

        }
    }
}
