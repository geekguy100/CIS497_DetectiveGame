/*****************************************************************************
// File Name :         DialogueHandler.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    private CharacterDialogue dialogue = null;

    private void Start()
    {
        TextAsset jsonFile = Resources.Load("CharacterDialogue") as TextAsset;
        dialogue = JsonUtility.FromJson<CharacterDialogue>(jsonFile.text);

        Debug.Log(dialogue.ToString());
    }
}
