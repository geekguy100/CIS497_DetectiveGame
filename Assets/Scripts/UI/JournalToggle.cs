using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalToggle : MonoBehaviour
{
    [SerializeField] private GameObject journal;
    //[SerializeField] private Journal journalComponent;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J) && !GameManager.Instance.GameOver)
        {
            ToggleJournal();
        }
    }

    public void ToggleJournal()
    {
        if (journal.activeInHierarchy == true)
        {
            journal.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameManager.Instance.UnpauseGame();
            SFXManager.Instance.source.PlayOneShot(SFXManager.Instance.journalClose);
        }
        else
        {
            if (UIManager.Instance.IsQPanelActive())
            {
                UIManager.Instance.OnQuestionPanelDisable();
                //UIManager.Instance.ToggleQuestionPanel();
            }
            journal.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameManager.Instance.PauseGame();
            SFXManager.Instance.source.PlayOneShot(SFXManager.Instance.journalOpen);
        }
    }
}
