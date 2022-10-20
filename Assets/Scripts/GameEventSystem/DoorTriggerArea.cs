using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerArea : MonoBehaviour
{
    private int gameObjectId = 0;

    private void Start()
    {
        //gameObjectId = GetComponentInParent<DoorController>().GameObjectId;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameEvents.Instance.DoorwayTriggerEnter(gameObjectId);
    }

    private void OnTriggerExit(Collider other)
    {
        GameEvents.Instance.DoorwayTriggerExtit(gameObjectId);
    }
}
