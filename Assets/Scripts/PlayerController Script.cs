using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour{
    public float speed = 7f;
    public float jump = 11f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector2 groundChecksize = new Vector2(0.5f, 0.1f);
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float move;
    private bool Grounded;
    private bool isBoosted = false;

    void Start(){
        rb = GetComponent<Rigidbody2D> () ;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>() ;
    }

    void Update()
    {
        move = 0f;
        if(Keyboard.current != null)
        {
            if(Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            move = -1f;

            else if(Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            move = 1f;
        }

        if (spriteRenderer != null)
        {
            if(move > 0) 
            spriteRenderer.flipX = false;

            else if (move < 0) 
            spriteRenderer.flipX = true;
        }
        if(groundCheck != null)
        {
            Grounded = Physics2D.OverlapBox(groundCheck.position, groundChecksize, 0f, groundLayer);

        }

        bool jumpPressed = Keyboard.current != null && (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame);

        if(jumpPressed && Grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
        }
    }

    void FixedUpdate()
    {
        if(!isBoosted)
        {
            rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        if(groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(groundCheck.position, groundChecksize);
        }
    }

    public void ApplySpeedBoost(Vector2 boostVelocity, float duration)
    {
        if(!isBoosted)
        {
            StartCoroutine(SpeedBoostRoutine(boostVelocity, duration));
        }
    }

    private System.Collections.IEnumerator SpeedBoostRoutine(Vector2 boostVelocity, float duration)
    {
        isBoosted = true;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if(rb != null)
        {
            rb.linearVelocity = boostVelocity;
        }

        yield return new WaitForSeconds(duration);
        isBoosted = false;
    }
}
