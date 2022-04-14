using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grisha : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 3f;
    [SerializeField] private float ClimbingSpeed = 3f;
    [SerializeField] private float jumpForce = 10f;
    private Rigidbody2D rb;
    public bool isGrounded;
    public bool isRuning;
    public int climbingTilesAtached;
    private Vector2 climbingDirection;
    private Vector2 runDirection;
    public float baseGravityScale;

    private void Awake()
    {

    }

    private void Run()
    {
        //Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        if (isRuning)
        {
            transform.Translate(runDirection.normalized * speed);
        }
    }

    private void Climbing()
    {
        if (climbingTilesAtached > 0)
        {
            //rb.gravityScale = 0f;
            transform.Translate(climbingDirection.normalized * ClimbingSpeed);
        }
        else
        {
            //rb.gravityScale = baseGravityScale;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        runDirection.x = 1;
        climbingDirection.y = 1;
        isRuning = true;
        climbingTilesAtached = 0;
        baseGravityScale = rb.gravityScale;
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
        Climbing();
    }
}
