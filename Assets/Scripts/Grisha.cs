using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Grisha : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _ñlimbingSpeed = 3f;
    [SerializeField] private float _baseJumpForce = 10f;
    [SerializeField] private float _strongJumpForce = 10f;
    [SerializeField] private float _waitTime = 3f;
    [SerializeField] public float _fallGravityScale = 30f;
    public bool _levelComplete;
    private Rigidbody2D _rigidbody;
    public bool _isGrounded;
    public bool _isRuning;
    public bool _isClimbing;
    private Vector2 _climbingDirection;
    private Vector2 _runDirection;
    public float _baseGravityScale;
    private enum charackterStates { runing, climbing, idle}
    private Animator _animator;
    //private Vector2 _normal;

    private void Run()
    {
        if (_isRuning)
        {
            //Vector2 directionsAlongSurface = _runDirection.normalized - Vector2.Dot(_runDirection.normalized, _normal) * _normal;
            //Vector2 offset = directionsAlongSurface * (_speed * Time.deltaTime);
            //_rigidbody.MovePosition(_rigidbody.position + offset);
            //Debug.Log(_normal);
            //_rigidbody.velocity = directionsAlongSurface * _speed;
            //if (_isGrounded)
            //{
            //    _rigidbody.gravityScale = _baseGravityScale;
            //}
            //else
            //{
            //    _rigidbody.gravityScale = _fallGravityScale * _rigidbody.gravityScale / Mathf.Abs(_rigidbody.gravityScale);
            //}
            transform.Translate(_runDirection.normalized * _speed);
        }
    }

    private void Climbing()
    {
        if (_isClimbing)
        {
            transform.Translate(_climbingDirection.normalized * _ñlimbingSpeed);
        }
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _runDirection.x = 1;
        _climbingDirection.y = 1;
        _isRuning = true;
        _animator.SetBool("isRunnig", false);
        _baseGravityScale = _rigidbody.gravityScale;
        StartCoroutine(StartWait());
        _levelComplete = false;


    }

    public void Jump(float jumpForce)
    {
        if (_isGrounded)
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
        //Debug.Log(collision.gameObject.name);
        //_normal = collision.contacts[0].normal;
        //Debug.Log(_normal);
        if (collision.gameObject.tag == "ClimbingTiles")
        {
            if (!_isClimbing)
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
                    
                    _isClimbing = true;
                    _animator.SetBool("isClimbing", true);
                    _isRuning = false;
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
        if (collision.gameObject.tag == "ClimbingTiles")
        {
            if (_isClimbing)
            {
                Debug.Log("Climbing off");
                _isClimbing = false;
                _animator.SetBool("isClimbing", false);
                _isRuning = true;
                _animator.SetBool("isRunnig", true);
                _rigidbody.gravityScale = _baseGravityScale;
                //_normal = new Vector2(0f, 1f);
            }
        }
    }

    private IEnumerator Wait(GameObject door)
    {
        Debug.Log("Wait");
        _isRuning = false;
        //_rigidbody.velocity = Vector2.zero;
        _animator.SetBool("isRunnig", false);
        yield return new WaitForSeconds(_waitTime);
        _isRuning = true;
        _animator.SetBool("isRunnig", true);
        Destroy(door);
    }

    private IEnumerator StartWait()
    {
        Debug.Log("StartWait");
        _isRuning = false;
        _animator.SetBool("isRunnig", false);
        yield return new WaitForSeconds(_waitTime);
        _isRuning = true;
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
