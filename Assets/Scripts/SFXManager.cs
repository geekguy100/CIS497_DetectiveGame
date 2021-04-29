using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    public AudioSource source;
    public AudioClip clueFound;
    public AudioClip gibberish;
    public AudioClip equipMag;
    public AudioClip journalClose;
    public AudioClip journalOpen;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
}
