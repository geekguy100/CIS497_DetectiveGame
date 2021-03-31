/*****************************************************************************
// File Name :         EventAudioManager.cs
// Author :            Kyle Grenier
// Creation Date :     03/24/2021
//
// Brief Description : Manages playing audio clips for in-game events.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EventAudioManager : MonoBehaviour
{
    public static EventAudioManager instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays an AudioClip.
    /// </summary>
    /// <param name="clip">The AudioClip to play.</param>
    public void PlayOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
