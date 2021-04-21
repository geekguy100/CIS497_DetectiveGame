/*****************************************************************************
// File Name :         GameManager.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    private bool paused = false;
    public bool Paused { get { return paused; } }

    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        paused = false;
        Time.timeScale = 1;
    }

    #region --- Accusation ---

    /// <summary>
    /// Accuses the last character talked to of the murder.
    /// </summary>
    /// <param name="accusationBasis">The Clue that the player determined as the reason for the murder.</param>
    public void AccuseCharacter(Clue accusationBasis)
    {
        // Check if we have the right tags first.
        string[] requiredAccusationTags = DialogueHandler.GetRequiredAccusationClueTags();

        // Since we only need to accuse one clue and have to have the other unlocked, make sure
        // we passed in one of them and have them unlocked in the Journal.
        if (requiredAccusationTags.Contains(accusationBasis.ClueTag))
        {
            // We passed in a correct clue, now let's make sure we have them unlocked in the journal.
            foreach(string tag in requiredAccusationTags)
            {
                if(!Journal.Instance.HasDiscoveredClue(tag))
                {
                    IncorrectAccusation(accusationBasis);
                    return;
                }
            }
        }
        // We didn't pass in a correct clue to begin with, so 
        // automatically an incorrect accusation.
        else
        {
            IncorrectAccusation(accusationBasis);
            return;
        }

        // We passed in the right clues. Now let's make sure we are accusing the right character!
        string characterAccused = NPCInteraction.activeCharacter.Name;
        string culpritName = DialogueHandler.GetCulpritName();

        // The name's match! We chose correctly.
        if (characterAccused == culpritName)
        {
            CorrectAccusation(accusationBasis);
        }
        // The name's mismatched, so incorrect accusation.
        else
        {
            IncorrectAccusation(accusationBasis);
        }
    }

    private void CorrectAccusation(Clue clue)
    {
        string result = "You made the right accusation! " + NPCInteraction.activeCharacter.Name + " == " + DialogueHandler.GetCulpritName() + " is the culprit!\n";
        result += "Clue passed in: " + clue.ClueTag;
        EventManager.Accusation(result);
    }

    private void IncorrectAccusation(Clue accusation)
    {
        string result = "WRONG ACCUSATION\n";

        string accusedCharacterName = NPCInteraction.activeCharacter.Name;
        string missingTags = string.Empty;

        // Check if we have the right tags first.
        string[] requiredAccusationTags = DialogueHandler.GetRequiredAccusationClueTags();

        // Since we only need to accuse one clue and have to have the other unlocked, make sure
        // we passed in one of them and have them unlocked in the Journal.
        if (requiredAccusationTags.Contains(accusation.ClueTag))
        {
            // We passed in a correct clue, now let's make sure we have them unlocked in the journal.
            foreach (string tag in requiredAccusationTags)
            {
                if (!Journal.Instance.HasDiscoveredClue(tag))
                {
                    missingTags += tag + ", ";
                }
            }
        }
        else
            missingTags = "You did not pass in the correct clue.";

        //result += "MISSING TAGS: " + missingTags + "\n";

        //if (DialogueHandler.GetCulpritName() != accusedCharacterName)
        //    result += "You did not accuse the correct character. The culprit was actually " + DialogueHandler.GetCulpritName();

        EventManager.Accusation(result);
    }

    #endregion
}