using UnityEngine;

public class JumpPad : MonoBehaviour
{
   [SerializeField] private float bounceForce = 18f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.contacts[0].normal.y < -0.5f)
            {
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

                if(playerRb != null)
                {
                    playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x,bounceForce);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.jumpPadSFX);
            }
        }
    }
}
