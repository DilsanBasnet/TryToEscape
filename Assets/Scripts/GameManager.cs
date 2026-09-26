using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {

         get; private set;
     }
    
    [SerializeField] private TMP_Text coinText;
    private int coinCount = 0;

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
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject textObj = GameObject.Find("CoinCounterText");

        if(textObj != null)
        {
            coinText = textObj.GetComponent<TMP_Text>();
        }
        UpdateCoinUI();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
     }

     private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void UpdateCoinUI()
    {
        if(coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
    }
     public void AddCoin(int amount = 1 )
    {
        coinCount += amount;
        UpdateCoinUI() ;
        
    }   
}
