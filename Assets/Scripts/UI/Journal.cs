using UnityEngine;
using System;
using System.Linq;
using TMPro;

[System.Serializable]
public class JournalPage
{
    public string charName;
    public string charDesc;
    public Clue[] clues;

    public int cluesDiscovered = 0;
}

public class Journal : Singleton<Journal>
{
    [SerializeField] JournalPage[] pages;

    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] TextMeshProUGUI characterDesc;
    [SerializeField] TextMeshProUGUI[] clueSlots;

    private int currentPage;

    void Start()
    {
        AddClue("Billy Bob", new Clue("test1", "I foundt this clue at runtime."));
        AddClue("Mary Sue", new Clue("test2", "I found this clue at runtime."));
    }

    public void AddClue(string charName, Clue clue)
    {
        JournalPage pageToAddClueTo = pages.Where(t => t.charName == charName).FirstOrDefault();

        if (pageToAddClueTo == null)
        {
            Debug.LogWarning("Could not find a journal page with character name '" + charName + "'.");
            return;
        }

        int characterIndex = Array.IndexOf(pages, pageToAddClueTo);

        pages[characterIndex].clues[pages[characterIndex].cluesDiscovered] = clue;
        pages[characterIndex].cluesDiscovered++;
    }

    /// <summary>
    /// Returns true if the player has discovered a clue.
    /// </summary>
    /// <param name="clueTag">The tag of the clue to check for.</param>
    /// <returns>True if the player has discovered a clue.</returns>
    public bool DiscoveredClue(string clueTag)
    {
        foreach (JournalPage page in pages)
        {
            Clue[] cluesOnPage = page.clues;
            string[] clueTags = new string[cluesOnPage.Length];

            for (int i = 0; i < clueTags.Length; ++i)
                clueTags[i] = cluesOnPage[i].ClueTag;

            if (clueTags.Contains(clueTag))
                return true;
        }

        return false;
    }

    public void ChangePage(int page)
    {
        currentPage = page;
        UpdateJournalDisplay();
    }

    void UpdateJournalDisplay()
    {
        characterName.text = pages[currentPage].charName;
        characterDesc.text = pages[currentPage].charDesc;
        for (int i = 0; i < pages[currentPage].clues.Length; i++)
        {
            clueSlots[i].text = pages[currentPage].clues[i].ClueDesc;
        }
    }
}