/*****************************************************************************
// File Name :         Footsteps.cs
// Author :            Kyle Grenier
// Creation Date :     03/31/2021
//
// Brief Description : Footstep sounds for character movement.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Footsteps : MonoBehaviour
{
    [Tooltip("The array of possible footstep sounds to play.")]
    [SerializeField] private AudioClip[] footstepClips;

    // The AudioSource to play the footstep sounds
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomFootstep()
    {
        AudioClip clip = AudioClipHepler.GetRandomAudioClipFromSet(footstepClips);
        audioSource.PlayOneShot(clip);
    }
}
