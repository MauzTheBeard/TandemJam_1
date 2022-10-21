using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCaster : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera = null;
    [SerializeField]
    private LayerMask layerMask;

    private bool isInDoorRange = false;
    private DoorController currentDoorController = null;
    private int doorOpenDirection = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInDoorRange && currentDoorController != null)
        {
            currentDoorController.Open(doorOpenDirection);            
        }
    }

    void FixedUpdate()
    {
        CheckDoorPriximity();
    }

    private void CheckDoorPriximity()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 3.0f, layerMask))
        {
            currentDoorController = hit.collider.gameObject.GetComponentInParent<DoorController>();

            if (GetDirectionToOpen(hit) > 0)
            {
                //Debug.Log("Right");
                doorOpenDirection = 1;
            }
            else
            {
                //Debug.Log("Left");
                doorOpenDirection = -1;
            }

            isInDoorRange = true;
        }
        else
        {
            Reset();
        }
    }

    private float GetDirectionToOpen(RaycastHit hit)
    {
        Vector3 forward = hit.transform.TransformDirection(Vector3.right);
        Vector3 toOther = transform.position - hit.transform.position;
        float DotResult = Vector3.Dot(toOther, forward);
        return DotResult;
    }

    private void Reset()
    {
        currentDoorController = null;
        doorOpenDirection = 0;
        isInDoorRange = false;
    }
}
