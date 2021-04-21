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

    [Tooltip("The clue tag exactly as it is written in the JSON file.")]
    [SerializeField] private string clueTag;

    [Tooltip("The description of the clue. This will be added to the journal.")]
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
        // Only log the clue of we haven't discovered it before.
        if (!Journal.Instance.HasDiscoveredClue(clue.ClueTag))
        {
            EventManager.ClueFound("Case", clue);
            interactor.UnassignInteractable();
            //UIManager.Instance.HideScanPanel();
            //UIManager.Instance.UpdateClueText(clue.ClueTag);
            //Journal.Instance.AddClue("Case", clue);
        }
    }

    public void OnAssigned(IInteractor interactor)
    {
        if (!Journal.Instance.HasDiscoveredClue(clue.ClueTag))
            UIManager.Instance.DisplayScanPanel();
    }

    public void OnUnassigned(IInteractor interactor)
    {
        UIManager.Instance.HideScanPanel();
    }
}