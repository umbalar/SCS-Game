using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersStartPosition : MonoBehaviour
{
    private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponentInParent<Grid>();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = grid.GetCellCenterWorld(grid.WorldToCell(transform.GetChild(i).transform.position));
        }
        //transform.position = grid.GetCellCenterWorld(grid.WorldToCell(transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
