using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
  [SerializeField] private bool rotateObstacle = true;
  [SerializeField] private float rotationSpeed = 200f;

    private void Update(){
        
        if(rotateObstacle) {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
    private void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("Player") || other.GetComponent<PlayerController>() != null) {
            
            RestartLevel() ;
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision){
        
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.GetComponent<PlayerController>() != null)  {

            RestartLevel();
        }
    }


}
