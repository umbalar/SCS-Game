using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitRreverse : MonoBehaviour
{
    private GameObject grisha;
    private Grid grid;
    void Start()
    {
        
        grisha = GameObject.FindGameObjectWithTag("Player");
        grid = GetComponentInParent<Grid>();
        transform.position = grid.GetCellCenterWorld(grid.WorldToCell(transform.position));
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //grisha.transform.localScale.Scale(Vector3.down);
        grisha.transform.Rotate(grisha.transform.rotation.x + 180, 0, 0, Space.Self);
        //Debug.Log(grisha.GetComponent<Rigidbody2D>().gravityScale);
        grisha.GetComponent<Rigidbody2D>().gravityScale *= -1;
        Debug.Log("Grav");
        Destroy(gameObject);
    }
}
