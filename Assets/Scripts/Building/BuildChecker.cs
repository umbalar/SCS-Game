using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildChecker : MonoBehaviour
{
    private Object _objectToBuild;
    private Grid _grid;
    public bool _allowedToBuild;
    private List<Tilemap> _tilemaps;

    void Start()
    {
        _tilemaps = FillTilemapList(_tilemaps);
        _grid = GetComponent<Grid>();
        _allowedToBuild = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private List<Tilemap> FillTilemapList(List<Tilemap> tilemaps)
    {
        tilemaps = new List<Tilemap>();
        tilemaps.Add(GameObject.FindGameObjectWithTag("DefoultTiles").GetComponent<Tilemap>());
        tilemaps.Add(GameObject.FindGameObjectWithTag("ClimbingTiles").GetComponent<Tilemap>());
        tilemaps.Add(GameObject.FindGameObjectWithTag("KillTiles").GetComponent<Tilemap>());
        return tilemaps;
    }
}
