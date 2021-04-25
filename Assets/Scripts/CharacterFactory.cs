/*****************************************************************************
// File Name :         CharacterFactory.cs
// Author :            Kyle Grenier
// Creation Date :     04/25/2021
//
// Brief Description : A simple factory used for retrieving character prefabs.
*****************************************************************************/
using UnityEngine;

public static class CharacterFactory
{
    public static GameObject GetCharacterPrefab(string characterName)
    {
        Debug.Log(characterName);
        return Resources.Load<GameObject>("Characters/" + characterName);
    }
}