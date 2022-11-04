using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockingchairController : MonoBehaviour
{
    [SerializeField]
    private GameObject rockingchairModel = null;

    private bool hasFlipped = false;

    private void Start()
    {
        RockIt();
    }

    private void RockIt()
    {
        LeanTween.rotateX(rockingchairModel, -6.5f, 1.5f).setEaseInOutQuad().setLoopPingPong();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasFlipped == false)
        {
            FlipChair();
        }
    }

    private void FlipChair()
    {
        LeanTween.moveY(gameObject, 2.5f, 0.1f);
        LeanTween.rotateZ(gameObject, 180, 0.1f);

        AudioManager.Instance.PlayEventSound("FlipRockingchair");
        AudioManager.Instance.PlayEventSound("RockingchairWhispers", 0.5f);

        hasFlipped = true;

        StartCoroutine(ScaryPropDoneDelay());
    }

    private IEnumerator ScaryPropDoneDelay()
    {
        yield return new WaitForSeconds(3.0f);
        PlayerProgress.Instance.IncrementEncounter();
    }
}
