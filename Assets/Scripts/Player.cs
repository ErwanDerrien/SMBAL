using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player
    [SerializeField] private GameObject playerPrefab; 
    [SerializeField] private Transform spawn;
    private Rigidbody2D player;
    public int nbDead;
   // private Transform _transformStart;

    // Mouvement related variables
    public int lateralForce;
    public int verticalForce;

    //Verification related variables
    public float verificationRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    private int facingRight = 1;
    
    // Ground related variables
    private bool touchingGround;
    public Transform groundedVerification;

    // Wall related varialbes
    private bool touchingWall;
    public Transform wallVerification;
    private bool wallSliding;
    public float wallSlidingSpeed;
    public float wallSlideTime;

    // Walljumping related variables
    private bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;
    
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Deplacement
        float input = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(input * lateralForce, player.velocity.y);

        // Changement de direction
        if ((facingRight == -1 && input > 0) || (facingRight == 1 && input < 0))
        {
            Flip();
        } 
        
        // Sauts
        touchingGround = Physics2D.OverlapCircle(groundedVerification.position, verificationRadius, whatIsGround);
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && touchingGround)
        {
            player.velocity = Vector2.up * verticalForce;
        }
        
        // Wall Sliding
        touchingWall = Physics2D.OverlapCircle(wallVerification.position, verificationRadius, whatIsWall);
        
        if (touchingWall && !touchingGround) //&& input != 0 
        {
            wallSliding = true;
            Invoke("SetWallSlidingFalse", wallSlideTime);
        }

        if (wallSliding)
        {
            player.velocity = new Vector2(player.velocity.x,
                Mathf.Clamp(player.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        
        //Walljumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && wallSliding)
        {
            wallJumping = true;
            Invoke("SetWallJumpingFalse", wallJumpTime);
        }

        if (wallJumping) 
        {
            player.velocity = new Vector2(xWallForce * -facingRight, yWallForce);
        }
    }

    private void Flip()
    {
        facingRight = facingRight * -1;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void SetWallJumpingFalse()
    {
        wallJumping = false;
    }

    private void SetWallSlidingFalse()
    {
        wallSliding = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacles"))
        {
            Destroy(gameObject);
            nbDead++;
            respawn();
        }
    }

    void respawn()
    {
        Instantiate(playerPrefab, spawn.position, Quaternion.identity);
    }
    
}
