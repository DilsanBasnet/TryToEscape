
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public enum FireDirections {Left, Right, Up, Down, Random360}

    [SerializeField] private GameObject cannonballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float initialDelay = 0.5f;
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private FireDirections direction = FireDirections.Right;
    [SerializeField] private float spreadAngle = 41f;

    private void Start()
    {
        StartCoroutine(AutoFireRoutine());
    }



    private IEnumerator AutoFireRoutine()
    {
        yield return new WaitForSeconds(initialDelay);

        while(true)
        {
           Fire();
           yield return new WaitForSeconds(fireRate);

    }
    }


    private void Fire()
    {
        if(cannonballPrefab == null)
        return;
        
        Transform spawnLocation = firePoint != null ? firePoint : transform;

        GameObject ball = Instantiate(cannonballPrefab, spawnLocation.position, Quaternion.identity);

        Cannonball ballScript = ball.GetComponent<Cannonball>();

        if(ballScript != null)
        {
            Vector2 baseDir = Vector2.right;
            switch (direction)
            {
                case 
                FireDirections.Left:  baseDir = Vector2.left;
                break;

                case 
                FireDirections.Right: baseDir = Vector2.right;
                break;

                case 
                FireDirections.Up: baseDir = Vector2.up;
                break;

                case
                FireDirections.Down: baseDir = Vector2.down;
                break;

                case 
                FireDirections.Random360: baseDir = Random.insideUnitCircle.normalized;
                break;
                
            }

            if(direction != FireDirections.Random360 && spreadAngle > 0f)
            {
                float randomOffset = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
                baseDir = Quaternion.Euler(0, 0, randomOffset) * baseDir;
            }
            ballScript.SetupDirection(baseDir.normalized);
        }

    }

}
