using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class TutorialMessage
{
    [TextArea(5,5)] public string message;

    public bool continueOnJournal;
    public bool continueOnPage;
    public bool continueOnMagnify;
    public bool continueOnScan;
    public bool continueOnTalk;
    public bool continueOnAsk;
    public bool continueOnNevermind;
}

public class Tutorial : Singleton<Tutorial>
{
    [SerializeField] TutorialMessage[] messages;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] TextMeshProUGUI tutorialText;
    private string currentMessage;
    private bool startedTutorial = false;
    private bool skipTutorial = false;
    private int index = 0;

    private void OnEnable()
    {
        EventManager.OnGameRestart += Init;
    }

    private void OnDisable()
    {
        EventManager.OnGameRestart -= Init;
    }

    public void NextMessage()
    {
        index++;
        if (index < messages.Length) tutorialText.text = messages[index].message;
        else
        {
            index = 0;
            tutorialPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (startedTutorial == false && SceneManager.GetActiveScene().name == "ChrisScene" && !skipTutorial)
        {
            index = 0;
            tutorialPanel.SetActive(true);
            tutorialText.text = messages[index].message;

            startedTutorial = true;
        }

        if (Input.GetKeyDown(KeyCode.Return)) NextMessage();
    }

    public void ContinueOnMagnify()
    {
        if (messages[index].continueOnMagnify == true) NextMessage();
    }
    public void ContinueOnTalk()
    {
        if (messages[index].continueOnTalk == true) NextMessage();
    }
    public void ContinueOnScan()
    {
        if (messages[index].continueOnScan == true) NextMessage();
    }
    public void ContinueOnJournal()
    {
        if (messages[index].continueOnJournal == true) NextMessage();
    }
    public void ContinueOnPage()
    {
        if (messages[index].continueOnPage == true) NextMessage();
    }
    public void ContinueOnNevermind()
    {
        if (messages[index].continueOnNevermind == true) NextMessage();
    }
    public void ContinueOnAsk()
    {
        if (messages[index].continueOnAsk == true) NextMessage();
    }
    public void SkipTutorial(bool value)
    {
        skipTutorial = !value;
    }

    public void HideTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    private void Init()
    {
        startedTutorial = false;
    }
}
