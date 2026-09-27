using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
   [SerializeField] private string NextScene;
   private bool isTransitioning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isTransitioning) return;

        if(other.CompareTag("Player") || other.GetComponent<PlayerController>()  != null)
        {

            isTransitioning = true;

            Time.timeScale = 1f;

            if(!string.IsNullOrEmpty(NextScene))
            {
                SceneManager.LoadScene(NextScene);
            }
            else
            {
                int currentScene = SceneManager.GetActiveScene().buildIndex;
                int nextSceneIndex = currentScene + 1;

                if(nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(nextSceneIndex);
                }
                else
                {
                Debug.LogWarning("Portal, No next Scene");
                }


            }
        }
        
    }
}
