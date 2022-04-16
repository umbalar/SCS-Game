using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grisha : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float ClimbingSpeed = 3f;
    [SerializeField] private float jumpForce = 10f;
    private Rigidbody2D rb;
    public bool isGrounded;
    public bool isRuning;
    public bool isClimbing;
    private Vector2 climbingDirection;
    private Vector2 runDirection;
    public float baseGravityScale;

    private void Run()
    {
        if (isRuning)
        {
            transform.Translate(runDirection.normalized * speed);
        }
    }

    private void Climbing()
    {
        if (isClimbing)
        {
            transform.Translate(climbingDirection.normalized * ClimbingSpeed);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        runDirection.x = 1;
        climbingDirection.y = 1;
        isRuning = true;
        baseGravityScale = rb.gravityScale;
    }

    public void Jump()
    {
        if (isGrounded)
        {
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

    void FixedUpdate()
    {
        Run();
        Climbing();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "JumpTrigger")
        {
            Debug.Log("Jump");
            Jump();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "GravityReverseTrigger")
        {
            Debug.Log("Grav");
            transform.Rotate(transform.rotation.x + 180, 0, 0, Space.Self);
            rb.gravityScale *= -1;
            baseGravityScale *= -1;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ClimbingTiles")
        {
            if (!isClimbing)
            {
                Debug.Log(collision.gameObject.name);
                float deflectionX = Mathf.Abs(collision.GetContact(0).point.x - collision.GetContact(1).point.x);
                float deflectionY = Mathf.Abs(collision.GetContact(0).point.y - collision.GetContact(1).point.y);
                foreach (ContactPoint2D contactPoint2D in collision.contacts)
                {
                    if (collision.GetContact(0).point.x - contactPoint2D.point.x > deflectionX)
                    {
                        deflectionX = Mathf.Abs(collision.GetContact(0).point.x - contactPoint2D.point.x);
                    }
                    if (collision.GetContact(0).point.y - contactPoint2D.point.y > deflectionX)
                    {
                        deflectionY = Mathf.Abs(collision.GetContact(0).point.y - contactPoint2D.point.y);
                    }
                }
                if (deflectionY > deflectionX)
                {
                    
                    isClimbing = true;
                    isRuning = false;
                    rb.gravityScale = 0f;
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ClimbingTiles")
        {
            if (isClimbing)
            {
                Debug.Log("Climbing off");
                isClimbing = false;
                isRuning = true;
                rb.gravityScale = baseGravityScale;
            }
        }
    }
}
