using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    GameObject grisha;
    Grisha grishaSqript;
    private Grid grid;
    public float baseGravityScale;

    void Start()
    {
        grisha = GameObject.FindGameObjectWithTag("Player");
        grishaSqript = grisha.GetComponent<Grisha>();
        grid = GetComponentInParent<Grid>();
        transform.position = grid.GetCellCenterWorld(grid.WorldToCell(transform.position));
        //baseGravityScale = grisha.GetComponent<Rigidbody2D>().gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Wall");
            //Debug.Log("G");
            //Debug.Log(grisha.GetComponent<Collider2D>().bounds.min.y);
            //Debug.Log("W");
            //Debug.Log(collision.collider.bounds.max.y);
            //if (baseGravityScale != 0f)
            //{
            baseGravityScale = grishaSqript.GetComponent<Grisha>().baseGravityScale;
            //}
            if ((collision.collider.bounds.min.y < GetComponent<Collider2D>().bounds.max.y && baseGravityScale > 0) || (collision.collider.bounds.max.y > GetComponent<Collider2D>().bounds.min.y && baseGravityScale < 0))
            {
                Debug.Log("off gravity");
                grisha.GetComponent<Rigidbody2D>().gravityScale = 0f;
                grishaSqript.isRuning = false;
                grishaSqript.climbingTilesAtached ++;
            }
            //else
            //{
            //    grishaSqript.isRuning = true;
            //    grishaSqript.isClimbing = false;
            //    grisha.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
            //}
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            if (grishaSqript.climbingTilesAtached > 0)
            {
                grishaSqript.climbingTilesAtached--;
                if (grishaSqript.climbingTilesAtached == 0)
                {
                    grishaSqript.isRuning = true;
                    Debug.Log("on gravity");
                    grisha.GetComponent<Rigidbody2D>().gravityScale = baseGravityScale;
                }
            }
        }
    }
}
