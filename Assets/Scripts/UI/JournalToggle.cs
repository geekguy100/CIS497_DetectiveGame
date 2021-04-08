using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalToggle : MonoBehaviour
{
    [SerializeField] GameObject journal;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            ToggleJournal();
        }
    }

    public void ToggleJournal()
    {
        if (journal.activeSelf == true)
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
