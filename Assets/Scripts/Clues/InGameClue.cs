/*****************************************************************************
// File Name :         InGameClue.cs
// Author :            Kyle Grenier
// Creation Date :     04/07/2021
//
// Brief Description : An in-game clue that can be interacted with.
*****************************************************************************/
using UnityEngine;

[System.Serializable]
public struct Clue
{
    public Clue(string clueTag, string clueDesc)
    {
        this.clueTag = clueTag;
        this.clueDesc = clueDesc;
    }

    [SerializeField] private string clueTag;
    [SerializeField] private string clueDesc;

    public string ClueTag { get { return clueTag; } }
    public string ClueDesc { get { return clueDesc; } }
}

public class InGameClue : MonoBehaviour, IInteractable
{
    [Tooltip("The clue's data.")]
    [SerializeField] private Clue clue;

    public void Interact(IInteractor interactor)
    {
        Debug.Log("Picked up a clue!");
    }
}