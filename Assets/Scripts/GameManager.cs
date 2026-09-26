using TMPro;
using UnityEngine;

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
        }
         else
        {
            Destroy(gameObject);
        }
    }

    private void UpdateCoinUI()
    {
        if(coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
    }

    private void Start()
    {
        UpdateCoinUI() ;
    }

    public void AddCoin(int amount = 1)
    {
        coinCount += amount;
        UpdateCoinUI();
    }

    
}
