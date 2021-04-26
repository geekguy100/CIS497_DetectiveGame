/*****************************************************************************
// File Name :         EventManager.cs
// Author :            Kyle Grenier
// Creation Date :     04/20/2021
//
// Brief Description : Handles subscribing, unsubscribing, and invoking events.
*****************************************************************************/
using System;
using UnityEngine;

public static class EventManager
{
    public static Action<Clue> OnClueFound;
    public static Action<bool> OnAccusation;
    public static Action OnGameRestart;

    public static void ClueFound(Clue clue)
    {
        OnClueFound?.Invoke(clue);
    }

    public static void Accusation(bool correctAccusation)
    {
        OnAccusation?.Invoke(correctAccusation);
    }

    public static void GameRestart()
    {
        Debug.Log("~~~~~~~~~~RESETTING~~~~~~~~~~~");
        OnGameRestart?.Invoke();
    }
}