using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField]
    private GameObject flashLightObject;
    private Light spotLight = null;

    private float lightOnTimerElapsed = 0.0f;
    private float lightOnTimerTime = 0.0f;

    private bool lightIsOn = false;
    private bool isFlickering = false;

    private void Awake()
    {
        spotLight = GetComponentInChildren<Light>();
        spotLight.enabled = false;
        
        ResetTimer();
    }

    private void Update()
    {
        HandleInput();
        FlashlightOnTimerTick();
    }

    private void FixedUpdate()
    {
        AimFlashlight();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.T) && isFlickering == false)
        {
            SwitchLight(true);
        }
    }

    private void FlashlightOnTimerTick()
    {
        if (lightIsOn)
        {
            lightOnTimerElapsed += Time.deltaTime;

            if (lightOnTimerElapsed >= lightOnTimerTime)
            {
                ResetTimer();
                StartCoroutine(FlickerLightOff());
            }
        }
    }

    private void SwitchLight(bool isManualSwitch)
    {
        if (isManualSwitch)
        {
            AudioManager.Instance.PlayEventSound("FlashlightSwitch");
        }

        if (lightIsOn)
        {
            spotLight.enabled = false;
            lightIsOn = false;
        }
        else
        {
            spotLight.enabled = true;
            lightIsOn = true;
        }
    }

    private IEnumerator FlickerLightOff()
    {
        isFlickering = true;
        int maxSwitches = Random.Range(3, 7);

        for (int i = 0; i <= maxSwitches; i++)
        {
            SwitchLight(false);
            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        }

        spotLight.enabled = false;
        lightIsOn = false;
        isFlickering = false;
    }

    private void ResetTimer()
    {
        lightOnTimerElapsed = 0.0f;
        lightOnTimerTime = GetRandomLightOnTimerTime();
    }

    private float GetRandomLightOnTimerTime()
    {
        return Random.Range(7.5f, 15.75f);
    }

    private void AimFlashlight()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            flashLightObject.transform.LookAt(hit.point);
        }
    }
}
