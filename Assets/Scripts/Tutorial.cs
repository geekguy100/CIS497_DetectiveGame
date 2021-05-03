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
    public bool continueOnNevermind;
}

public class Tutorial : Singleton<Tutorial>
{
    [SerializeField] TutorialMessage[] messages;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] TextMeshProUGUI tutorialText;
    private string currentMessage;
    private bool startedTutorial = false;
    private int index = 0;

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
        if (startedTutorial == false && SceneManager.GetActiveScene().name == "ChrisScene")
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

    }
    public void ContinueOnScan()
    {

    }
    public void ContinueOnJournal()
    {

    }
    public void ContinueOnPage()
    {

    }
    public void ContinueOnNevermind()
    {

    }
}
