using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalToggle : ISubject
{
    [SerializeField] private GameObject journal;
    //[SerializeField] private Journal journalComponent;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            ToggleJournal();
            NotifyObservers(journal.activeInHierarchy);
        }
    }

    public void ToggleJournal()
    {
        if (journal.activeInHierarchy == true)
        {
            journal.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            journal.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
