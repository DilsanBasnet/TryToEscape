using UnityEngine;

public class Coin : MonoBehaviour
{
   [SerializeField] private int coinValue = 1;
   private string coinID;

   private void Start()
    {
        coinID = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + "_" + transform.position.ToString() ;

        if(CoinManagerScript.Instance != null && CoinManagerScript.Instance.IsCoinCollected(coinID))
        {
            Destroy(gameObject);
        }
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(GameManager.Instance != null)
            {
                CoinManagerScript.Instance.RegisterCollectedCoin(coinID);
            }

            if(GameManager.Instance != null)
            {
                GameManager.Instance.AddCoin(coinValue);
            }

            Destroy(gameObject);
        }
    }
}
