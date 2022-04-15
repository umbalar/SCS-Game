using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TriggerChest : MonoBehaviour
{
    private GameObject grisha;
    //private Camera mainCamera;
    private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        //mainCamera = Camera.main;
        grisha = GameObject.FindGameObjectWithTag("Player");
        grid = GetComponentInParent<Grid>();
        transform.position = grid.GetCellCenterWorld(grid.WorldToCell(transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 clickWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //    Vector3Int clickCellPosition = grid.WorldToCell(clickWorldPosition);
        //    Debug.Log("click");
        //    Debug.Log(clickCellPosition);
        //}
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //collision = grisha.GetComponent<Collider2D>();
    //    grisha.GetComponent<Grisha>().Jump();
    //    Debug.Log("Jump");
    //    Destroy(gameObject);
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    collision = grisha.GetComponent<TilemapCollider2D>();
    //    grisha.Jump();
    //}

}
