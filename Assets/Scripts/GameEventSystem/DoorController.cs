using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private GameObject doorModel;
    [SerializeField]
    private float openPosition = 0.0f;
    [SerializeField]
    private float closedPosition = 0.0f;
    [SerializeField]
    private float transitionTime = 0.0f;

    private bool isOpen = false;
    private float closeTime = 4.0f;
    private float closeTimeElapsed = 0.0f;

    private DoorAudioController audioController = null;

    private void Start()
    {
        audioController = GetComponent<DoorAudioController>();
    }

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
        audioController.PlayOpenSound();
        LeanTween.rotateY(doorModel, openPosition * direction, transitionTime).setEaseInOutQuad();
        isOpen = true;
    }

    private void Close()
    {
        LeanTween.rotateY(doorModel, closedPosition, transitionTime).setEaseInOutQuad();
        audioController.PlayCloseSound();
        isOpen = false;
    }
}
