using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersStartPosition : MonoBehaviour
{
    void Start()
    {
        Grid grid = GetComponentInParent<Grid>();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = grid.GetCellCenterWorld(grid.WorldToCell(transform.GetChild(i).transform.position));
        }
    }
}
