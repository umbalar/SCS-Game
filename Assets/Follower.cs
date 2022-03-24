using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float expectedFollowRange = 3;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    GameObject grisha;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Run()
    {
        Vector3 dir = grisha.transform.position;
        if (Vector3.Distance(dir, transform.position) > expectedFollowRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, dir, speed * Time.deltaTime);
        }

        //sprite.flipX = dir.x < 0.0f;
    }

    void Start()
    {
        grisha = GameObject.Find("Grisha");
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }
}
