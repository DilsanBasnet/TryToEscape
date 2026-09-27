using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
  [SerializeField] private bool rotateObstacle = true;
  [SerializeField] private float rotationSpeed = 200f;

  [SerializeField] private float respawnDelay = 0.5f;
  private bool isPlayerDying = false;

  

    private void Update(){
        
        if(rotateObstacle) {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

private IEnumerator HandleRespawn(GameObject player)
    {
        isPlayerDying = true;

        PlayerController controller = player.GetComponent<PlayerController>();

        if(controller != null)
        {
            controller.enabled = false;
        }

        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();

        if(playerSprite != null)
        {
            playerSprite.enabled = false;
        }

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if(rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }
        yield return new WaitForSeconds(respawnDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isPlayerDying) return;

        if(other.CompareTag("Player") || other.GetComponent<PlayerController>() != null)
        {
            StartCoroutine(HandleRespawn(other.gameObject));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isPlayerDying) return;

        if(collision.gameObject.CompareTag("Player") || collision.gameObject.GetComponent<PlayerController>() != null)
        {
            StartCoroutine(HandleRespawn(collision.gameObject));
        }
    }
    

}
