/*****************************************************************************
// File Name :         PlayerInteractor.cs
// Author :            Kyle Grenier
// Creation Date :     04/07/2021
//
// Brief Description : Player controls for interacting with interactables.
*****************************************************************************/
using UnityEngine;

// TODO: Should this component ONLY be added (or enabled) when the player has their magnifying glass out?
public class PlayerInteractor : IInteractor
{
    [SerializeField] private GameObject magnifyingGlass;
    [SerializeField] GameObject speechBubble;

    private void Update()
    {
        if (GetInteractable()!= null && GetInteractable() is NPCInteraction) speechBubble.gameObject.SetActive(true);
        else speechBubble.gameObject.SetActive(false);
        if (Input.GetKeyDown(KeyCode.E) && !magnifyingGlass.activeInHierarchy)
            PerformInteraction();
    }
}