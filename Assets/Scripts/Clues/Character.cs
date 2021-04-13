/*****************************************************************************
// File Name :         Character.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public struct CharacterData
{
    public CharacterData(string name, string[] knownClueTags)
    {
        this.name = name;
        this.knownClueTags = knownClueTags;
    }

    public readonly string name;
    public readonly string[] knownClueTags;
}

[System.Serializable]
public class Character
{
    public string Name;
    public CharacterClue[] KnownClues;
    public string Intro;

    /// <summary>
    /// Returns a struct containing the character's essential data: name and known clue tags.
    /// </summary>
    /// <returns>The character's essential data.</returns>
    public CharacterData GetData()
    {
        string[] knownClueTags = new string[KnownClues.Length];

        for (int i = 0; i < knownClueTags.Length; ++i)
            knownClueTags[i] = KnownClues[i].ClueTag;

        return new CharacterData(Name, knownClueTags);
    }

    public override string ToString()
    {
        string result = "Name: " + Name + ":\n";

        if (KnownClues != null)
        {
            foreach (CharacterClue clue in KnownClues)
            {
                result += clue.ToString();
            }
        }
        else
            result += "No known clues...\n";

        return result;
    }
}
