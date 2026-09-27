using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    [SerializeField] private GameObject pauseMenuPanel;

    [SerializeField] private string mainMenuSceneName = "Main Menu Scene";

    private bool isPaused = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if(scene.name == mainMenuSceneName)
        {
            Destroy(gameObject);
            return;
        }

        Time.timeScale = 1f;
        isPaused = false;

        if(pauseMenuPanel == null)
        {
            pauseMenuPanel = GameObject.Find("PauseMenuPanel");
        }
        if(pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false) ;
        }
        
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    public void Resume()
    {
        if(pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
        Time.timeScale = 1f;
        isPaused = false;
        
    }

    public void Pause()
    {
        if(pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
        }
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void TogglePause()
    {
        if(isPaused)
        {
            Resume();

        }
         else
        {
            Pause();
        }
    }


    private void Update()
    {
        if(Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ExitGame()
    {
        Application.Quit() ;
    }
}
