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

    private void OnEnable()
    {
        EventManager.OnClueFound += AddClue;
    }

    private void OnDisable()
    {
        EventManager.OnClueFound -= AddClue;
    }

    void Start()
    {
        //AddClue("Case", new Clue("Knowledge", ""));
        ChangePage(5);
    }

    public void AddClue(Clue clue)
    {
        JournalPage pageToAddClueTo = pages.Where(t => t.charName == clue.Person).FirstOrDefault();

        if (pageToAddClueTo == null)
        {
            Debug.LogWarning("Could not find a journal page with character name '" + clue.Person + "'.");
            return;
        }

        int characterIndex = Array.IndexOf(pages, pageToAddClueTo);

        pages[characterIndex].clues[pages[characterIndex].cluesDiscovered] = clue;
        pages[characterIndex].cluesDiscovered++;

        UpdateJournalDisplay();
    }

    /// <summary>
    /// Moves a clue from whatever page it is currently on to the specified person's page.
    /// </summary>
    /// <param name="clue">The clue to move.</param>
    /// <param name="person">The tab to move the clue to.</param>
    public void MoveClueToPage(Clue clue, string person)
    {
        JournalPage pageToRemoveFrom = pages.Where(t => t.charName == "Misc").FirstOrDefault();
        if (pageToRemoveFrom == null)
        {
            Debug.LogWarning("Could not remove clue from page -- page is nonexistent.");
            return;
        }

        int characterIndex = Array.IndexOf(pages, pageToRemoveFrom);

        // We'll convert the array of pages into a list, convert the array of clues on that page into a list,
        // remove the clue we want, convert everything back into an array, and reassign our pages array.
        List<JournalPage> pageList = pages.ToList();
        List<Clue> cluesOnPage = pageList.ElementAt(characterIndex).clues.ToList();

        // ****IF THIS DOESN'T WORK DELETE THE ELEMENT THAT MATCHES THE PASSED IN CLUETAG!!!****
        if (!cluesOnPage.Contains(clue))
        {
            Debug.LogWarning("JOURNAL: Cannot move clue to page '" + person + "' because the clue is non-existnent on the Misc page.");
            return;
        }

        cluesOnPage.Remove(clue);
        pageList.ElementAt(characterIndex).cluesDiscovered--;

        // Reassigning the clues.
        pageList.ElementAt(characterIndex).clues = cluesOnPage.ToArray();

        // Reassigning the pages.
        pages = pageList.ToArray();

        AddClue(new Clue(clue.ClueTag, clue.ClueDesc, person));
    }

    public bool PageContainsClue(string page, Clue clue)
    {
        JournalPage pageWithClue = pages.Where(t => t.charName == page).FirstOrDefault();

        if (pageWithClue == null)
        {
            Debug.LogWarning("JOURNAL: Cannot check if page '" + page + "' contains clue because it is nonexistent.");
            return false;
        }

        bool alreadyContains = false;
        foreach(Clue clueInQuestion in pageWithClue.clues.Where(t=>t.ClueTag == clue.ClueTag))
        {
            alreadyContains = true;
            break;
        }

        print("PAGE " + page + " CONTAINS THE CLUE: " + alreadyContains);
        return alreadyContains;
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