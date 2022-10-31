using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    #region Singleton Members
    private static PlayerProgress instance;
    public static PlayerProgress Instance => instance;
    #endregion

    [SerializeField]
    private int propsCount = 0;

    public bool IsGameOver = false;

    private int propsEncountered = 0;    

    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    
    private void Update()
    {
        if (propsEncountered == propsCount)
        {
            IsGameOver = true;
        }
    }

    public void IncrementEncounter()
    {
        propsEncountered++;
    }

    public string GetProgress()
    {
        return $"{propsEncountered}/{propsCount}";
    }
}
