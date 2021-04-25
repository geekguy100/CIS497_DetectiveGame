/*****************************************************************************
// File Name :         TransitionHandler.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class TransitionHandler : Singleton<TransitionHandler>
{
    private Animator anim;

    public delegate void TransitionCallback();
    public TransitionCallback callback;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();
        if (anim == null)
            throw new MissingComponentException("Could not find Animator in child!");
    }

    /// <summary>
    /// Begins the transition animation (fade in a black background).
    /// </summary>
    /// <param name="waitTime">The time to wait before starting to fade out. Set to -1 to ignore a wait time.</param>
    public void FadeIn(TransitionCallback callback = null, float waitTime = -1)
    {
        if (anim == null)
            return;

        anim.SetTrigger("FadeIn");
        if (waitTime > -1)
            StartCoroutine(WaitThenFadeOut(waitTime));

        this.callback = callback;
    }

    public void InvokeCallback()
    {
        callback?.Invoke();
    }

    private IEnumerator WaitThenFadeOut(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        FadeOut();
    }

    public void FadeOut()
    {
        if (anim == null)
            return;

        anim.SetTrigger("FadeOut");
    }
}
