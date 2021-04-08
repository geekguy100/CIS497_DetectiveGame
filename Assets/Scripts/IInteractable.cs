/*****************************************************************************
// File Name :         IInteractable.cs
// Author :            Kyle Grenier
// Creation Date :     04/07/2021
//
// Brief Description : Defines a contract for all in-game interactables.
*****************************************************************************/
using UnityEngine;

public interface IInteractable
{
    void Interact(IInteractor interactor);
}
