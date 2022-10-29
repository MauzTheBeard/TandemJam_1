using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetController : MonoBehaviour
{
    [SerializeField]
    private GameObject doorLeftObject, doorRightObject = null;
    [SerializeField]
    private GameObject drawerLeftObject = null, drawerRightObject = null;

    [Space(10)]

    [SerializeField]
    private AudioSource leftAudioSource = null;
    [SerializeField]
    private AudioSource rightAudioSource = null;

    private GameObject player = null;
    private bool hasScreamed = false;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (!hasScreamed && (Vector3.Distance(transform.position, player.transform.position) < 3.5f))
        {
            Scream();
        }
    }

    private void Scream()
    {
        hasScreamed = true;

        LeanTween.rotateLocal(doorLeftObject, new Vector3(0, 9.9f, 0), 0.4f).setEaseInOutQuad().setLoopPingPong(5);
        LeanTween.rotateLocal(doorRightObject, new Vector3(0, -11, 0), 0.45f).setEaseInOutQuad().setLoopPingPong(5);
        LeanTween.moveLocalZ(drawerLeftObject, 0.5366f, 0.3f).setEaseInOutQuad().setLoopPingPong(7);
        LeanTween.moveLocalZ(drawerRightObject, 0.5366f, 0.34f).setEaseInOutQuad().setLoopPingPong(6);
    }

    private IEnumerator ScreamAudioRoutine()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
