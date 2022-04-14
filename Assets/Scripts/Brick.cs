using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Grid grid;

    void Start()
    {
        grid = GetComponentInParent<Grid>();
        transform.position = grid.GetCellCenterWorld(grid.WorldToCell(transform.position));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Brick")
        {
            Debug.Log("brick");
        }
    }
}
