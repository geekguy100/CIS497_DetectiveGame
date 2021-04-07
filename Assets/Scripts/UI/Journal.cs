using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public class Journal : MonoBehaviour
{
    [SerializeField] JournalPage[] pages;

    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] TextMeshProUGUI characterDesc;
    [SerializeField] TextMeshProUGUI[] clueSlots;

    private int currentPage;

    // Update is called once per frame
    void Start()
    {
        AddClue(0, "I found this clue at runtime");
        AddClue(1, "I found this clue at runtime");
    }
    private void Update()
    {
        
    }
    public void AddClue(int character, string clue)
    {
        pages[character].clues[pages[character].discoveredClues] = clue;
        pages[character].discoveredClues++;
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
