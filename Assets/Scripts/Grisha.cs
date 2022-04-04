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

    private void Awake()
    {

    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        //sprite.flipX = dir.x < 0.0f;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
    }
}
