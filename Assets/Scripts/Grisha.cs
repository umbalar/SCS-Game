using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grisha : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 3f;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        //sprite.flipX = dir.x < 0.0f;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
    }
}