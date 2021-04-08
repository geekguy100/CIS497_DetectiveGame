/*****************************************************************************
// File Name :         Character.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

[System.Serializable]
public class Character
{
    public string Name;
    public CharacterClue[] KnownClues;

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
