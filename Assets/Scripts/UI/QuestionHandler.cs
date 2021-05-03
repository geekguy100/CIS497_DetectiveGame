/*****************************************************************************
// File Name :         QuestionHandler.cs
// Author :            Kyle Grenier
// Creation Date :     04/12/2021
//
// Brief Description : Handles the process of checking if a character knows anything about the selected clue.
*****************************************************************************/
using System.Linq;
using UnityEngine;

public static class QuestionHandler
{
    public static void ProbeClue(Clue clue)
    {
        Character character = NPCInteraction.activeCharacter;
        CharacterData activeCharacterData = character.GetData();

        // If we are probing a character about a clue they have info on and it's NOT already in their journal tab,
        // remove the clue from the Misc tab and add it to their tab.
        ////This code commented out due to unintended side effects. A workaround was made so that this wouldn't be needed anymore.
        ////Sorry Kyle
        /*if (!Journal.Instance.PageContainsClue(activeCharacterData.name, clue) && DialogueHandler.CharacterKnowsClue(activeCharacterData.name, clue.ClueTag))
        {
            Debug.Log("Moving clue: " + clue.ClueTag + " to page: " + activeCharacterData.name);
            Journal.Instance.MoveClueToPage(clue, activeCharacterData.name);
            //Journal.Instance.AddClue(new Clue(clue.ClueTag, clue.ClueDesc, false, clue.Person));
        }*/

        // If this character knows anything about the clue, let them speak about it.
        Debug.Log("Character knows clue tagged " + clue.ClueTag + "?    " + activeCharacterData.knownClueTags.Contains(clue.ClueTag));
        if (activeCharacterData.knownClueTags.Contains(clue.ClueTag))
        {
            CharacterClue charClue = character.KnownClues.Where(t => t.ClueTag == clue.ClueTag).FirstOrDefault();
            UIManager.Instance.DisplayDialoguePanel(activeCharacterData.name, charClue.Dialogue);

            //Adding in other clues gained from questioning
            if (activeCharacterData.name == "Richard Wright" && clue.ClueTag == "Knowledge" && !Journal.Instance.HasDiscoveredClue("Black Car"))
            {
                EventManager.OnClueFound(new Clue("Black Car", "Reports of a strange black car outside the motel.", "Misc"));
                //Journal.Instance.AddClue(new Clue("Black Car", "Reports of a strange black car outside the motel."));
                //UIManager.Instance.UpdateClueText("Black Car");
            }
            if (activeCharacterData.name == "Richard Wright" && clue.ClueTag == "Black Car" && !Journal.Instance.HasDiscoveredClue("Altercation"))
            {
                EventManager.OnClueFound(new Clue("Altercation", "Reports of a verbal fight between Faith and Nancy.", "Misc"));
                //Journal.Instance.AddClue(new Clue("Altercation", "Reports of a verbal fight between Faith and Nancy."));
                //UIManager.Instance.UpdateClueText("Altercation");
            }
            if (activeCharacterData.name == "Nancy Reed" && clue.ClueTag == "Knowledge" && !Journal.Instance.HasDiscoveredClue("Altercation"))
            {
                EventManager.OnClueFound(new Clue("Altercation", "Reports of a verbal fight between Faith and Nancy.", "Misc"));
                //Journal.Instance.AddClue(new Clue("Altercation", "Reports of a verbal fight between Faith and Nancy."));
                //UIManager.Instance.UpdateClueText("Altercation");
            }
            if (activeCharacterData.name == "Nancy Reed" && clue.ClueTag == "Knowledge" && !Journal.Instance.HasDiscoveredClue("Flirting"))
            {
                Debug.Log("Flirting thing");
                EventManager.OnClueFound(new Clue("Flirting", "Reports of flirting between Michael and Nancy.", "Nancy Reed"));
                //Journal.Instance.AddClue("Nancy Reed", new Clue("Flirting", "Reports of flirting between Michael and Nancy."));
                //UIManager.Instance.UpdateClueText("Flirting");
            }
        }
        else
            UIManager.Instance.DisplayDialoguePanel(activeCharacterData.name, "I'm not sure what you're talking about...");

        Tutorial.Instance.ContinueOnAsk();
    }
}
