using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] Grisha grisha;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision = grisha.GetComponent<Collider2D>();
        Debug.Log("level end");
    }
}
