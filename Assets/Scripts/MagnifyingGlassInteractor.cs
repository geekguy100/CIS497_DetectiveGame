/*****************************************************************************
// File Name :         MagnifyingGlassInteractor.cs
// Author :            Kyle Grenier
// Creation Date :     04/07/2021
//
// Brief Description : Player controls for interacting with the magnifying glass.
*****************************************************************************/
using UnityEngine;

public class MagnifyingGlassInteractor : IInteractor
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            PerformInteraction();
    }

    private void OnDisable()
    {
        UnassignInteractable();
    }
}
