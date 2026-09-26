using UnityEngine;

public class Coin : MonoBehaviour
{
   [SerializeField] private int coinValue = 1;

   private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") || other.GetComponent<PlayerController>() != null)
        {
            if(GameManager.Instance != null)
            {
                GameManager.Instance.AddCoin(coinValue);
            }

            Destroy(gameObject);
        }
    }
}
