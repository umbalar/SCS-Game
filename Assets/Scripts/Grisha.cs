using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Grisha : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float ClimbingSpeed = 3f;
    [SerializeField] private float baseJumpForce = 10f;
    [SerializeField] private float strongJumpForce = 10f;
    [SerializeField] private float waitTime = 3f;
    private Rigidbody2D rb;
    public bool isGrounded;
    public bool isRuning;
    public bool isClimbing;
    private Vector2 climbingDirection;
    private Vector2 runDirection;
    public float baseGravityScale;
    private Animator animator;

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
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        runDirection.x = 1;
        climbingDirection.y = 1;
        isRuning = true;
        animator.SetBool("isRunnig", false);
        baseGravityScale = rb.gravityScale;
        StartCoroutine(StartWait());
        
    }

    public void Jump(float jumpForce)
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
            Jump(baseJumpForce);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "StrongJumpCoin")
        {
            Debug.Log("StrongJump");
            Jump(strongJumpForce);
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
        else if (collision.gameObject.tag == "Door")
        {
            Debug.Log("Door");
            collision.gameObject.GetComponent<Door>().cameraRelocating = true;
            StartCoroutine(Wait(collision.gameObject));
        }
        else if (collision.gameObject.tag == "LevelEnd")
        {
            Debug.Log("LevelEnd");
            collision.gameObject.GetComponent<LevelEnd>().LoadNextScene();
        }
        else if (collision.gameObject.tag == "KillTiles")
        {
            Debug.Log("Killed");
            Destroy(gameObject);
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
                    animator.SetBool("isClimbing", true);
                    isRuning = false;
                    animator.SetBool("isRunnig", false);
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
                animator.SetBool("isClimbing", false);
                isRuning = true;
                animator.SetBool("isRunnig", true);
                rb.gravityScale = baseGravityScale;
            }
        }
    }

    private IEnumerator Wait(GameObject door)
    {
        Debug.Log("Wait");
        isRuning = false;
        animator.SetBool("isRunnig", false);
        yield return new WaitForSeconds(waitTime);
        isRuning = true;
        animator.SetBool("isRunnig", true);
        Destroy(door);
    }

    private IEnumerator StartWait()
    {
        Debug.Log("StartWait");
        isRuning = false;
        animator.SetBool("isRunnig", false);
        yield return new WaitForSeconds(waitTime);
        isRuning = true;
        animator.SetBool("isRunnig", true);
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
