using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int GameObjectId = 0;

    [Space(10)]

    [SerializeField]
    private GameObject doorModel = null;
        
    [SerializeField]
    private float openPosition = 0.0f;
    [SerializeField]
    private float closedPosition = 0.0f;
    [SerializeField]
    private float transitionTime = 0.0f;

    private void Awake()
    {
        GameObjectId = gameObject.GetInstanceID();
    }

    private void Start()
    {
        GameEvents.Instance.OnDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.Instance.OnDoorwayTriggerExit += OnDoorwayClose;        
    }

    private void OnDoorwayOpen(int gameObjectId)
    {
        if (gameObjectId == this.GameObjectId)
        {
            LeanTween.moveLocalY(doorModel, openPosition, transitionTime).setEaseInOutQuad();
        }        
    }

    private void OnDoorwayClose(int gameObjectId)
    {
        if (gameObjectId == this.GameObjectId)
        {
            LeanTween.moveLocalY(doorModel, closedPosition, transitionTime).setEaseInOutQuad();
        }        
    }
}
