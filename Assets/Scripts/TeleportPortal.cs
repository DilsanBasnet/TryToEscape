using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class TeleportPortal : MonoBehaviour
{
  [SerializeField] private Transform  destinationPortal;
  [SerializeField] private Vector2 exitOffset = Vector2.zero;
    [SerializeField] private float cooldownTime = 0.5f;

  private static bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(destinationPortal == null || isTeleporting) return;

        if(other.CompareTag("Player") || other.GetComponentInParent<PlayerController>() != null)
        {
            Transform playerTransform = other.transform.root;
            StartCoroutine(TeleportRoutine(playerTransform.gameObject));
        }}

        private IEnumerator TeleportRoutine(GameObject player)
    {
        isTeleporting = true;

        Vector3 targetPos = destinationPortal.position + (Vector3)exitOffset;
        targetPos.z = player.transform.position.z;
       
       Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

       if(rb != null)
        {
            rb.interpolation = RigidbodyInterpolation2D.None;
            rb.angularVelocity = 0f;
            rb.linearVelocity = Vector2.zero;
        }
        player.transform.position = targetPos;

        Transform spritechild = player.transform.Find("PlayerSprite");

        if(spritechild != null)
        {
            spritechild.localPosition = Vector3.zero;
        }

        Physics2D.SyncTransforms();

        CinemachineCamera vcam = FindAnyObjectByType<CinemachineCamera>();
        if(vcam != null)
        {
            vcam.ForceCameraPosition(targetPos, Quaternion.identity);
        }

        if(rb != null)
        {
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }


        
        yield return new WaitForSeconds(cooldownTime);

        isTeleporting = false;

        
    }

    private void OawGizmos()
    {
        
 if(destinationPortal != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, destinationPortal.position);
               Gizmos.DrawWireSphere(destinationPortal.position + (Vector3)exitOffset, 0.2f);
        }   }


}
