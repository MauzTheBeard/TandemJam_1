using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBurst : MonoBehaviour
{
    [SerializeField]
    private Light lightSource = null;

    private GameObject player = null;
    private bool hasBursted = false;
    
    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (!hasBursted)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 3.5f)
            {
                BurstIt();
            }
        }
    }

    private void BurstIt()
    {
        AudioManager.Instance.PlayEventSound("LightBurst");
        lightSource.enabled = false;
        hasBursted = true;
    }
}
