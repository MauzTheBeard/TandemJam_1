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
            if (Vector3.Distance(transform.position, player.transform.position) < 2.75f)
            {
                BurstIt();
            }
        }
    }

    private void BurstIt()
    {
        AudioManager.Instance.PlayEventSound("LightBurst");
        AudioManager.Instance.PlayEventSound("GlassBreak", 0.175f);
        lightSource.enabled = false;
        hasBursted = true;

        StartCoroutine(ScaryPropDoneDelay());
    }

    private IEnumerator ScaryPropDoneDelay()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerProgress.Instance.IncrementEncounter();
    }
}
