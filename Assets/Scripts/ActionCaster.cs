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
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 3.0f, layerMask))
        {
            Vector3 forward = hit.transform.TransformDirection(Vector3.forward);
            Vector3 toOther = transform.position - hit.transform.position;            
            float DotResult = Vector3.Dot(toOther, forward);
            currentDoorController = hit.collider.gameObject.GetComponentInParent<DoorController>();

            if (DotResult > 0)
            {
                Debug.Log("Right");
                doorOpenDirection = -1;
            }
            else
            {
                Debug.Log("Left");
                doorOpenDirection = 1;
            }            

            
            isInDoorRange = true;
        }
        else
        {
            isInDoorRange = false;
        }
    }
}
