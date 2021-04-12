/*****************************************************************************
// File Name :         IInteractable.cs
// Author :            Kyle Grenier
// Creation Date :     04/07/2021
//
// Brief Description : Defines a contract for all in-game interactables.
*****************************************************************************/
using UnityEngine;

// TODO: Make this an abstract class and make OnAssigned and OnUnassigned optional hooks.
public interface IInteractable
{
    void Interact(IInteractor interactor);
    void OnAssigned(IInteractor interactor);
    void OnUnassigned(IInteractor interactor);
}
