using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingController : MonoBehaviour
{
    [SerializeField]
    private GameObject paintingObject = null;

    private bool hasGlitched = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasGlitched)
        {
            Glitch();
        }
    }

    private void Glitch()
    {
        hasGlitched = true;
        AudioManager.Instance.PlayEventSound("PaintingGlitch");
        StartCoroutine(MoveAround());
        StartCoroutine(ScaryPropDoneDelay());
    }

    private IEnumerator MoveAround()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 newPos = new Vector3(Random.Range(-0.75f, 0.75f), Random.Range(-0.12f, 0.16f), 0);
            paintingObject.transform.localPosition = newPos;

            yield return new WaitForSeconds(0.1f);
        }

        paintingObject.transform.localPosition = Vector3.zero;
    }

    private IEnumerator ScaryPropDoneDelay()
    {
        yield return new WaitForSeconds(0.75f);
        PlayerProgress.Instance.IncrementEncounter();
    }
}
