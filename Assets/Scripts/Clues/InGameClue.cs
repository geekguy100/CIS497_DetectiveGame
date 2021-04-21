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
    public Clue(string clueTag, string clueDesc, string person = "Case")
    {
        this.clueTag = clueTag;
        this.clueDesc = clueDesc;
        this.person = person;
    }

    [Tooltip("The clue tag exactly as it is written in the JSON file.")]
    [SerializeField] private string clueTag;

    [Tooltip("The description of the clue. This will be added to the journal.")]
    [SerializeField] private string clueDesc;

    [Tooltip("The person associated with this clue and the page of the Journal the clue will be recorded into. " +
        "Defaults to 'Case' if left blank.")]
    [SerializeField] private string person;

    public string ClueTag { get { return clueTag; } }
    public string ClueDesc { get { return clueDesc; } }
    public string Person { get { return person; } }
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
            EventManager.ClueFound(clue);
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