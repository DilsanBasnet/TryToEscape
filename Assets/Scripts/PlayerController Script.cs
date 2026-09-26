using UnityEngine;

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

    void Start(){
        rb = GetComponent<Rigidbody2D> () ;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>() ;
    }

    void Update()
    {
        move = Input.GetAxisRaw("Horizontal"); 

        if(spriteRenderer != null)
        {
            if(move > 0) {
            spriteRenderer.flipX = false;
        }
        else if(move < 0){
            spriteRenderer.flipX = true;
        } 

        }
       

        if(groundCheck != null) {
            Grounded = Physics2D.OverlapBox(groundCheck.position, groundChecksize, 0f, groundLayer);
        }

        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && Grounded){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
        }   
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if(groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(groundCheck.position, groundChecksize);
        }
    }
}
