using UnityEngine;

public class SpeedPad : MonoBehaviour
{
   public enum BoostDirection {Right, Left, Up, Down, FacingDirection}

   [SerializeField] private float boostForce = 25f;
   [SerializeField] private BoostDirection direction = BoostDirection.Right;
   [SerializeField] private float BoostDuration = 0.5f;
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if(player == null) 
        player = other.GetComponentInParent<PlayerController>();

        if(player != null)
        {
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();


            if(playerRb != null)
            {
                Vector2 pushVector = GetBoostVector();
                player.ApplySpeedBoost(pushVector * boostForce, BoostDuration);
            }
        }
    }

    private Vector2 GetBoostVector()
    {
        switch(direction)
        {
            case 
            BoostDirection.Right:
            return
            Vector2.right;

            case 
            BoostDirection.Left:
            return
            Vector2.left;

            case 
            BoostDirection.Up:
            return
            Vector2.up;

            case 
            BoostDirection.Down:
            return
            Vector2.down;

            case 
            BoostDirection.FacingDirection:

            default:

            return transform.right;
        }
    }

}
