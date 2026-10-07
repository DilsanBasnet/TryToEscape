using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
   [SerializeField] private int coinValue = 1;
   private string coinID;

   private void Start()
    {
        coinID = SceneManager.GetActiveScene().name + "_" + transform.position.ToString();

        if(CoinManagerScript.Instance != null && CoinManagerScript.Instance.IsCoinCollected(coinID))
        {
            Destroy(gameObject);
        }
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
           if(CoinManagerScript.Instance != null)
            {
                CoinManagerScript.Instance.RegisterCollectedCoin(coinID);
            }

            if(GameManager.Instance != null)
            {
                GameManager.Instance.AddCoin(coinValue);
            }

            if(AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.coinCollectSFX);
            }

            Destroy(gameObject);
        }
    }

    
}
