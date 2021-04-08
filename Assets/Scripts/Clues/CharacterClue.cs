/*****************************************************************************
// File Name :         CharacterClue.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

[System.Serializable]
public class CharacterClue
{
    public string ClueTag;
    public string Dialogue;

    public override string ToString()
    {
        string result = "Clue Tag: " + ClueTag + "\nDialogue: " + Dialogue + "\n";
        return result;
    }
}
