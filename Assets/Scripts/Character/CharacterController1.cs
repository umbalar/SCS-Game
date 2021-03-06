using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController1 : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _˝limbingSpeed = 3f;
    [SerializeField] private float _baseJumpForce = 10f;
    [SerializeField] private float _strongJumpForce = 10f;
    [SerializeField] private float _waitTime = 3f;
    [SerializeField] public float _fallGravityScale = 30f;
    public bool _levelComplete;
    private Rigidbody2D _rigidbody;
    private bool _onClimbingTiles;
    //public bool _isGrounded;
    //public bool _isRunning;
    //public bool _isClimbing;
    private Vector2 _climbingDirection;
    private Vector2 _runDirection;
    public float _baseGravityScale;
    private enum charackterStates { running, climbing, idle}
    private charackterStates state;
    private Animator _animator;
    //private Vector2 _normal;

    private void Run()
    {
        transform.Translate(_runDirection.normalized * _speed);
    }

    private void Climbing()
    {
        transform.Translate(_climbingDirection.normalized * _˝limbingSpeed);
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _runDirection.x = 1;
        _climbingDirection.y = 1;
        //_isRuning = true;
        //_animator.SetBool("isRunnig", false);
        _baseGravityScale = _rigidbody.gravityScale;
        //StartCoroutine(StartWait());
        _levelComplete = false;
        state = charackterStates.idle;

    }

    public void Jump(float jumpForce)
    {
        if (_rigidbody.gravityScale > 0)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce);
        }
        else
        {
            _rigidbody.AddForce(Vector2.down * jumpForce);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(gameObject);
        }
        if (state == charackterStates.idle && Input.GetKeyDown(KeyCode.Space))
        {
            state = charackterStates.running;
            _animator.SetBool("isRunnig", true);
        }
    }

    void FixedUpdate()
    {
        
        switch (state)
        {
            case charackterStates.running:
                Run();
                break;
            case charackterStates.climbing:
                Climbing();
                break;
            case charackterStates.idle:
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "JumpTrigger")
        {
            Debug.Log("Jump");
            Jump(_baseJumpForce);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "StrongJumpCoin")
        {
            Debug.Log("StrongJump");
            Jump(_strongJumpForce);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "GravityReverseTrigger")
        {
            Debug.Log("Grav");
            transform.Rotate(transform.rotation.x + 180, 0, 0, Space.Self);
            _rigidbody.gravityScale *= -1;
            _baseGravityScale *= -1;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Door")
        {
            Debug.Log("Door");
            _animator.SetBool("isRunnig", false);
            collision.gameObject.GetComponent<Door>()._cameraRelocating = true;
            //StartCoroutine(Wait(collision.gameObject));
            state = charackterStates.idle;
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
        //Debug.Log(collision.gameObject.name);
        //_normal = collision.contacts[0].normal;
        //Debug.Log(_normal);
        if (collision.gameObject.tag == "ClimbingTiles")
        {
            _onClimbingTiles = true;
            if (state != charackterStates.climbing)
            {
                Debug.Log(collision.gameObject.name);
                //Debug.Log(_climbingDirection);
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
                    state = charackterStates.climbing;
                    //_isClimbing = true;
                    _animator.SetBool("isClimbing", true);
                    //_isRunning = false;
                    _animator.SetBool("isRunnig", false);
                    _rigidbody.gravityScale = 0f;
                }
            }
        }
        //else if (collision.gameObject.tag == "Ramp")
        //{
        //    _normal = new Vector2(-0.7f, 0.7f);
        //    //Debug.Log(_normal);
        //}
        //else
        //{
        //    _normal = new Vector2(0f, 1f);
        //}
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _onClimbingTiles = false;
        if (collision.gameObject.tag == "ClimbingTiles")
        {
            if (state == charackterStates.climbing)
            {
                Debug.Log("Climbing off");
                state = charackterStates.running;
                //_isClimbing = false;
                _animator.SetBool("isClimbing", false);
                //_isRunning = true;
                _animator.SetBool("isRunnig", true);
                _rigidbody.gravityScale = _baseGravityScale;
                //_normal = new Vector2(0f, 1f);
            }
        }
    }

    private IEnumerator Wait(GameObject door)
    {
        Debug.Log("Wait");
        state = charackterStates.idle;
        //_isRunning = false;
        //_rigidbody.velocity = Vector2.zero;
        _animator.SetBool("isRunnig", false);
        yield return new WaitForSeconds(_waitTime);
        state = charackterStates.running;
        //_isRunning = true;
        _animator.SetBool("isRunnig", true);
        //Destroy(door);
    }

    private IEnumerator StartWait()
    {
        Debug.Log("StartWait");
        state = charackterStates.idle;
        //_isRunning = false;
        _animator.SetBool("isRunnig", false);
        yield return new WaitForSeconds(_waitTime);
        state = charackterStates.running;
        //_isRunning = true;
        _animator.SetBool("isRunnig", true);
    }

    private void OnDestroy()
    {
        if (!_levelComplete)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
}
