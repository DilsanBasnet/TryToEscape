using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinManagerScript : MonoBehaviour
{
    public static CoinManagerScript Instance;

    private HashSet<string> collectedCoinIDs = new HashSet<string>();

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
            return;
        }
         SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Main Menu Scene")
        {
            collectedCoinIDs.Clear() ;
            Destroy(gameObject); 
        }
    }

    public void RegisterCollectedCoin(string coinID)
    {
        if(!collectedCoinIDs.Contains(coinID))
        {
            collectedCoinIDs.Add(coinID);
        }
    }
    public bool IsCoinCollected(string coinID)
    {
        return collectedCoinIDs.Contains(coinID);
    }
}
