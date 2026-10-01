
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public enum FireDirections {Left, Right, Up, Down}

    [SerializeField] private GameObject cannonballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float initialDelay = 0.5f;
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private FireDirections direction = FireDirections.Left;

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
            Vector2 dirVector = Vector2.left;
            switch (direction)
            {
                case 
                FireDirections.Left:  dirVector = Vector2.left;
                break;

                case 
                FireDirections.Right: dirVector = Vector2.right;
                break;

                case 
                FireDirections.Up: dirVector = Vector2.up;
                break;

                case
                FireDirections.Down: dirVector = Vector2.down;
                break;
                
            }

            ballScript.SetupDirection(dirVector);
        }

    }

}
