using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{        
    [SerializeField]
    private float openPosition = 0.0f;
    [SerializeField]
    private float closedPosition = 0.0f;
    [SerializeField]
    private float transitionTime = 0.0f;

    private bool isOpen = false;
    private float closeTime = 4.0f;
    private float closeTimeElapsed = 0.0f;
        
    private void Update()
    {
        if (isOpen)
        {
            closeTimeElapsed += Time.deltaTime;

            if (closeTimeElapsed >= closeTime)
            {
                closeTimeElapsed = 0.0f;
                Close();
            }
        }
    }

    public void Open(int direction)
    {
        LeanTween.rotateY(gameObject, openPosition * direction, transitionTime).setEaseInOutQuad();
        isOpen = true;
    }

    private void Close()
    {
        LeanTween.rotateY(gameObject, closedPosition, transitionTime).setEaseInOutQuad();
        isOpen = false;
    }
}
