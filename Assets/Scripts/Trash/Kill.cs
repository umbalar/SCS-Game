using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    GameObject character;
    private Grid grid;
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<Renderer>().enabled = false;
        grid = GetComponentInParent<Grid>();
        transform.position = grid.GetCellCenterWorld(grid.WorldToCell(transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("PreKilled");
        if (collision.gameObject.tag == "Player")
        {
            Destroy(character);
            Debug.Log("Killed");
        }
    }
}
