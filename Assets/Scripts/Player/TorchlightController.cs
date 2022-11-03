using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchlightController : MonoBehaviour
{
    [SerializeField]
    private Light spotLight = null;

    private float torchOnTimerElapsed = 0.0f;
    private float torchOnTimerTime = 0.0f;

    private void Awake()
    {
        spotLight.enabled = false;
    }

    private void Update()
    {
        HandleInput();
        TorchOnTimerTick();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchLight();
        }
    }

    private void TorchOnTimerTick()
    {
        if (spotLight.enabled)
        {

        }
    }

    private void SwitchLight()
    {
        if (spotLight.enabled)
        {
            spotLight.enabled = false;
        }
        else
        {
            spotLight.enabled = true;
        }
    }
}
