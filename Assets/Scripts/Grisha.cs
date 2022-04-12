using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grisha : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 10f;
    private Rigidbody2D rb;
    public bool isGrounded;
    private Vector2 runDirection;

    private void Awake()
    {

    }

    private void Run()
    {
        //Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        transform.Translate(runDirection.normalized * speed);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        runDirection.x = 1;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            //Debug.Log(isGrounded);
            if (rb.gravityScale > 0)
            {
                rb.AddForce(Vector2.up * jumpForce);
            }
            else
            {
                rb.AddForce(Vector2.down * jumpForce);
            }
        }
    } 

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        Run();
    }
}
