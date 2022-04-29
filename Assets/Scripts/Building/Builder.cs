using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Builder : MonoBehaviour
{
    public BuildObjectSelecter _activeButtonSqcript;
    private Camera _mainCamera;
    private List<Tilemap> _tilemaps;
    [SerializeField] private TileBase _tileBase;
    [SerializeField] private TileBase _climbingTile;
    [SerializeField] private TileBase _climbingTileReverse;
    void Start()
    {
        _mainCamera = Camera.main;
        _tilemaps = FillTilemapList(_tilemaps);
    }
    //void FixedUpdate()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector3 clickWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

    //        Ray ray = _mainCamera.ScreenPointToRay(clickWorldPosition);
    //        Debug.Log(clickWorldPosition);
    //        Debug.Log(Physics2D.Raycast(clickWorldPosition, new Vector2(clickWorldPosition.x, clickWorldPosition.y)).collider.gameObject.name);
    //        //Debug.DrawRay()
    //        if (Physics2D.Raycast(_mainCamera.transform.position, new Vector2(clickWorldPosition.x, clickWorldPosition.y)))
    //        {
    //            Debug.Log("Something hit");
    //        }
    //    }
    //}
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 clickWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //    Vector3Int clickCellPosition = _tilemaps[0].WorldToCell(clickWorldPosition);
        //    Vector2 vector2 = _tilemaps[0].CellToWorld(clickCellPosition);
        //    Vector2 offset = new Vector2(0.5f, 0.5f);
        //    RaycastHit2D raycastHit2D = Physics2D.Raycast(vector2 + offset, Vector2.down, 1f);
        //    if (raycastHit2D)
        //    {
        //        Debug.Log(raycastHit2D.collider.gameObject.GetComponent<Tilemap>());
        //        Debug.Log("DownHit");
        //    }
        //    else
        //    {
        //        Debug.Log("NotDownHit");
        //    }

        //}
        if (Input.GetKeyDown(KeyCode.Mouse1) && _activeButtonSqcript != null)
        {
            Destroy(_activeButtonSqcript._buildMarker);
            _activeButtonSqcript = null;
        }
        if (Input.GetMouseButtonDown(0) && _activeButtonSqcript != null)
        {
            
            Vector3 clickWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickCellPosition = _tilemaps[0].WorldToCell(clickWorldPosition);
            

            if (CellFreeFromTiles(clickCellPosition))//Нужна еще проверка на отсутствие триггера в клетке
            {
                
                if (_activeButtonSqcript._objectToBuild is TileBase)
                {
                    TileBase tileToBuild = (TileBase)_activeButtonSqcript._objectToBuild;
                    if (tileToBuild == _climbingTile)
                    {
                        Vector2 vector2 = _tilemaps[0].CellToWorld(clickCellPosition);
                        Vector2 offset = new Vector2(0.5f, 0.5f);
                        RaycastHit2D raycastHit2D = Physics2D.Raycast(vector2 + offset, Vector2.down, 1f);
                        if (raycastHit2D && raycastHit2D.collider.gameObject.layer == 8)
                        {
                            //tileToBuild = _climbingTileReverse;
                        }
                        else
                        {
                            raycastHit2D = Physics2D.Raycast(vector2 + offset, Vector2.up, 1f);
                            if (raycastHit2D && raycastHit2D.collider.gameObject.layer == 8)
                            {
                                tileToBuild = _climbingTileReverse;
                            }
                            
                        }
                    }
                    if (tileToBuild.Equals(_tileBase))
                    {
                        _tilemaps[0].SetTile(clickCellPosition, tileToBuild);
                    }
                    else
                    {
                        _tilemaps[1].SetTile(clickCellPosition, tileToBuild);
                    }
                    _activeButtonSqcript._counter--;
                    _activeButtonSqcript.gameObject.GetComponentInChildren<Text>().text = _activeButtonSqcript._counter.ToString();
                    if (_activeButtonSqcript._counter <= 0)
                    {
                        Destroy(_activeButtonSqcript._buildMarker);
                        _activeButtonSqcript = null;
                    }
                    
                }
                else
                {
                    GameObject triggerToBuild = (GameObject)_activeButtonSqcript._objectToBuild;
                    triggerToBuild =  Instantiate(triggerToBuild, _tilemaps[0].GetCellCenterWorld(clickCellPosition), new Quaternion() , GameObject.FindGameObjectWithTag("Triggers").transform);
                    Vector2 vector2 = _tilemaps[0].CellToWorld(clickCellPosition);
                    Vector2 offset = new Vector2(0.5f, 0.5f);
                    RaycastHit2D raycastHit2D = Physics2D.Raycast(vector2 + offset, Vector2.down, 1f);
                    if (raycastHit2D && raycastHit2D.collider.gameObject.layer == 8)
                    {

                    }
                    else
                    {
                        raycastHit2D = Physics2D.Raycast(vector2 + offset, Vector2.up, 1f);
                        if (raycastHit2D && raycastHit2D.collider.gameObject.layer == 8)
                        {
                            triggerToBuild.transform.Rotate(transform.rotation.x + 180, 0, 0, Space.Self);
                        }

                    }
                    _activeButtonSqcript._counter--;
                    _activeButtonSqcript.gameObject.GetComponentInChildren<Text>().text = _activeButtonSqcript._counter.ToString();
                    if (_activeButtonSqcript._counter <= 0)
                    {
                        Destroy(_activeButtonSqcript._buildMarker);
                        _activeButtonSqcript = null;
                    }
                        
                }
            }
            else
            {
                Debug.Log("Tile already set");
            }
        }

    }

    private bool CellFreeFromTiles(Vector3Int clickCellPosition)
    {
        return !_tilemaps[0].HasTile(clickCellPosition) && !_tilemaps[1].HasTile(clickCellPosition) && !_tilemaps[2].HasTile(clickCellPosition);
    }
    private bool CellFreeFromTriggers(Vector3 clickWorldPosition) //не рабатает
    {
        Ray ray = _mainCamera.ScreenPointToRay(clickWorldPosition);
        if (Physics.Raycast(ray))
        {
            Debug.Log("Something hit");
        }
        return true;
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
