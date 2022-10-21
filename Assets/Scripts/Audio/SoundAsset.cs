using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundAsset
{
    #region Members

    public string Name = "";

    public AudioClip Clip = null;

    [Range(0, 1)]
    public float Volume = 1.0f;
    [Range(0, 1)]
    public float Pitch = 1.0f;

    [HideInInspector]
    public AudioSource Source = null;

    #endregion
}