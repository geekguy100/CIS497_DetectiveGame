/*****************************************************************************
// File Name :         EventManager.cs
// Author :            Kyle Grenier
// Creation Date :     04/20/2021
//
// Brief Description : Handles subscribing and unsubscribing to events.
*****************************************************************************/
using System;

public static class EventManager
{
    public static Action<string, Clue> OnClueFound;

    public static void ClueFound(string page, Clue clue)
    {
        OnClueFound?.Invoke(page, clue);
    }
}