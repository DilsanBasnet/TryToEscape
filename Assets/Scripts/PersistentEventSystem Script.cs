using UnityEngine;

public class PersistentEventSystemScript : MonoBehaviour
{
    public static PersistentEventSystemScript Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
         else
        {
            Destroy(gameObject);
        }
    }
}
