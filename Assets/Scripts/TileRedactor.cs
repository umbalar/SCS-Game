using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class TileRedactor : MonoBehaviour
{
    public TileBase tile1;
    public TileBase tile2;
    public TileBase tile3;
    public TileBase tileToBuild;
    private Tilemap tilemap;
    private Camera mainCamera;
    public GameObject buildingPauseOwner;
    private BuildingPause buildingPause;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        mainCamera = Camera.main;
        buildingPause = buildingPauseOwner.GetComponent<BuildingPause>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && tileToBuild != null)
        {
            Vector3 clickWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickCellPosition = tilemap.WorldToCell(clickWorldPosition);
            //Debug.Log(clickCellPosition);
            if (BuildingPause.BuildPauseActive)
            {
                tilemap.SetTile(clickCellPosition, tileToBuild);
                tileToBuild = null;
                buildingPause.Resume();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PickTile1();
            buildingPause.Pause();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PickTile2();
            buildingPause.Pause();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PickTile3();
            buildingPause.Pause();
        }
    }
    public void PickTile1()
    {
        tileToBuild = tile1;
    }
    public void PickTile2()
    {
        tileToBuild = tile2;
    }
    public void PickTile3()
    {
        tileToBuild = tile3;
    }
}
