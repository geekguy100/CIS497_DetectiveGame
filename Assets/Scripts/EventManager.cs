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
    public static Action<Clue> OnClueFound;

    public static void ClueFound(Clue clue)
    {
        OnClueFound?.Invoke(clue);
    }
}