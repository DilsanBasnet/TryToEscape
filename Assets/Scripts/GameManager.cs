using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
    
    [SerializeField] private TMP_Text coinText;
    public int totalCoins = 0;

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
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;

        if(scene.name == "Main Menu Scene")
        {
            totalCoins = 0;
       Destroy(gameObject);
       return;
        }
        FindCoinTextUI() ;
        UpdateCoinUI();
       
    }

    private void FindCoinTextUI()
    {
        GameObject coinObj = GameObject.Find("CoinText");
        if(coinObj == null)
        {
            coinObj = GameObject.Find("CoinsText");

        }
        if(coinObj != null)
        {
            coinText = coinObj.GetComponent<TMP_Text>();
        }

       else if(coinText == null)
        {
            coinText = FindAnyObjectByType<TMP_Text>() ;
        }
    }

    public void AddCoin(int value)
    {
        totalCoins += value;
        UpdateCoinUI();
    }

    public void UpdateCoinUI()
    {
        if(coinText != null)
        {
           FindCoinTextUI();
        }
        if(coinText != null)
        {
            coinText.text = "Coins : " + totalCoins;
        }
    }

    public void RestartCurrentLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu Scene");
    }

}
