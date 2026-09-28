using UnityEngine;

public class MovingPlatformsScript : MonoBehaviour
{
   [SerializeField] private Transform pointA;
   [SerializeField] private Transform pointB;
   [SerializeField] private float speed = 3f;
   [SerializeField] private float waitTimeAtPoint = 0.5f;


   private Vector3 targetPosition;
   private float waitTimer;
   private bool isWaiting;

   private void Start()
    {
        if(pointA != null)
        {
            targetPosition = pointA.position;
        }
    }
    private void Update()
    {
        if(pointA == null || pointB == null)  return;

        if(isWaiting)
        {
            waitTimer += Time.deltaTime;

            if(waitTimer >= waitTimeAtPoint)
            {
                isWaiting = false;
                waitTimer = 0f;
            }
             return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
            isWaiting = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.contacts[0].normal.y < -0.5f)
            {
                collision.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        if(pointA != null && pointB != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawWireSphere(pointA.position, 0.3f);
            Gizmos.DrawWireSphere(pointB.position, 0.03f);
        }
    }
}
