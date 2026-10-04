using System.Collections;
using UnityEngine;

public class TriggerTrap : MonoBehaviour
{
   [SerializeField] private Transform platformToDrop;
   [SerializeField] private Vector3 targetOffset = new Vector3(0f, -15f, 0f);
   [SerializeField] private float dropSpeed = 80f;

   private Vector3 targetPosition;
   private bool hasTriggered = false;

   private void Start()
    {
        if(platformToDrop != null)
        {
            targetPosition = platformToDrop.position + targetOffset;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(hasTriggered) return;

        PlayerController player = other.GetComponent<PlayerController>();
        
        if(player == null) player = other.GetComponentInParent<PlayerController>();
        
        if(player != null & platformToDrop != null)
        {
            hasTriggered= true;
            StartCoroutine(DropPlatformRoutine());
        }    }

        private IEnumerator DropPlatformRoutine()
    {
        while(Vector3.Distance(platformToDrop.position, targetPosition) > 0.05f)
        {
            platformToDrop.position = Vector3.MoveTowards(platformToDrop.position, targetPosition, dropSpeed * Time.deltaTime);

            yield return null;
        }

        platformToDrop.position = targetPosition;
    }

    private void OnDrawGizmosSelected()
    {
        if(platformToDrop != null)
        {
            Gizmos.color = Color.red;
            Vector3 endPos = Application.isPlaying ? targetPosition : platformToDrop.position + targetOffset;
            Gizmos.DrawWireCube(endPos, platformToDrop.localScale);
            Gizmos.DrawLine(platformToDrop.position, endPos);
        }
    }
}
