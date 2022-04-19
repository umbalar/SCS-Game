using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Grisha _grisha;
    private void Start()
    {
        _grisha = gameObject.GetComponentInParent<Grisha>();
        //Debug.Log(grisha.isGrounded);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            if (_grisha._isGrounded != true)
            {
                Debug.Log(true);
            }
            _grisha._isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            _grisha._isGrounded = false;
            Debug.Log(false);
        }
    }
}
