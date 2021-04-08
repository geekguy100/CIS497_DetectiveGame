/*****************************************************************************
// File Name :         InGameClue.cs
// Author :            Kyle Grenier
// Creation Date :     04/07/2021
//
// Brief Description : An in-game clue that can be interacted with.
*****************************************************************************/
using UnityEngine;

public class InGameClue : MonoBehaviour, IInteractable
{
    [Tooltip("The tag associated with the clue in the JSON file.")]
    [SerializeField] private string clueTag;

    public void Interact(IInteractor interactor)
    {
        Debug.Log("Picked up a clue!");
    }
}