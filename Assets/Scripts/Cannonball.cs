
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cannonball : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;

    private Vector2 moveDirection = Vector2.left;


    public void SetupDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") || other.GetComponentInParent<PlayerController>() != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Destroy(gameObject);
        }

        else if(other.CompareTag("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Ground"))

        {
            Destroy(gameObject);
        }
    }
    
}
