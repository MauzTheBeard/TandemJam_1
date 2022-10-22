using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAudioController : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> doorOpenClips = null;
    [SerializeField]
    private List<AudioClip> doorCloseClips = null;
    [SerializeField]
    private List<AudioClip> doorKnockClips = null;

    private AudioSource audioSource = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOpenSound()
    {
        audioSource.clip = doorOpenClips[0];
        audioSource.Play();        
    }

    public void PlayCloseSound()
    {
        audioSource.clip = doorCloseClips[0];
        audioSource.PlayDelayed(0.75f);
    }

    public void PlayRandomKnockSound()
    {
        int rIndex = Random.RandomRange(0, doorKnockClips.Count - 1);

        audioSource.clip = doorKnockClips[rIndex];
        audioSource.Play();
    }
}
