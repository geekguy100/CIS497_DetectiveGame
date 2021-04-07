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
        if (journal.activeSelf == true) journal.SetActive(false);
        else journal.SetActive(true);
    }
}
