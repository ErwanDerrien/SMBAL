using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Player
    [SerializeField] private Transform spawn;
    private Rigidbody2D _player;
    private int _deathLvl3 = 0;

    // Key
    [SerializeField] private GameObject key;
    private bool _helpKey;
    
    // Message
    [SerializeField] private GameObject message;
    private bool _helpMsg;
    
    // Mouvement related variables
    public int lateralForce;
    public int verticalForce;

    //Verification related variables
    public float verificationRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    private int _facingRight = 1;
    
    // Ground related variables
    private bool _touchingGround;
    public Transform groundedVerification;

    // Wall related varialbes
    private bool _touchingWall;
    public Transform wallVerification;
    private bool _wallSliding;
    public float wallSlidingSpeed;
    public float wallSlideTime;

    // Walljumping related variables
    private bool _wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;
    
    void Start()
    {
        _player = GetComponent<Rigidbody2D>();
        _helpKey = false;
        _helpMsg = false;
    }

    void Update()
    {
        // Deplacement
        float input = Input.GetAxisRaw("Horizontal");
        _player.velocity = new Vector2(input * lateralForce, _player.velocity.y);

        // Changement de direction
        if ((_facingRight == -1 && input > 0) || (_facingRight == 1 && input < 0))
        {
            Flip();
        } 
        
        // Sauts
        _touchingGround = Physics2D.OverlapCircle(groundedVerification.position, verificationRadius, whatIsGround);
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && _touchingGround)
        {
            GameManager.PlaySound("jump");
            _player.velocity = Vector2.up * verticalForce;
        }
        
        // Wall Sliding
        _touchingWall = Physics2D.OverlapCircle(wallVerification.position, verificationRadius, whatIsWall);
        
        if (_touchingWall && !_touchingGround) //&& input != 0 
        {
            _wallSliding = true;
            Invoke("SetWallSlidingFalse", wallSlideTime);
        }

        if (_wallSliding)
        {
            _player.velocity = new Vector2(_player.velocity.x,
                Mathf.Clamp(_player.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        
        //Walljumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && _wallSliding)
        {
            GameManager.PlaySound("jump");
            _wallJumping = true;
            Invoke("SetWallJumpingFalse", wallJumpTime);
        }

        if (_wallJumping) 
        {
            _player.velocity = new Vector2(xWallForce * -_facingRight, yWallForce);
        }
    }

    private void Flip()
    {
        _facingRight = _facingRight * -1;
        var transform1 = transform;
        Vector3 scaler = transform1.localScale;
        scaler.x *= -1;
        transform1.localScale = scaler;
    }

    private void SetWallJumpingFalse()
    {
        _wallJumping = false;
    }

    private void SetWallSlidingFalse()
    {
        _wallSliding = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = other.transform;
        }
        
        if (other.gameObject.CompareTag("Obstacles"))
        {
            GameManager.PlaySound("death");
            gameObject.transform.position = spawn.position; 
            GameManager.GetInstance().IncrementDeaths();
            if (GameManager.GetInstance().GetStageCount() == 3)
                _deathLvl3++;
            if(_deathLvl3 > 5 && !_helpKey)
            {
                Instantiate(key, new Vector3(-10f, 0f, 0f), Quaternion.identity);
                _helpKey = true;
            }
        }

        if (other.gameObject.CompareTag("Door"))
        {
            if (GameManager.GetInstance().GETKey())
            {
                GameManager.GetInstance().IncrementStage();
                // Changing stage
                switch (GameManager.GetInstance().GetStageCount())
                {
                    case 2:
                        SceneManager.LoadScene("RezDeChaussee");
                        break;
                    case 3:
                        SceneManager.LoadScene("Pont");
                        //If current stage is the 3rd floor, make it so key is required
                        GameManager.GetInstance().SetKey(false);
                        Debug.Log("The key is now false : " +GameManager.GetInstance().GETKey());
                        break;
                    case 4:
                        SceneManager.LoadScene("EcranFinal");
                        break;
                }
            }
            else
            {
                if (!_helpMsg)
                {
                    Instantiate(message, new Vector3(235f, 20f, 0f), Quaternion.identity);
                    _helpMsg = true;
                }
             
                Debug.Log("manque la clé");
            }
        }

        if (other.gameObject.CompareTag("Key"))
        {
            GameManager.GetInstance().SetKey(true);
            Debug.Log("The key is now true : " +GameManager.GetInstance().GETKey());
            Destroy(GameObject.FindWithTag("Key"));
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }

        if (other.gameObject.CompareTag("Door") && GameManager.GetInstance().GetStageCount() == 3)
        {
            Destroy(GameObject.FindWithTag("MsgHelp"));
            _helpMsg = false;
        }
    }
}
