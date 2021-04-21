/*****************************************************************************
// File Name :         EventManager.cs
// Author :            Kyle Grenier
// Creation Date :     04/20/2021
//
// Brief Description : Handles subscribing, unsubscribing, and invoking events.
*****************************************************************************/
using System;

public static class EventManager
{
    public static Action<Clue> OnClueFound;
    public static Action<string> OnAccusation;

    public static void ClueFound(Clue clue)
    {
        OnClueFound?.Invoke(clue);
    }

    public static void Accusation(string text)
    {
        OnAccusation?.Invoke(text);
    }
}