/*****************************************************************************
// File Name :         ClueIterator.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class ClueIterator : Iterator
{
    private Clue[] clues;
    private int position = -1;

    public ClueIterator(Clue[] list)
    {
        clues = list;
        Debug.Log("LENGTH::" + clues.Length);
    }
    
    public bool hasNext()
    {
        return (position + 1) < clues.Length;
    }

    public object next()
    {
        ++position;
        return clues[position];
    }
}
