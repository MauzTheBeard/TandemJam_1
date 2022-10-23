using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    private Light currentLight;

    [SerializeField]
    private float baseInterval = 0.4f;

    private float timerInterval = 0.4f;
    private float timerElapsed = 0.0f;

    void Start()
    {
        //currentLight = GetComponent<Light>();
    }

    void Update()
    {
        TimerTick();
    }

    private void TimerTick()
    {
        if (timerElapsed <= timerInterval)
        {
            timerElapsed += Time.deltaTime;
        }
        else
        {
            timerElapsed = 0.0f;
            timerInterval = Random.Range((baseInterval * 0.5f), (baseInterval * 1.5f));

            currentLight.enabled = !currentLight.enabled;
        }
    }
}