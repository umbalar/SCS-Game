using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMarker : MonoBehaviour
{
    private Vector3 _mouseWoldPosition;
    private Vector3Int _CellPosition;
    private Camera _mainCamera;
    private Grid _grid;
    public bool _allowedToBuild;
    private Color _baseColor;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        _mainCamera = Camera.main;
        _mouseWoldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _CellPosition = _grid.WorldToCell(_mouseWoldPosition);
        transform.position = _grid.GetCellCenterWorld(_CellPosition);
        _allowedToBuild = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _baseColor = _spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        _mouseWoldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _CellPosition = _grid.WorldToCell(_mouseWoldPosition);
        transform.position = _grid.GetCellCenterWorld(_CellPosition);
        if (_allowedToBuild)
        {
            _spriteRenderer.color = _baseColor;
        }
        else
        {
            _spriteRenderer.color = Color.red;
        }
    }
}
