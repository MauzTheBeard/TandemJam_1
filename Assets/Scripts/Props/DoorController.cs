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

    private bool isKnocking = false;
    private bool hasKnocked = false;

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
        if (!LeanTween.isTweening(doorModel) && isOpen == false && isKnocking == false)
        {
            audioController.PlayOpenSound();
            LeanTween.rotateLocal(doorModel, new Vector3(0, openPosition * direction, 0), transitionTime).setEaseInOutQuad();
            isOpen = true;
        }        
    }

    private void Close()
    {
        LeanTween.rotateLocal(doorModel, new Vector3(0, closedPosition, 0), transitionTime).setEaseInOutQuad();
        audioController.PlayCloseSound();
        isOpen = false;
    }

    public void TryKnocking()
    {
        if (hasKnocked == false)
        {
            if (Random.Range(0, 100) < 5 && isOpen == false)
            {
                hasKnocked = true;
                StartCoroutine(DoorKnocking());
            }
        }
        
    }

    private IEnumerator DoorKnocking()
    {
        isKnocking = true;

        for (int i = 0; i < 3; i++)
        {
            audioController.PlayRandomKnockSound();
            yield return new WaitForSeconds(0.3f);
        }

        isKnocking = false;
        PlayerProgress.Instance.IncrementEncounter();
    }
}
