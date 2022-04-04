using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    Grisha grisha;
    private void Start()
    {
        grisha = gameObject.GetComponentInParent<Grisha>();
        //Debug.Log(grisha.isGrounded);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            grisha.isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            grisha.isGrounded = false;
        }
    }
}
