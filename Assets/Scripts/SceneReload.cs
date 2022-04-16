using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReload : MonoBehaviour
{
    GameObject character;
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
    }

    public void Reload()
    {
        Destroy(character);
    }

}
