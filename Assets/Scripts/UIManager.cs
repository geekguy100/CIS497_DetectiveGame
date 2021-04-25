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
using System.Linq;

public class UIManager : Singleton<UIManager>
{
    [Header("Settings")]
    [SerializeField] private float dialogueScreenTime;

    [Header("Dialogue Containers")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Button Prefab")]
    [SerializeField] private Button buttonPrefab;

    [Header("Clue Probing")]
    [SerializeField] private GameObject questionPanel;
    [SerializeField] private GameObject questionButtonPanel;
    [SerializeField] private Button knowledgeButton;

    [Header("Accusation")]
    [SerializeField] private GameObject accusationPanel;
    [SerializeField] private GameObject accusationButtonPanel;
    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private TextMeshProUGUI resultsText;

    [Header("Misc")]
    [SerializeField] private GameObject clueFoundPanel;
    [SerializeField] private TextMeshProUGUI clueFoundText;
    [SerializeField] private GameObject scanPanel;

    [Header("Icons")]
    [SerializeField] private GameObject journalIcon;
    [SerializeField] private GameObject magIcon;

    [Header("End Game")]
    [SerializeField] private Newspaper[] newspapers;

    private List<QuestionButton> questionButtons;
    private List<QuestionButton> accusationButtons;

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


    private void OnEnable()
    {
        EventManager.OnClueFound += UpdateClueText;
        EventManager.OnAccusation += HandleEndGame;
    }

    private void OnDisable()
    {
        EventManager.OnClueFound -= UpdateClueText;
        EventManager.OnAccusation -= HandleEndGame;
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        questionButtons = new List<QuestionButton>();
        accusationButtons = new List<QuestionButton>();
        knowledgeButton.onClick.AddListener(() => { QuestionHandler.ProbeClue(new Clue("Knowledge", "Preliminary knowledge the character knows.")); });
        //AddClue("Case", new Clue("Knowledge", ""));
        //questionButtons.Add(new QuestionButton(questionCloseButton, new Clue()));
    }

    public void DisplayDialoguePanel(string characterName, string dialogue)
    {
        dialoguePanel.SetActive(true);
        nameText.text = characterName;
        dialogueText.text = dialogue;

        //StartCoroutine(DeactivateAfterTime(dialoguePanel));
    }

    public void ToggleQuestionPanel(string characterName, string characterIntro)
    {
        if (accusationPanel.activeInHierarchy)
            return;

        questionPanel.SetActive(!questionPanel.activeInHierarchy);

        // If we toggled on the question panel, fill it with all of the
        // buttons we need.
        if (questionPanel.activeInHierarchy)
        {
            DisplayDialoguePanel(characterName, characterIntro);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PopulateQuestionPanelButtons();
            GameManager.Instance.PauseGame();
        }
        else
        {
            OnQuestionPanelDisable();
        }
    }

    public void DisplayAccusationPanel()
    {
        questionPanel.SetActive(false);
        accusationPanel?.SetActive(true);

        // If we toggled on the accusation panel, fill it with all of the
        // button we need.
        if (accusationPanel.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PopulateAccusationPanelButtons();
            GameManager.Instance.PauseGame();
        }
    }

    public void OnAccusationPanelDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        dialoguePanel.SetActive(false);
        accusationPanel.SetActive(false);
        GameManager.Instance.UnpauseGame();
    }

    public void OnQuestionPanelDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        dialoguePanel.SetActive(false);
        GameManager.Instance.UnpauseGame();
    }

    public void DisplayScanPanel()
    {
        scanPanel.SetActive(true);
    }

    public void HideScanPanel()
    {
        scanPanel.SetActive(false);
    }

    private void UpdateClueText(Clue clue)
    {
        print("CLUE FOUND: " + clue.ClueTag);
        clueFoundText.text = "Clue Recorded: " + clue.ClueTag.ToUpper();
        clueFoundPanel.SetActive(true);
        //PopulateQuestionPanelButtons();
        StartCoroutine(DeactivateAfterTime(clueFoundPanel));
    }

    private void HandleEndGame(bool correctAccusation)
    {
        journalIcon.SetActive(false);
        magIcon.SetActive(false);

        Newspaper newspaper;
        if (correctAccusation)
            newspaper = newspapers.Where(t => t.Character == NPCInteraction.activeCharacter.Name).FirstOrDefault();
        else
            newspaper = newspapers.Where(t => t.Character == "Incorrect").FirstOrDefault();

        if (newspaper == null)
            throw new MissingReferenceException("The newspaper could not be found!");

        StartCoroutine(WaitThenSpawnNewspaper(newspaper));
    }

    private IEnumerator WaitThenSpawnNewspaper(Newspaper newspaper)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(newspaper, transform.GetChild(0));
    }

    //public void HideOpenPanels()
    //{
    //    StopAllCoroutines();
    //    dialoguePanel.SetActive(false);
    //    accusationPanel.SetActive(false);
    //}

    public void PopulateQuestionPanelButtons()
    {
        Clue[] knownClues = Journal.Instance.GetAllKnownClues();

        // Check to see if we already have a button associated with the clue.
        // If we do, don't create a new one.
        foreach (Clue clue in knownClues)
        {
            if (clue.ClueTag == string.Empty)
                continue;

            //print(clue.ClueTag);
            bool foundClue = false;

            foreach (QuestionButton qb in questionButtons)
            {
                if (qb.clue.ClueTag == clue.ClueTag)
                {
                    foundClue = true;
                    break;
                }
            }

            if (foundClue)
                continue;

            print("Creating a button for clue " + clue.ClueTag);

            Button button = Instantiate(buttonPrefab, questionButtonPanel.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = clue.ClueTag + "?";

            // Reset whatever was on the close button.
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { QuestionHandler.ProbeClue(clue); });

            questionButtons.Add(new QuestionButton(button, clue));
        }
        
    }

    public void PopulateAccusationPanelButtons()
    {
        foreach (QuestionButton qb in questionButtons)
        {
            // If we already have an accusation button set up, continue.
            if (accusationButtons.Contains(qb))
                continue;

            Button button = Instantiate(buttonPrefab, accusationButtonPanel.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = qb.clue.ClueTag;

            // Reset whatever was on the close button.
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { GameManager.Instance.AccuseCharacter(qb.clue); });

            accusationButtons.Add(qb);
        }
    }

    private IEnumerator DeactivateAfterTime(GameObject obj, float time = -1)
    {
        if (time < 0)
            time = dialogueScreenTime;

        yield return new WaitForSecondsRealtime(dialogueScreenTime);

        obj.SetActive(false);
    }
}