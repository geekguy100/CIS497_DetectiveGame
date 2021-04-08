using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class JournalPage
{
    public string charName;
    public string charDesc;
    public string[] clues;

    public int discoveredClues = 0;
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
        AddClue("Billy Bob", "I found this clue at runtime");
        AddClue("Mary Sue", "I found this clue at runtime");
    }

    public void AddClue(string charName, string clue)
    {
        JournalPage pageToAddClueTo = pages.Where(t => t.charName == charName).FirstOrDefault();

        if (pageToAddClueTo == null)
        {
            Debug.LogWarning("Could not find a journal page with character name '" + charName + "'.");
            return;
        }

        int characterIndex = Array.IndexOf(pages, pageToAddClueTo);

        pages[characterIndex].clues[pages[characterIndex].discoveredClues] = clue;
        pages[characterIndex].discoveredClues++;
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
        for(int i = 0; i < pages[currentPage].clues.Length; i++)
        {
            clueSlots[i].text = pages[currentPage].clues[i];
        }

    } 
}