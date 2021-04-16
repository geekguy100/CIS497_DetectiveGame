/*****************************************************************************
// File Name :         QuestionHandler.cs
// Author :            Kyle Grenier
// Creation Date :     04/12/2021
//
// Brief Description : Handles the process of checking if a character knows anything about the selected clue.
*****************************************************************************/
using System.Linq;

public static class QuestionHandler
{
    public static void ProbeClue(Clue clue)
    {
        Character character = NPCInteraction.activeCharacter;
        CharacterData activeCharacterData = character.GetData();

        // If this character knows anything about the clue, let them speak about it.
        if (activeCharacterData.knownClueTags.Contains(clue.ClueTag))
        {
            CharacterClue charClue = character.KnownClues.Where(t => t.ClueTag == clue.ClueTag).FirstOrDefault();
            UIManager.Instance.UpdateDialoguePanel(activeCharacterData.name, charClue.Dialogue);

            //Adding in other clues gained from questioning
            if (activeCharacterData.name == "Richard Wright" && clue.ClueTag == "Knowledge" && !Journal.Instance.HasDiscoveredClue("Black Car"))
            {
                Journal.Instance.AddClue("Misc", new Clue("Black Car", "Reports of a strange black car outside the motel."));
                UIManager.Instance.UpdateClueText("Black Car");
            }
            if (activeCharacterData.name == "Richard Wright" && clue.ClueTag == "Black Car" && !Journal.Instance.HasDiscoveredClue("Altercation"))
            {
                Journal.Instance.AddClue("Misc", new Clue("Altercation", "Reports of a verbal fight between Faith and Nancy."));
                UIManager.Instance.UpdateClueText("Altercation");
            }
            if (activeCharacterData.name == "Nancy Reed" && clue.ClueTag == "Knowledge" && !Journal.Instance.HasDiscoveredClue("Altercation"))
            {
                Journal.Instance.AddClue("Misc", new Clue("Altercation", "Reports of a verbal fight between Faith and Nancy."));
                UIManager.Instance.UpdateClueText("Altercation");
            }
            if (activeCharacterData.name == "Nancy Reed" && clue.ClueTag == "Knowledge" && !Journal.Instance.HasDiscoveredClue("Flirting"))
            {
                Journal.Instance.AddClue("Nancy Reed", new Clue("Flirting", "Reports of flirting between Michael and Nancy."));
                UIManager.Instance.UpdateClueText("Flirting");
            }
        }
        else
            UIManager.Instance.UpdateDialoguePanel(activeCharacterData.name, "I'm not sure what you're talking about...");

        // TODO: We'll need to add in a JSON field to represent what the character says if they don't know what you're talking about. For now, it's hardcoded.
    }
}
