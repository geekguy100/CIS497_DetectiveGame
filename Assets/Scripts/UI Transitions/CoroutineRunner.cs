/*****************************************************************************
// File Name :         CoroutineRunner.cs
// Author :            Kyle Grenier
// Creation Date :     04/25/2021
//
// Brief Description : Allows coroutines to be ran outside of a MonoBehaviour.
*****************************************************************************/
using System.Collections;
using UnityEngine;

public static class CoroutineRunner
{
    private class CoroutineHolder : MonoBehaviour { };

    private static CoroutineHolder _runner;
    private static CoroutineHolder runner
    {
        get
        {
            if (_runner == null)
                _runner = new GameObject("Coroutine Holder").AddComponent<CoroutineHolder>();

            return _runner;
        }
    }

    public static void StartCoroutine(IEnumerator coroutine)
    {
        runner.StartCoroutine(coroutine);
    }
}
