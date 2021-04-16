/*****************************************************************************
// File Name :         UIManager.cs
// Author :            Kyle Grenier
// Creation Date :     04/12/2021
//
// Brief Description : Manages updating UI elements.
*****************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Settings")]
    [SerializeField] private float dialogueScreenTime;

    [Header("Dialogue Containers")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Clue Probing")]
    [SerializeField] private GameObject questionPanel;
    [SerializeField] private GameObject buttonPanel;
    [SerializeField] Button closeButton;

    [Header("Misc")]
    [SerializeField] private TextMeshProUGUI clueFoundText;

    private List<QuestionButton> questionButtons;
    private struct QuestionButton
    {
        public QuestionButton(Button button, Clue clue)
        {
            this.button = button;
            this.clue = clue;
        }

        public Button button;
        public Clue clue;
    }



    private void Start()
    {
        questionButtons = new List<QuestionButton>();
        questionButtons.Add(new QuestionButton(closeButton, new Clue()));
    }

    public void UpdateDialoguePanel(string characterName, string dialogue)
    {
        StopAllCoroutines();

        dialoguePanel.SetActive(true);
        nameText.text = characterName;
        dialogueText.text = dialogue;

        StartCoroutine(DeactivateAfterTime(dialoguePanel));
    }

    public void ToggleQuestionPanel()
    {
        questionPanel.SetActive(!questionPanel.activeInHierarchy);

        // If we toggled on the question panel, fill it with all of the
        // button we need.
        if (questionPanel.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PopulateButtons();
        }
        else
            OnQuestionPanelDisable();
    }

    public void OnQuestionPanelDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateClueText(string clue)
    {
        clueFoundText.text = "A new clue has been recorded: " + clue;
        clueFoundText.gameObject.SetActive(true);
        StartCoroutine(DeactivateAfterTime(clueFoundText.gameObject));
    }

    private void PopulateButtons()
    {
        Clue[] knownClues = Journal.Instance.GetAllKnownClues();

        // Check to see if we already have a button associated with the clue.
        // If we do, don't create a new one.
        foreach (Clue clue in knownClues)
        {
            print(clue.ClueTag);
            bool foundClue = false;

            foreach (QuestionButton qb in questionButtons)
            {
                if (qb.clue.Equals(clue))
                {
                    foundClue = true;
                    break;
                }
            }

            if (foundClue)
                continue;

            print("Creating a button for clue " + clue.ClueTag);

            Button button = Instantiate(closeButton, buttonPanel.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = clue.ClueTag + "?";

            // Reset whatever was on the close button.
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { QuestionHandler.ProbeClue(clue); });

            questionButtons.Add(new QuestionButton(button, clue));
        }
        
    }

    private IEnumerator DeactivateAfterTime(GameObject obj)
    {
        yield return new WaitForSeconds(dialogueScreenTime);
        obj.SetActive(false);
    }
}
