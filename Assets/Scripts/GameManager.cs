using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
    
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private Text legacyCoinText;
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
        GameObject textObj = GameObject.Find("CoinsText") ?? GameObject.Find("CoinText");

        if(textObj != null)
        {
            coinText = textObj.GetComponent<TMP_Text>();

            if(coinText == null)
            {
                legacyCoinText = textObj.GetComponent<Text>();
            }
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
            coinText.text = "Coins : "  + totalCoins;
        }
         else if(legacyCoinText != null)
        {
            legacyCoinText.text = "Coins : "  + totalCoins;
        }
    }

}
