using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private static GameEvents instance;
    public static GameEvents Instance => instance;
    
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

    public event Action<int> OnDoorwayTriggerEnter;
    public void DoorwayTriggerEnter(int gameObjectId)
    {
        if (OnDoorwayTriggerEnter != null)
        {
            OnDoorwayTriggerEnter(gameObjectId);
        }
    }

    public event Action<int> OnDoorwayTriggerExit;
    public void DoorwayTriggerExtit(int gameObjectId)
    {
        if (OnDoorwayTriggerExit != null)
        {
            OnDoorwayTriggerExit(gameObjectId);
        }
    }
}
