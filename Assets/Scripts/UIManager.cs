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
    [SerializeField] private GameObject speechBubble;

    [Header("End Game")]
    [SerializeField] private Newspaper[] newspapers;
    [SerializeField] private GameObject journal;
    private Newspaper activeNewspaper;

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
        EventManager.OnGameRestart += Init;
    }

    private void OnDisable()
    {
        EventManager.OnClueFound -= UpdateClueText;
        EventManager.OnAccusation -= HandleEndGame;
        EventManager.OnGameRestart -= Init;
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
        print(DialogueHandler.GetCulpritName());
    }

    public void DisplayDialoguePanel(string characterName, string dialogue)
    {
        dialoguePanel.SetActive(true);
        nameText.text = characterName;
        dialogueText.text = dialogue;

        //StartCoroutine(DeactivateAfterTime(dialoguePanel));
    }

    public void ShowSpeechBubble()
    {
        speechBubble.SetActive(true);
    }

    public void HideSpeechBubble()
    {
        speechBubble.SetActive(false);
    }

    public void ToggleQuestionPanel(string characterName, string characterIntro)
    {
        if (journal.activeInHierarchy)
        {
            journal.SetActive(false);
        }

        if (accusationPanel.activeInHierarchy)
            return;

        questionPanel.SetActive(!questionPanel.activeInHierarchy);

        // If we toggled on the question panel, fill it with all of the
        // buttons we need.
        if (questionPanel.activeInHierarchy)
        {
            DisplayDialoguePanel(characterName, characterIntro);
            if (!SFXManager.Instance.source.isPlaying)
            {
                SFXManager.Instance.source.PlayOneShot(SFXManager.Instance.gibberish);
            }

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

    public bool IsQPanelActive()
    {
        return questionPanel.activeInHierarchy;
    }

    public void DisplayAccusationPanel()
    {
        questionPanel.SetActive(false);
        accusationPanel.SetActive(true);

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
        if (SFXManager.Instance.source.isPlaying)
        {
            SFXManager.Instance.source.Stop();
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        dialoguePanel.SetActive(false);
        questionPanel.SetActive(false);
        GameManager.Instance.UnpauseGame();

        Tutorial.Instance.ContinueOnNevermind();
    }

    public void DisplayScanPanel()
    {
        scanPanel.SetActive(true);
    }

    public void HideScanPanel()
    {
        scanPanel.SetActive(false);
    }

    public bool IsPanelOpen()
    {
        return (questionButtonPanel.activeInHierarchy || accusationButtonPanel.activeInHierarchy);
    }

    private void UpdateClueText(Clue clue)
    {
        print("CLUE FOUND: " + clue.ClueTag + " : " + clue.Person);
        SFXManager.Instance.source.PlayOneShot(SFXManager.Instance.clueFound);
        clueFoundText.text = "Clue Recorded: " + clue.ClueTag.ToUpper() + ": <i>" + clue.Person + "</i>";
        clueFoundPanel.SetActive(true);
        //PopulateQuestionPanelButtons();
        StartCoroutine(DeactivateAfterTime(clueFoundPanel));
    }

    private void HandleEndGame(bool correctAccusation)
    {
        journalIcon.SetActive(false);
        magIcon.SetActive(false);
        journal.SetActive(false);
        dialoguePanel.SetActive(false);
        accusationPanel.SetActive(false);
        HideSpeechBubble();
        Tutorial.Instance.HideTutorial();

        Newspaper newspaper;

        if (correctAccusation)
        {
            Debug.Log("Correct accusation");
            newspaper = newspapers.Where(t => t.Character == NPCInteraction.activeCharacter.Name).FirstOrDefault();
        }
        else
        {
            Debug.Log("Incorrect accusation...");
            newspaper = newspapers.Where(t => t.Character == "Incorrect").FirstOrDefault();
        }

        if (newspaper == null)
            throw new MissingReferenceException("The newspaper could not be found!");

        StartCoroutine(WaitThenSpawnNewspaper(newspaper));
    }

    private IEnumerator WaitThenSpawnNewspaper(Newspaper newspaper)
    {
        yield return new WaitForSeconds(2f);
        activeNewspaper = Instantiate(newspaper, transform.GetChild(0));
        resultsPanel.SetActive(true);
        IterateClues();
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
            button.onClick.AddListener(() => {SFXManager.Instance.source.PlayOneShot(SFXManager.Instance.click);});

            questionButtons.Add(new QuestionButton(button, clue));
        }
        
    }

    public void PopulateAccusationPanelButtons()
    {
        Clue[] knownClues = Journal.Instance.GetAllKnownClues();

        foreach (Clue clue in knownClues)
        {
            if (clue.ClueTag == string.Empty)
                continue;

            //print(clue.ClueTag);
            bool foundClue = false;

            foreach (QuestionButton qb in accusationButtons)
            {
                if (qb.clue.ClueTag == clue.ClueTag)
                {
                    foundClue = true;
                    break;
                }
            }

            if (foundClue)
                continue;

            print("Creating an accusation button for clue " + clue.ClueTag);

            Button button = Instantiate(buttonPrefab, accusationButtonPanel.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = clue.ClueTag;

            // Reset whatever was on the close button.
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { GameManager.Instance.AccuseCharacter(clue); });
            button.onClick.AddListener(() => { SFXManager.Instance.source.PlayOneShot(SFXManager.Instance.click); });

            accusationButtons.Add(new QuestionButton(button, clue));
        }
    }

    private IEnumerator DeactivateAfterTime(GameObject obj, float time = -1)
    {
        if (time < 0)
            time = dialogueScreenTime;

        yield return new WaitForSecondsRealtime(dialogueScreenTime);

        obj.SetActive(false);
    }

    /// <summary>
    /// Initializes all UI elements (icons, buttons, etc.)
    /// </summary>
    private void Init()
    {
        journalIcon.SetActive(true);
        magIcon.SetActive(true);
        HideSpeechBubble();
        journal.SetActive(false);
        resultsPanel.SetActive(false);

        if (activeNewspaper != null)
            Destroy(activeNewspaper.gameObject);

        for (int i = 0; i < questionButtons.Count; ++i)
        {
            Destroy(questionButtons[i].button.gameObject);
            questionButtons.RemoveAt(i);
        }

        for (int i = 0; i < accusationButtons.Count; ++i)
        {
            Destroy(accusationButtons[i].button.gameObject);
            accusationButtons.RemoveAt(i);
        }
    }

    public void IterateClues()
    {
        ClueIterator iterator = new ClueIterator(Journal.Instance.GetAllKnownClues());
        resultsText.text = string.Empty;
        while(iterator.hasNext())
        {
            string thing = iterator.next().ToString();
            resultsText.text += thing + "\n";
        }
    }
}