using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalToggle : MonoBehaviour
{
    [SerializeField] GameObject journal;

    public void ToggleJournal()
    {
        if (journal.activeSelf == true) journal.SetActive(false);
        else journal.SetActive(true);
    }
}
