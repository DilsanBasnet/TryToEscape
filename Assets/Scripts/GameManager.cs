using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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
       if(coinText == null)
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

}
