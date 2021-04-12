/*****************************************************************************
// File Name :         NPCInteraction.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Checks to see if the player knows anything about the only in game clue.
    /// If so, displays some dialogue.
    /// </summary>
    /// <param name="interactor">The IInteractor that interacted with the NPC.</param>
    public void Interact(IInteractor interactor)
    {
        Debug.Log("Interacted with NPC");
    }
}