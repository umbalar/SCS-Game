using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReload : MonoBehaviour
{
    private GameObject _character;
    void Start()
    {
        _character = GameObject.FindGameObjectWithTag("Player");
    }

    public void Reload()
    {
        Destroy(_character);
    }

}
