/*****************************************************************************
// File Name :         AudioClipHepler.cs
// Author :            Kyle Grenier
// Creation Date :     03/12/2021
//
// Brief Description : A class to help get audio clips.
*****************************************************************************/
using UnityEngine;
public static class AudioClipHepler
{
    /// <summary>
    /// Returns a random audio clip from the given set.
    /// </summary>
    /// <param name="audioClips">The set of audio clips.</param>
    /// <returns>A random audio clip from the given set.</returns>
    public static AudioClip GetRandomAudioClipFromSet(AudioClip[] audioClips)
    {
        int index = Random.Range(0, audioClips.Length);
        return audioClips[index];
    }
}
