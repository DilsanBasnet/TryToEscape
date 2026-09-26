using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
   [SerializeField] private string NextScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") || other.GetComponent<PlayerController>()  != null)
        {
            if(!string.IsNullOrEmpty(NextScene))
            {
                SceneManager.LoadScene(NextScene);
            }
            else
            {
                int currentScene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentScene + 1);
            }
        }
        
    }
}
