using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class TileRedactor : MonoBehaviour
{
    public TileBase _tile1;
    public TileBase _tile2;
    public TileBase _tile3;
    public TileBase _tileToBuild;
    [SerializeField] private GameObject _tilemap;
    private Tilemap _tilemapComponent;
    private Camera _mainCamera;

    void Start()
    {
        _tilemapComponent = _tilemap.GetComponent<Tilemap>();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _tileToBuild != null)
        {
            Vector3 clickWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickCellPosition = _tilemapComponent.WorldToCell(clickWorldPosition);
            //Debug.Log(clickCellPosition);
            if (!_tilemapComponent.HasTile(clickCellPosition))
            {
                _tilemapComponent.SetTile(clickCellPosition, _tileToBuild);
                _tileToBuild = null;
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
        _tileToBuild = _tile1;
    }
    public void PickTile2()
    {
        _tileToBuild = _tile2;
    }
    public void PickTile3()
    {
        _tileToBuild = _tile3;
    }
}
