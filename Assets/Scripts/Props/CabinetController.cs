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

        StartCoroutine(ScreamAudioRoutine());
        StartCoroutine(DoorLeftAudioCoroutine());
        StartCoroutine(DoorRightAudioCoroutine());

        LeanTween.rotateLocal(doorLeftObject, new Vector3(0, 9.9f, 0), 0.2f).setEaseInOutQuad().setLoopPingPong(5);
        LeanTween.rotateLocal(doorRightObject, new Vector3(0, -11, 0), 0.25f).setEaseInOutQuad().setLoopPingPong(5);

        LeanTween.moveLocalZ(drawerLeftObject, 0.5366f, 0.1f).setEaseInOutQuad().setLoopPingPong(7);
        LeanTween.moveLocalZ(drawerRightObject, 0.5366f, 0.14f).setEaseInOutQuad().setLoopPingPong(6);
    }

    private IEnumerator ScreamAudioRoutine()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.4f);
            AudioManager.Instance.PlayCabinetScreamSound(i);
        }
    }

    private IEnumerator DoorLeftAudioCoroutine()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.2f);
            leftAudioSource.Play();
        }
    }

    private IEnumerator DoorRightAudioCoroutine()
    {
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.25f);
            rightAudioSource.Play();
        }
    }
}
