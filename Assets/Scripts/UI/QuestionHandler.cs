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
        }
        else
            UIManager.Instance.UpdateDialoguePanel(activeCharacterData.name, "I'm not sure what you're talking about...");

        // TODO: We'll need to add in a JSON field to represent what the character says if they don't know what you're talking about. For now, it's hardcoded.
    }
}
