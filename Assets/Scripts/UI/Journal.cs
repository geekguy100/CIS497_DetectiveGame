using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
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
        ChangePage(5);
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
    public bool HasDiscoveredClue(string clueTag)
    {
        foreach (JournalPage page in pages)
        {
            Clue[] cluesOnPage = page.clues;
            if (cluesOnPage.Length == 0)
                continue;

            string[] clueTags = new string[cluesOnPage.Length];

            for (int i = 0; i < clueTags.Length; ++i)
                clueTags[i] = cluesOnPage[i].ClueTag;

            if (clueTags.Contains(clueTag))
                return true;
        }

        return false;
    }

    public Clue[] GetAllKnownClues()
    {
        List<Clue> knownClues = new List<Clue>();

        foreach(JournalPage page in pages)
        {
            for (int i = 0; i < page.cluesDiscovered; ++i)
            {
                knownClues.Add(page.clues[i]);
            }
        }

        return knownClues.ToArray();
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