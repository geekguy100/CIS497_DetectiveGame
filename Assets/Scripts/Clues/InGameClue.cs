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
    /// <summary>
    /// A Clue that can be added to the journal and probed about to NPCs.
    /// </summary>
    /// <param name="clueTag">The keyword/tag associated with the clue.</param>
    /// <param name="clueDesc">The lengthier description of the clue.</param>
    /// <param name="person">The journal tab this clue will be initally placed into. Defaults to 'Misc' if no name is provided.</param>
    public Clue(string clueTag, string clueDesc, string person = "Misc")
    {
        this.clueTag = clueTag;
        this.clueDesc = clueDesc;
        this.person = person;
    }

    public override string ToString()
    {
        return clueTag;
    }

    [Tooltip("The clue tag exactly as it is written in the JSON file.")]
    [SerializeField] private string clueTag;

    [Tooltip("The description of the clue. This will be added to the journal.")]
    [SerializeField] private string clueDesc;

    [Tooltip("The person associated with this clue and the page of the Journal the clue will be recorded into.")]
    [SerializeField] private string person;

    public string ClueTag { get { return clueTag; } }
    public string ClueDesc { get { return clueDesc; } }
    public string Person { get { return person; } }
}

public class InGameClue : MonoBehaviour, IInteractable
{
    [Tooltip("The clue's data.")]
    [SerializeField] private Clue clue;

    private GameObject visual;

    private void Start()
    {
        GameObject visualPrefab = Factory.Spawn(clue.ClueTag.ToString());
        if (visualPrefab != null)
        {
            visual = Instantiate(visualPrefab, transform.position, visualPrefab.transform.rotation);
            visual.gameObject.name = clue.ClueTag;
        }
    }


    public void Interact(IInteractor interactor)
    {
        // Only log the clue of we haven't discovered it before.
        if (!Journal.Instance.HasDiscoveredClue(clue.ClueTag))
        {
            EventManager.ClueFound(clue);
            gameObject.layer = LayerMask.NameToLayer("Default"); // Makes sure the clue shader is not present after discovering the clue.
            interactor.UnassignInteractable();
            GameObject go = ObjectPooler.Instance.SpawnFromPool("pickup", this.gameObject.transform.position, Quaternion.identity);
            go.GetComponent<ParticleSystem>().Play();
            ObjectPooler.Instance.ReturnObjectToPool("pickup", go);
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