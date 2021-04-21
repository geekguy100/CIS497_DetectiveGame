/*****************************************************************************
// File Name :         CharacterDialogue.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

[System.Serializable]
public class CharacterDialogue
{
    public Character[] Characters;
    public string Culprit;
    public string[] RequiredAccusationClueTags;

    public override string ToString()
    {
        string result = string.Empty;

        foreach (Character character in Characters)
        {
            result += character.ToString();
            result += "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";
        }

        return "All Character Dialogues\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" + result;
    }
}