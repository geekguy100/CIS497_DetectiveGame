/*****************************************************************************
// File Name :         DialogueHandler.cs
// Author :            Kyle Grenier
// Creation Date :     04/09/2021
//
// Brief Description : Handles retrieving the JSON dialogue.
*****************************************************************************/
using UnityEngine;
using System.Linq;

public static class DialogueHandler
{
    private static CharacterDialogue dialogue;
    private static int scenario;
    public static int Scenario { get { return scenario; } }

    [RuntimeInitializeOnLoadMethod]
    private static void LoadDialogue()
    {
        TextAsset jsonFile;
        int r = Random.Range(0, 3);
        scenario = r;
        Debug.Log(r);
        switch (r)
        {
            case 0:
                jsonFile = Resources.Load("CharacterDialogue") as TextAsset;
                break;
            case 1:
                jsonFile = Resources.Load("CharacterDialogue 1") as TextAsset;
                break;
            case 2:
                jsonFile = Resources.Load("CharacterDialogue 2") as TextAsset;
                break;
            default:
                jsonFile = Resources.Load("CharacterDialogue") as TextAsset;
                break;
        }
        
        dialogue = JsonUtility.FromJson<CharacterDialogue>(jsonFile.text);

        Debug.Log("Retrieved character dialogue.");
    }

    /// <summary>
    /// Returns true if the given character knows something about the given clue.
    /// </summary>
    /// <param name="characterName">The character's name.</param>
    /// <param name="clueTag">The clue tag to check for.</param>
    /// <returns>True if the given character knows something about the given clue.</returns>
    public static bool CharacterKnowsClue(string characterName, string clueTag)
    {
        Character character = dialogue.Characters
            .Where(t => t.Name == characterName)
            .FirstOrDefault();

        if (character == null)
        {
            Debug.LogWarning("CHARACTER_KNOWS_CLUE: Cannot find character with name " + characterName);
            return false;
        }

        string[] knownClueTags = character.GetData().knownClueTags;
        if (knownClueTags.Contains(clueTag))
            return true;

        return false;
    }

    public static Character GetCharacter(string name)
    {
        return dialogue.Characters.Where(t => t.Name == name).FirstOrDefault();
    }

    public static string GetCulpritName()
    {
        return dialogue.Culprit;
    }

    public static string[] GetRequiredAccusationClueTags()
    {
        return dialogue.RequiredAccusationClueTags;
    }
}