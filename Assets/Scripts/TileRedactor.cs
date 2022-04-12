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
    [SerializeField] private GameObject tilemap;
    private Tilemap tilemapComponent;
    private Camera mainCamera;

    void Start()
    {
        tilemapComponent = tilemap.GetComponent<Tilemap>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && tileToBuild != null)
        {
            Vector3 clickWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickCellPosition = tilemapComponent.WorldToCell(clickWorldPosition);
            //Debug.Log(clickCellPosition);
            if (!tilemapComponent.HasTile(clickCellPosition))
            {
                tilemapComponent.SetTile(clickCellPosition, tileToBuild);
                tileToBuild = null;
            }
            else
            {
                Debug.Log("Tile already set");
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PickTile1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PickTile2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PickTile3();
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
